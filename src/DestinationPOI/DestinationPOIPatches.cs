using HarmonyLib;
using UnityEngine;

namespace DestinationPOI
{
    public class DestinationPOIPatches
    {
        [HarmonyPatch(typeof(ClusterGrid), "GetLocationDescription")]
        public static class ClusterGrid_GetLocationDescription_Patch
        {
            [HarmonyPrefix]
            public static bool Prefix (ClusterGrid __instance, AxialI location, ref Sprite sprite, ref string label, ref string sublabel)
            {
                ClusterGridEntity poi = __instance.GetVisibleEntityOfLayerAtCell(location, EntityLayer.POI);
                if (null != poi)
                {
                    label = poi.Name;
                    return false;
                }

                return true;
            }
        }
    }
}
