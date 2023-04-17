using System;
using System.Collections.Generic;
using BattleTech;
using BattleTech.Data;
using Random = UnityEngine.Random;

namespace MechSprayPaint.Framework
{
    public class PainterInfo
    {
        public string effectID = "";
        public bool RandomColors = false;
        public string primaryMechColorID = "";
        public string secondaryMechColorID = "";
        public string tertiaryMechColorID = "";

    }
    class Util
    {
        public static void Initialize()
        {
            var dm = UnityGameInstance.BattleTechGame.DataManager;
            ModInit.modLog.LogMessage($"A different swatch for every day, why not?");
            foreach (var color in dm.ColorSwatches)
            {
                SwatchHolder.swatches.Add(color.Key);
                ModInit.modLog.LogMessage($"{color.Key}");
            }
        }

        public static string GetRandomSwatch()
        {
            var dm = UnityGameInstance.BattleTechGame.DataManager;
            if (SwatchHolder.swatches.Count == 0)
            {
                Initialize();
            }
            var idx = Random.Range(0, SwatchHolder.swatches.Count);
            ModInit.modLog.LogMessage($"Mmmm Mmm Mmm {SwatchHolder.swatches[idx]} is a lovely color");
            return SwatchHolder.swatches[idx];
        }

        public static HeraldryDef GetNewHeraldry(HeraldryDef def, DataManager dataManager, PainterInfo paint, Action loadCompleteCallback = null)
        { 
            var primaryID = "";
            var secondaryID = "";
            var tertiaryID = "";
            if (paint.RandomColors)
            {
                primaryID = Util.GetRandomSwatch();
                secondaryID = Util.GetRandomSwatch();
                tertiaryID = Util.GetRandomSwatch();
            }
            else
            {
                primaryID = paint.primaryMechColorID;
                secondaryID = paint.secondaryMechColorID;
                tertiaryID = paint.tertiaryMechColorID;
            }

            var newHeraldry = new HeraldryDef(def.Description, def.textureLogoID, primaryID, secondaryID, tertiaryID)
            {
                DataManager = dataManager
            };

            LoadRequest loadRequest = dataManager.CreateLoadRequest(delegate (LoadRequest request)
            {
                newHeraldry.Refresh();
                loadCompleteCallback?.Invoke();
            }, false);
            loadRequest.AddBlindLoadRequest(BattleTechResourceType.Texture2D, newHeraldry.textureLogoID, new bool?(false));
            loadRequest.AddBlindLoadRequest(BattleTechResourceType.Sprite, newHeraldry.textureLogoID, new bool?(false));
            loadRequest.AddBlindLoadRequest(BattleTechResourceType.ColorSwatch, newHeraldry.primaryMechColorID, new bool?(false));
            loadRequest.AddBlindLoadRequest(BattleTechResourceType.ColorSwatch, newHeraldry.secondaryMechColorID, new bool?(false));
            loadRequest.AddBlindLoadRequest(BattleTechResourceType.ColorSwatch, newHeraldry.tertiaryMechColorID, new bool?(false));
            loadRequest.ProcessRequests(10U);
            newHeraldry.Refresh();
            return newHeraldry;
        }

    }
    public static class SwatchHolder
    {
        public static List<string> swatches = new List<string>();
    }
}
