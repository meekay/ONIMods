using System;
using KMod;

namespace Common
{
    /**
     * Based on Cairath's Logger.
     */
    public static class Logger
    {
        private static Mod _mod;

        public static void LogInit (Mod mod)
        {
            _mod = mod;
            Console.WriteLine($"{Timestamp()} Loaded [{mod?.title}] with version {mod?.packagedModInfo?.version}");
        }

        public static void Log (String message)
        {
            Console.WriteLine($"{Timestamp()} [{_mod?.title}] " + message);
        }

        public static string Timestamp() => System.DateTime.UtcNow.ToString("[HH:mm:ss.fff]");
    }
}
