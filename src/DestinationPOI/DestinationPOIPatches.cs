using HarmonyLib;
using UnityEngine;
using static Common.Logger;

namespace DestinationPOI
{
    public class DestinationPOIPatches
    {
        [HarmonyPatch(typeof(ClusterGrid), "GetLocationDescription")]
        public static class ClusterGrid_GetLocationDescription_Patch
        {
            [HarmonyPostfix]
            public static void Postfix (ClusterGrid __instance, AxialI location, ref Sprite sprite, ref string label, ref string sublabel)
            {
                ClusterGridEntity poi = __instance.GetVisibleEntityOfLayerAtCell(location, EntityLayer.POI);
                if (null != poi)
                {
                    label = poi.Name;
                }
            }
        }
    }
}
