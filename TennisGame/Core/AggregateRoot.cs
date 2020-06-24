using System.Collections.Generic;

namespace TennisGame.Core
{
    public abstract class AggregateRoot<TId> : IAggregateRoot<TId>
    {
        #region Fields

        private readonly HashSet<DomainEvent> _events;

        #endregion Fields

        #region Constructors

        protected AggregateRoot()
        {
            _events = new HashSet<DomainEvent>();
        }

        protected AggregateRoot(TId id, int version)
            : this()
        {
            Id = id;
            EventVersion = version;
        }

        #endregion Constructors

        #region Properties

        public TId Id { get; protected set; }
        public int EventVersion { get; protected set; }
        public IReadOnlyCollection<DomainEvent> Events => _events;

        #endregion Properties

        #region Static Methods

        public static AggregateRoot<TId> Create<T>(T ag, IEnumerable<DomainEvent> @events)
        where T : AggregateRoot<TId>
        {
            foreach (var @event in @events)
            {
                ag.ApplyEvent(@event);
            }

            ag.ClearUncommittedEvents();

            return ag;
        }

        #endregion Static Methods

        #region Protected Methods

        protected void RaiseEvent(DomainEvent @event)
        {
            @event.Version = EventVersion;
            _events.Add(@event);
        }

        protected abstract void ApplyEvent(DomainEvent @event);

        #endregion Protected Methods

        #region Private Methods

        private void ClearUncommittedEvents()
        {
            _events.Clear();
        }

        #endregion Private Methods

        public override bool Equals(object? obj)
        {
            if (!(obj is AggregateRoot<TId> target))
                return false;

            if (ReferenceEquals(this, target))
                return true;

            return Id.Equals(target.Id);
        }

        public override int GetHashCode()
            => Id.GetHashCode();
    }
}