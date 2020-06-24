namespace TennisGame.Persistent.NEventStore.Configs
{
    public class StorageEngineSetting
    {
        #region Constructors

        public StorageEngineSetting()
        {
            InMemory = true;
            ConnectionString = string.Empty;
        }

        public StorageEngineSetting(bool inMemory, string connectionString)
        {
            InMemory = inMemory;
            ConnectionString = connectionString;
        }

        #endregion Constructors

        public bool InMemory { get; }
        public string ConnectionString { get; }
    }
}