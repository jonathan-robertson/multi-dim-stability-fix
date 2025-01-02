using HarmonyLib;
using MultiDimStabilityFix.Utilities;
using System;
using System.Reflection;

namespace MultiDimStabilityFix
{
    internal class ModApi : IModApi
    {
        private const string ModMaintainer = "kanaverum";
        private const string SupportLink = "https://discord.gg/hYa2sNHXya";

        private static readonly ModLog<ModApi> _log = new ModLog<ModApi>();
        public static bool DebugMode { get; set; } = false;

        public void InitMod(Mod _modInstance)
        {
            try
            {
                new Harmony(GetType().ToString()).PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception e)
            {
                _log.Error($"Failed to start up Off World Fix mod; take a look at logs for guidance but feel free to also reach out to the mod maintainer {ModMaintainer} via {SupportLink}", e);
            }
        }
    }
}
