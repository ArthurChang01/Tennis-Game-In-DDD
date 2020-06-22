using System.Collections.Generic;

namespace TennisGame.Core
{
    public interface IAggregateRoot<TId>
    {
        TId Id { get; }

        int EventVersion { get; }

        IReadOnlyCollection<DomainEvent> Events { get; }
    }
}