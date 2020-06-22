using System.Threading.Tasks;
using TennisGame.Core;

namespace TennisGame.Persistent.EventStore
{
    public interface IEventRepository<T, in TId> where T : AggregateRoot<TId>
    {
        Task Append(T aggregate);
        Task<T> Rehydrate(TId id);
    }
}