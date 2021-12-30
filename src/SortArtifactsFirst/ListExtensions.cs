using System;
using System.Collections.Generic;
using UnityEngine;

namespace SortArtifactsFirst
{
    public static class ListExtensions
    {
        /// <summary>
        /// This method has an interface like List.Sort() but adds in behavior to
        /// promote SpaceArtifacts to the front of the list. This was done so
        /// that we could replace a single instruction in ReceptacleSideScreen.Initialize()
        /// instead of the whole method.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="defaultBehavior"></param>
        public static void SortArtifactsFirst(this List<IHasSortOrder> list, Comparison<IHasSortOrder> defaultBehavior)
        {
            // Build a new comparison delegate that thunks the old one.
            Comparison<IHasSortOrder> betterCompare = (IHasSortOrder a, IHasSortOrder b) =>
            {
                // This comparison runs within the inner loop of the Sort, so let's
                // maximize intermediate variable reuse for speed.
                GameObject objA = (a as KMonoBehaviour).gameObject;
                GameObject objB = (b as KMonoBehaviour).gameObject;
                bool isArtifactA = objA.GetComponent<SpaceArtifact>() != null;
                bool isArtifactB = objB.GetComponent<SpaceArtifact>() != null;

                if (isArtifactA)
                {
                    if (isArtifactB)
                    {
                        // Both items are artifacts, compare inventory values.
                        float inventoryA = objA.GetMyWorld().worldInventory.GetAmount(objA.PrefabID(), includeRelatedWorlds: true);
                        float inventoryB = objB.GetMyWorld().worldInventory.GetAmount(objB.PrefabID(), includeRelatedWorlds: true);
                        return (int)(inventoryB - inventoryA);
                    }
                    else
                    {
                        // Only A is an artifact, it should come first.
                        return -1;
                    }
                }
                else
                {
                    if (isArtifactB)
                    {
                        // Only B is an artifact, it should come first.
                        return 1;
                    }
                    else
                    {
                        // Neither is an artifact.
                        return defaultBehavior(a, b);
                    }
                }
            };

            list.Sort(betterCompare);
        }
    }
}
