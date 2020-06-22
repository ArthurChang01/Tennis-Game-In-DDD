using System;

namespace TennisGame.Core
{
    public abstract class DomainEvent
    {
        #region Construcktors

        protected DomainEvent(int version = 1)
        {
            Id = Guid.NewGuid().ToString();
            Version = version;
            OccurredDate = DateTimeOffset.Now;
        }

        #endregion Construcktors

        #region Properties

        public string Id { get; }

        public int Version { get; }

        public DateTimeOffset OccurredDate { get; }

        #endregion Properties

        #region Override Methods

        public override bool Equals(object? obj)
        {
            var target = obj as DomainEvent;
            if (null == target)
                return false;

            if (ReferenceEquals(this, target))
                return true;

            return Id.Equals(target.Id) && Version.Equals(target.Version) && OccurredDate.Equals(target.OccurredDate);
        }

        public override int GetHashCode()
            => (Id, Version, OccurredDate).GetHashCode();

        #endregion Override Methods
    }
}