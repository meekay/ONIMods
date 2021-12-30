using HarmonyLib;
using KMod;
using static Common.Logger;

namespace AutoSavePerformance
{
    public class AutoSavePerformanceMod : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            LogInit(this.mod);
            base.OnLoad(harmony);
        }
    }
}
