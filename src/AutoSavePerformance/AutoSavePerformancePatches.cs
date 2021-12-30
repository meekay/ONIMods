using HarmonyLib;
using System.Diagnostics;
using System.Collections.Generic;
using static Common.Logger;
using System;

namespace AutoSavePerformance
{
    public static class AutoSavePerformancePatches
    {
        [HarmonyPatch(typeof(GameClock), "DoAutoSave")]
        public static class GameClock_DoAutoSave_Patch
        {
            static Stopwatch time;

            [HarmonyPrefix]
            public static void Prefix(GameClock __instance, int day)
            {
                time = Stopwatch.StartNew();
            }

            [HarmonyPostfix]
            public static void Postfix(GameClock __instance, int day)
            {
                long duration = time.ElapsedMilliseconds;
                Log($"GameClock.DoAutoSave completed in {duration}ms, day = {day}");
            }
        }


        [HarmonyPatch(typeof(KleiMetrics), "SendEvent")]
        public static class KleiMetrics_SendEvent_Patch
        {
            static Stopwatch time;

            [HarmonyPrefix]
            public static void Prefix(KleiMetrics __instance, Dictionary<string, object> eventData, string debug_event_name)
            {
                time = Stopwatch.StartNew();
            }

            [HarmonyPostfix]
            public static void Postfix(KleiMetrics __instance, Dictionary<string, object> eventData, string debug_event_name)
            {
                long duration = time.ElapsedMilliseconds;
                Log($"KleiMetrics.SendEvent completed in {duration}ms");
            }
        }

        [HarmonyPatch(typeof(SaveLoader), "Save")]
        [HarmonyPatch(new Type[] { typeof(string), typeof(bool), typeof(bool) })]
        public static class SaveLoader_Save_Patch
        {
            static Stopwatch time;

            [HarmonyPrefix]
            public static void Prefix(SaveLoader __instance, string filename, bool isAutoSave, bool updateSavePointer)
            {
                time = Stopwatch.StartNew();
            }

            [HarmonyPostfix]
            public static void Postfix(SaveLoader __instance, string filename, bool isAutoSave, bool updateSavePointer)
            {
                long duration = time.ElapsedMilliseconds;
                Log($"SaveLoader.Save completed in {duration}ms, isAutoSave = {isAutoSave}");
            }
        }
    }
}
