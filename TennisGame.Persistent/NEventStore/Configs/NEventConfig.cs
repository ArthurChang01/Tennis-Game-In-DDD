namespace TennisGame.Persistent.NEventStore.Configs
{
    public class NEventConfig
    {
        #region Cosntructors

        public NEventConfig()
        {
            Log = new LogSetting();
            Hook = new HookSetting();
            StorageEngine = new StorageEngineSetting();
            SnapShotThreshold = 200;
        }

        public NEventConfig(LogSetting log, HookSetting hook, StorageEngineSetting store, int snapShotThreshold)
        {
            Log = log;
            Hook = hook;
            StorageEngine = store;
            SnapShotThreshold = snapShotThreshold;
        }

        #endregion Cosntructors

        public LogSetting Log { get; set; }
        public HookSetting Hook { get; set; }
        public StorageEngineSetting StorageEngine { get; set; }
        public int SnapShotThreshold { get; set; }
    }
}