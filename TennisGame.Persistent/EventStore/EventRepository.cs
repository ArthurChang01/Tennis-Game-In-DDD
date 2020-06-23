using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using TennisGame.Core;

namespace TennisGame.Persistent.EventStore
{
    public class EventRepository<T, TId> : IEventRepository<T, TId>
        where T : AggregateRoot<TId>, new()
    {
        #region Fields

        private readonly IESConnection _con;
        private readonly IEventSerializer _serializer;
        private readonly string _streamBaseName;

        #endregion Fields

        #region Constructors

        public EventRepository(IESConnection con, IEventSerializer serializer)
        {
            _con = con;
            _serializer = serializer;

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
                    var eventData = _serializer.Convert(@event);
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
                events.AddRange(currentSlice.Events.Select(_serializer.Convert));
            } while (!currentSlice.IsEndOfStream);

            return (T)AggregateRoot<TId>.Create<T>(new T(), events.OrderBy(o => o.OccuredDate));
        }

        #endregion Public Methods

        #region Private Methods

        private string GetStreamName(object id)
            => $"{_streamBaseName}_{id}";

        #endregion Private Methods
    }
}