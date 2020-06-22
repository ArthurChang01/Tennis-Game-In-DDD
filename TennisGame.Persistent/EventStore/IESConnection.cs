using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace TennisGame.Persistent.EventStore
{
    public interface IESConnection
    {
        Task<IEventStoreConnection> GetConnectionAsync();
    }
}