using BannerlordExpanded.CompanionExpanded.Settings;
using HarmonyLib;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace BannerlordExpanded.CompanionExpanded.SpawnWanderers.Patches
{
    [HarmonyPatchCategory("CompanionSpawning")]
    [HarmonyPatch(typeof(CompanionsCampaignBehavior), "GetCompanionTemplateToSpawn")]
    public static class CompanionsCampaignBehaviorGetCompanionTemplateToSpawnPatch

    {

        static Random random = new Random();

        [HarmonyPostfix]
        static void Postfix(CompanionsCampaignBehavior __instance, ref CharacterObject __result)
        {
            if (__result == null && !MCMSettings.Instance.UniqueWandererSpawning)
            {
                List<CharacterObject> allCompanionTemplates = new List<CharacterObject>();
                foreach (CultureObject cultureObject in MBObjectManager.Instance.GetObjectTypeList<CultureObject>())
                {
                    foreach (CharacterObject characterObject in cultureObject.NotableAndWandererTemplates)
                    {
                        if (characterObject.Occupation == Occupation.Wanderer)
                            allCompanionTemplates.Add(characterObject);
                    }
                }

                allCompanionTemplates.Randomize();
                __result = allCompanionTemplates[random.Next(allCompanionTemplates.Count)];

            }


        }
    }
}
