using HarmonyLib;
using SandBox.CampaignBehaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.Settlements;
using BannerlordExpanded.CompanionExpanded.Settings;
using TaleWorlds.Core;
using TaleWorlds.LinQuick;
using TaleWorlds.Library;
using TaleWorlds.CampaignSystem;
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
            if (__result == null)
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
