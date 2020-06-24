using System;
using System.Collections.Generic;
using System.Linq;
using NEventStore;
using NEventStore.Serialization;
using TennisGame.Core;
using TennisGame.Models;
using TennisGame.Persistent.NEventStore.Configs;

namespace TennisGame.Persistent.NEventStore
{
    public sealed class NEventRepository<T, TId> : IDisposable
        where T : AggregateRoot<TId>, new()
    {
        #region Fields

        private IStoreEvents _store;
        private bool _disposedValue;
        private readonly int _snapShotThreshold;

        #endregion Fields

        #region Conctructors

        public NEventRepository()
            : this(new NEventConfig())
        {
        }

        public NEventRepository(NEventConfig config)
        {
            _snapShotThreshold = config.SnapShotThreshold;

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

            if (NeedToSnapShot(stream))
                _store.Advanced.AddSnapshot(new Snapshot(streamId, stream.StreamRevision, game));
        }

        #endregion Public Methods

        #region Private Methods

        private bool NeedToSnapShot(IEventStream stream)
        {
            return stream.StreamRevision % _snapShotThreshold == 0;
        }

        #endregion Private Methods

        #region Dispose Methods

        private void Dispose(bool disposing)
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
}