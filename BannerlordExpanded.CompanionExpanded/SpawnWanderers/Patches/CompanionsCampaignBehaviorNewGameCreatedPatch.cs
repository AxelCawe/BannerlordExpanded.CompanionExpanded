using HarmonyLib;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.LinQuick;

namespace BannerlordExpanded.CompanionExpanded.SpawnWanderers.Patches
{
    [HarmonyPatchCategory("CompanionSpawning")]
    [HarmonyPatch(typeof(CompanionsCampaignBehavior), "OnNewGameCreated")]
    public static class CompanionsCampaignBehaviorNewGameCreatedPatch
    {
        [HarmonyPrefix]
        static bool Prefix(CompanionsCampaignBehavior __instance)
        {
            typeof(CompanionsCampaignBehavior).GetMethod("InitializeCompanionTemplateList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(__instance, null);
            List<Town> list = Town.AllTowns.ToListQ();
            float desiredTotalCompanionCount = (float)typeof(CompanionsCampaignBehavior).GetMethod("get__desiredTotalCompanionCount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(__instance, null);
            int companionsPerSettlement = (int)(desiredTotalCompanionCount / list.Count);


            for (int i = 0; i < desiredTotalCompanionCount; ++i)
            {
                list.Shuffle();
                Town town = list[0];
                int counter = 0;
                while (town.Settlement.HeroesWithoutParty.WhereQ<Hero>((x) => x.IsWanderer && x.CompanionOf == null).ToListQ().Count >= companionsPerSettlement)
                {
                    if (counter > 10000)
                    {
                        return false;
                    }

                    ++counter;
                    list.Shuffle();
                    town = list[0];
                }

                typeof(CompanionsCampaignBehavior).GetMethod("CreateCompanionAndAddToSettlement", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(__instance, new object[] { town.Settlement });
                //InformationManager.DisplayMessage(new InformationMessage($"Added{desiredTotalCompanionCount}/{companionsPerSettlement}"));
            }

            return false;
        }
    }
}
