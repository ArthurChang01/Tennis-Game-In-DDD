using System;
using System.Collections.Generic;

namespace TennisGame.Core
{
    public abstract class DomainEvent : IEqualityComparer<DomainEvent>
    {
        #region Construcktors

        public DomainEvent()
        {
        }

        protected DomainEvent(int version = 1)
            : this(Guid.NewGuid().ToString(), version, DateTimeOffset.Now)
        {
        }

        public DomainEvent(string id, int version, DateTimeOffset occuredDate)
        {
            Id = id;
            Version = version;
            OccuredDate = occuredDate;
        }

        #endregion Construcktors

        #region Properties

        public string Id { get; }

        public int Version { get; }

        public DateTimeOffset OccuredDate { get; }

        #endregion Properties

        #region Override Methods

        public override bool Equals(object? obj)
        {
            var target = obj as DomainEvent;
            if (null == target)
                return false;

            if (ReferenceEquals(this, target))
                return true;

            return Id.Equals(target.Id) && Version.Equals(target.Version) && OccuredDate.Equals(target.OccuredDate);
        }

        public override int GetHashCode()
            => (Id, Version, OccurredDate: OccuredDate).GetHashCode();

        #endregion Override Methods

        public bool Equals(DomainEvent x, DomainEvent y)
        {
            if (ReferenceEquals(x, y))
                return true;

            return x.Id.Equals(y.Id) && x.Version.Equals(y.Version) &&
                   x.OccuredDate.ToString("yyyyMMdd HH:mm:ss").Equals(y.OccuredDate.ToString("yyyyMMdd HH:mm:ss"));
        }

        public int GetHashCode(DomainEvent obj)
        {
            return (obj.Id, obj.Version, obj.OccuredDate).GetHashCode();
        }
    }
}