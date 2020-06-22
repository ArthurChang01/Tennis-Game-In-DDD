using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Microsoft.Extensions.Logging;

namespace TennisGame.Persistent.EventStore
{
    public class ESConnection : IDisposable, IESConnection
    {
        #region Fields

        private readonly Uri _conString;
        private readonly ILogger<ESConnection> _logger;
        private readonly Lazy<Task<IEventStoreConnection>> _lazyConnection;

        #endregion Fields

        #region Constructors

        public ESConnection(Uri conString, ILogger<ESConnection> logger)
        {
            _conString = conString;
            _logger = logger;

            _lazyConnection = new Lazy<Task<IEventStoreConnection>>(() =>
            {
                return Task.Run(async () =>
                {
                    var con = SetupConnection();
                    await con.ConnectAsync();
                    return con;
                });
            });
        }

        #endregion Constructors

        #region Public Methods

        public Task<IEventStoreConnection> GetConnectionAsync()
        {
            return _lazyConnection.Value;
        }

        public void Dispose()
        {
            if (!_lazyConnection.IsValueCreated)
                return;

            _lazyConnection.Value.Result.Dispose();
        }

        #endregion Public Methods

        #region Private Methods

        private IEventStoreConnection SetupConnection()
        {
            var settings = ConnectionSettings.Create()
                .EnableVerboseLogging()
                .UseConsoleLogger()
                .DisableTls()
                .Build();
            var con = EventStoreConnection.Create(settings, _conString);

            con.ErrorOccurred += async (s, e) =>
            {
                _logger.LogWarning(e.Exception, $"an error has occurred on the Eventstore connection: {e.Exception.Message}. Trying to reconnect...");
                con = SetupConnection();
                await con.ConnectAsync();
            };
            con.Disconnected += async (s, e) =>
            {
                _logger.LogWarning($"The Eventstore connection has dropped. Trying to reconnect ...");
                con = SetupConnection();
                await con.ConnectAsync();
            };
            con.Closed += async (s, e) =>
            {
                _logger.LogWarning($"The Eventstore connection was closed: {e.Reason}. Opening new connection ...");
                con = SetupConnection();
                await con.ConnectAsync();
            };

            return con;
        }

        #endregion Private Methods
    }
}