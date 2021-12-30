using HarmonyLib;
using KMod;
using static Common.Logger;

namespace SortArtifactsFirst
{
    public class SortArtifactsFirstMod : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            LogInit(this.mod);
            base.OnLoad(harmony);
        }
    }
}
