using System.Linq;
using BattleTech;
using BattleTech.Data;
using MechSprayPaint.Framework;

namespace MechSprayPaint.Patches
{
    class CombatPatches
    {

        [HarmonyPatch(typeof(SimGameState), "RequestDataManagerResources")]

        public static class SimGameState_RequestDataManagerResources_Patch
        {
            public static void Postfix(SimGameState __instance)
            {
                LoadRequest loadRequest = __instance.DataManager.CreateLoadRequest();
                loadRequest.AddAllOfTypeBlindLoadRequest(BattleTechResourceType.ColorSwatch, new bool?(true));
                loadRequest.ProcessRequests(10U);
                Util.Initialize();
            }
        }

        [HarmonyPatch(typeof(AbstractActor), "InitEffectStats")]
        public static class AbstractActor_InitEffectStats_Patch
        {
            public static void Postfix(AbstractActor __instance)
            {
                __instance.StatCollection.AddStatistic<bool>("MSPainted", false);
            }
        }

        [HarmonyPatch(typeof(AttackDirector.AttackSequence), "OnAttackSequenceResolveDamage")]
        public static class AttackDirectorAttackSequence_OnAttackSequenceResolveDamage
        {
            public static void Postfix(AttackDirector.AttackSequence __instance)
            {
                if (__instance.chosenTarget is AbstractActor targetActor)
                {
                    var effects =
                        __instance.Director.Combat.EffectManager.GetAllEffectsTargeting(targetActor);
                    foreach (var effect in effects)
                    {
                        var paintInfo =
                            ModInit.modSettings.paintTypes.FirstOrDefault(x =>
                                x.effectID == effect.EffectData.Description.Id);
                        if (paintInfo != null)
                        {
                            ModInit.modLog.LogMessage($"Sometimes when you have a thing!");
                            ModInit.modLog.LogMessage($"The reason for the thing, is that you have it!");

                            var newHeraldry = Util.GetNewHeraldry(targetActor.team.HeraldryDef,
                                targetActor.Combat.DataManager, paintInfo);
                            var pilotableActorRepresentation = targetActor.GameRep as PilotableActorRepresentation;
                            if (pilotableActorRepresentation == null) return;
                            pilotableActorRepresentation.paintSchemeInitialized = false;
                            //Traverse.Create(pilotableActorRepresentation).Field("paintSchemeInitialized").SetValue(false);
                            pilotableActorRepresentation.InitPaintScheme(newHeraldry, targetActor.team.GUID);

                        }
                    }
                }
            }
        }
    }
}