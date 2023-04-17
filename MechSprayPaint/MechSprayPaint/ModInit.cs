using System;
using System.Collections.Generic;
using System.Reflection;
using MechSprayPaint.Framework;
using Newtonsoft.Json;

namespace MechSprayPaint
{
    public static class ModInit
    {
        internal static Logger modLog;
        internal static string modDir;
        internal static Settings modSettings;
        public static readonly Random Random = new Random();
        public const string HarmonyPackage = "us.tbone.MechSprayPaint";
        public static void Init(string directory, string settingsJSON)
        {
            modDir = directory;
            modLog = new Logger(modDir, "MechSprayPaint", true);
            try
            {
                ModInit.modSettings = JsonConvert.DeserializeObject<Settings>(settingsJSON);

            }
            catch (Exception ex)
            {
                ModInit.modLog.LogException(ex);
                ModInit.modSettings = new Settings();
            }

            ModInit.modLog.LogMessage($"Initializing {HarmonyPackage} - Version {typeof(Settings).Assembly.GetName().Version}");
            //var harmony = HarmonyInstance.Create(HarmonyPackage);
            //harmony.PatchAll(Assembly.GetExecutingAssembly());
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), HarmonyPackage);
        }
    }
    class Settings
    {
        public List<PainterInfo> paintTypes = new List<PainterInfo>();

    }
}