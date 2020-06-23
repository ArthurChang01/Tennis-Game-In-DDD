using EventStore.ClientAPI;
using TennisGame.Core;

namespace TennisGame.Persistent.EventStore
{
    public interface IEventSerializer
    {
        DomainEvent Convert(ResolvedEvent @event);

        EventData Convert(DomainEvent @event);
    }
}