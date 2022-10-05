using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using static Common.Logger;

namespace SortArtifactsFirst
{
    public class SortArtifactsFirstPatches
    {
        /// <summary>
        /// The ReceptacleSideScreen is invoked when the user selects an
        /// ItemPedestal.
        /// </summary>
        [HarmonyPatch(typeof(ReceptacleSideScreen), "Initialize")]
        public static class ReceptacleSideScreen_Initialize_Patch
        {
            /// <summary>
            /// Inspects the code for ReceptacleSideScreen.Initialize() and replaces
            /// the single instruction that calls List.Sort() with one provided
            /// by us instead.
            /// </summary>
            /// <param name="instructions"></param>
            /// <returns></returns>
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                MethodInfo sortMethod = typeof(List<IHasSortOrder>).GetMethod("Sort", new Type[]{ typeof(Comparison<IHasSortOrder>) });

                Log("Beginning transpilation");
                foreach (var oldInstr in instructions)
                {
                    if (CodeInstructionExtensions.Calls(oldInstr, sortMethod))
                    {
                        Log(oldInstr.ToString());
                        CodeInstruction newInstr = CodeInstruction.Call(typeof(ListExtensions), nameof(ListExtensions.SortArtifactsFirst))
                            .MoveBlocksFrom(oldInstr)
                            .MoveLabelsFrom(oldInstr);
                        Log(newInstr.ToString());
                        yield return newInstr;
                    } else
                    {
                        yield return oldInstr;
                    }
                }

                Log("Done with transpilation");
            }
        }
    }
}
