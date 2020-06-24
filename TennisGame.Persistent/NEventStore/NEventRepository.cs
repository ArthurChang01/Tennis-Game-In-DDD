using System;
using System.Collections.Generic;
using System.Linq;
using NEventStore;
using NEventStore.Logging;
using NEventStore.Serialization;
using TennisGame.Core;
using TennisGame.Models;

namespace TennisGame.Persistent.NEventStore
{
    public class NEventRepository<T, TId> : IDisposable
        where T : AggregateRoot<TId>, new()
    {
        #region Fields

        private IStoreEvents _store;
        private bool _disposedValue;

        #endregion Fields

        #region Conctructors

        public NEventRepository()
            : this(new NEventConfig())
        {
        }

        public NEventRepository(NEventConfig config)
        {
            var preStore = Wireup.Init();
            if (config.Log.NeedLog)
                preStore = preStore.LogTo(config.Log.Logger);

            if (config.Hook.HasHook)
                preStore = preStore.UseOptimisticPipelineHook()
                    .HookIntoPipelineUsing(config.Hook.Hooks);

            preStore = config.StorageEngine.InMemory ?
                preStore.UsingInMemoryPersistence() :
                preStore.UsingMongoPersistence(config.StorageEngine.ConnectionString, new DocumentObjectSerializer());

            _store = ((PersistenceWireup)preStore).InitializeStorageEngine().Build();
        }

        #endregion Conctructors

        #region Public Methods

        public T Rehydrate(TId id)
        {
            T aggregate;
            var streamId = id.ToString();
            var latestSnapshot = _store.Advanced.GetSnapshot(streamId, int.MaxValue);
            using var stream = latestSnapshot == null ? _store.OpenStream(streamId) : _store.OpenStream(latestSnapshot, int.MinValue);

            if (latestSnapshot != null)
                aggregate = (T)_store.Advanced.GetSnapshot(streamId, int.MaxValue).Payload;
            else
            {
                aggregate = new T();
            }

            return (T)AggregateRoot<TId>.Create(aggregate, stream.CommittedEvents.Select(o => o.Body).OfType<DomainEvent>());
        }

        public void PersistentEvent(IAggregateRoot<GameId> game)
        {
            var streamId = game.Id.ToString();
            var latestSnapshot = _store.Advanced.GetSnapshot(streamId, int.MaxValue);
            using var stream = latestSnapshot == null ? _store.OpenStream(streamId) : _store.OpenStream(latestSnapshot, int.MinValue);

            foreach (var @event in game.Events)
            {
                stream.Add(new EventMessage()
                {
                    Headers = new Dictionary<string, object>()
                {
                    {"EventType", @event.GetType().AssemblyQualifiedName}
                },
                    Body = @event
                });
            }

            stream.CommitChanges(Guid.NewGuid());

            if (stream.StreamRevision % 200 == 0)
                _store.Advanced.AddSnapshot(new Snapshot(streamId, stream.StreamRevision, game));
        }

        #endregion Public Methods

        #region Dispose Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _store.Dispose();
                }

                _store = null;
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion Dispose Methods
    }

    public class NEventConfig
    {
        #region Cosntructors

        public NEventConfig()
        {
            Log = new LogSetting();
            Hook = new HookSetting();
            StorageEngine = new StorageEngineSetting();
        }

        public NEventConfig(LogSetting log, HookSetting hook, StorageEngineSetting store)
        {
            Log = log;
            Hook = hook;
            StorageEngine = store;
        }

        #endregion Cosntructors

        public LogSetting Log { get; }
        public HookSetting Hook { get; }
        public StorageEngineSetting StorageEngine { get; }
    }

    public class StorageEngineSetting
    {
        #region Constructors

        public StorageEngineSetting()
        {
            InMemory = true;
            ConnectionString = string.Empty;
        }

        public StorageEngineSetting(bool inMemory, string connectionString)
        {
            InMemory = inMemory;
            ConnectionString = connectionString;
        }

        #endregion Constructors

        public bool InMemory { get; }
        public string ConnectionString { get; }
    }

    public class HookSetting
    {
        #region Constructors

        public HookSetting()
        {
            HasHook = false;
            Hooks = new List<IPipelineHook>();
        }

        public HookSetting(bool hasHook, IEnumerable<IPipelineHook> hooks)
        {
            HasHook = hasHook;
            Hooks = hooks;
        }

        #endregion Constructors

        public bool HasHook { get; }
        public IEnumerable<IPipelineHook> Hooks { get; }
    }

    public class LogSetting
    {
        #region Constructors

        public LogSetting()
        {
            NeedLog = false;
            Logger = null;
        }

        public LogSetting(bool needLog, Func<Type, ILog> logger)
        {
            NeedLog = needLog;
            Logger = logger;
        }

        #endregion Constructors

        public bool NeedLog { get; }

        public Func<Type, ILog> Logger { get; }
    }
}