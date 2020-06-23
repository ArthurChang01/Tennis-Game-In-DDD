using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using TennisGame.Core;
using TennisGame.Persistent.EventStore.JsonConverters;

namespace TennisGame.Persistent.EventStore
{
    public class EventRepository<T, TId> : IEventRepository<T, TId>
        where T : AggregateRoot<TId>, new()
    {
        #region Fields

        private readonly ESConnection _con;
        private readonly string _streamBaseName;

        #endregion Fields

        #region Constructors

        public EventRepository(ESConnection con)
        {
            _con = con;

            _streamBaseName = typeof(T).Name;
        }

        #endregion Constructors

        #region Public Methods

        public async Task Append(T aggregate)
        {
            if (null == aggregate)
                throw new ArgumentException(nameof(aggregate));

            var con = await _con.GetConnectionAsync();
            var streamName = GetStreamName(aggregate.Id);

            using var tran = await con.StartTransactionAsync(streamName, aggregate.EventVersion);
            try
            {
                foreach (var @event in aggregate.Events)
                {
                    var eventData = Map(@event);
                    await tran.WriteAsync(eventData);
                }

                await tran.CommitAsync();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        public async Task<T> Rehydrate(TId id)
        {
            var con = await _con.GetConnectionAsync();

            var streamName = GetStreamName(id);

            var events = new List<DomainEvent>();

            StreamEventsSlice currentSlice;
            long nextSliceStart = StreamPosition.Start;
            do
            {
                currentSlice = await con.ReadStreamEventsForwardAsync(streamName, nextSliceStart, 200, false);
                nextSliceStart = currentSlice.NextEventNumber;
                events.AddRange(currentSlice.Events.Select(Map));
            } while (!currentSlice.IsEndOfStream);

            return (T)AggregateRoot<TId>.Create<T>(new T(), events.OrderBy(o => o.OccurredDate));
        }

        #endregion Public Methods

        #region Private Methods

        private DomainEvent Map(ResolvedEvent @event)
        {
            var meta = JsonSerializer.Deserialize<EventMeta>(@event.Event.Metadata);
            var opt = new JsonSerializerOptions();
            opt.Converters.Add(new GameIdConverter());
            opt.Converters.Add(new PlayersConverter());
            opt.Converters.Add(new TeamConverter());

            return JsonSerializer.Deserialize(@event.Event.Data, Type.GetType(meta.EventType), opt) as DomainEvent;
        }

        private EventData Map(DomainEvent @event)
        {
            var json = JsonSerializer.Serialize(@event,
                new JsonSerializerOptions() { IgnoreNullValues = false, MaxDepth = int.MaxValue }
                );
            var data = Encoding.UTF8.GetBytes(json);

            var evnType = @event.GetType();
            var meta = new EventMeta()
            {
                EventType = evnType.AssemblyQualifiedName
            };
            var metaJson = JsonSerializer.Serialize(meta);
            var metaData = Encoding.UTF8.GetBytes(metaJson);

            return new EventData(Guid.Parse(@event.Id), evnType.Name, true, data, metaData);
        }

        private string GetStreamName(object id)
            => $"{_streamBaseName}_{id}";

        #endregion Private Methods
    }
}