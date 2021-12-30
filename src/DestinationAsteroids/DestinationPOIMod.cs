using HarmonyLib;
using KMod;
using static Common.Logger;

namespace DestinationPOI
{
    public class DestinationPOIMod : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            LogInit(this.mod);
            base.OnLoad(harmony);
        }
    }
}
