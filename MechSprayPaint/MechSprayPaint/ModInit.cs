using System;
using System.Collections.Generic;
using System.Reflection;
using IRBTModUtils.Logging;
using MechSprayPaint.Framework;
using Newtonsoft.Json;

namespace MechSprayPaint
{
    public static class ModInit
    {
        //public static Logger modLog;
        public static DeferringLogger modLog;
        public static string modDir;
        public static Settings modSettings;
        public static readonly Random Random = new Random();
        public const string HarmonyPackage = "us.tbone.MechSprayPaint";
        public static void Init(string directory, string settingsJSON)
        {
            modDir = directory;
            modLog = new DeferringLogger(modDir, "MechSprayPaint", false, false);
            try
            {
                ModInit.modSettings = JsonConvert.DeserializeObject<Settings>(settingsJSON);

            }
            catch (Exception ex)
            {
                ModInit.modLog?.Error?.Write(ex);
                ModInit.modSettings = new Settings();
            }

            ModInit.modLog?.Info?.Write($"Initializing {HarmonyPackage} - Version {typeof(Settings).Assembly.GetName().Version}");
            //var harmony = HarmonyInstance.Create(HarmonyPackage);
            //harmony.PatchAll(Assembly.GetExecutingAssembly());
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), HarmonyPackage);
        }
    }
    public class Settings
    {
        public List<PainterInfo> paintTypes = new List<PainterInfo>();

    }
}