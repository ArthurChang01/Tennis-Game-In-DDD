using System;
using NEventStore.Logging;

namespace TennisGame.Persistent.NEventStore.Configs
{
    public class LogSetting
    {
        #region Constructors

        public LogSetting()
        {
            NeedLog = false;
            Logger = null;
        }

        public LogSetting(bool needLog, Func<Type, ILog> logger)
        {
            NeedLog = needLog;
            Logger = logger;
        }

        #endregion Constructors

        public bool NeedLog { get; }

        public Func<Type, ILog> Logger { get; }
    }
}