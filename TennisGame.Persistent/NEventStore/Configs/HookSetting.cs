using System.Collections.Generic;
using NEventStore;

namespace TennisGame.Persistent.NEventStore.Configs
{
    public class HookSetting
    {
        #region Constructors

        public HookSetting()
        {
            HasHook = false;
            Hooks = new List<IPipelineHook>();
        }

        public HookSetting(bool hasHook, IEnumerable<IPipelineHook> hooks)
        {
            HasHook = hasHook;
            Hooks = hooks;
        }

        #endregion Constructors

        public bool HasHook { get; }
        public IEnumerable<IPipelineHook> Hooks { get; }
    }
}