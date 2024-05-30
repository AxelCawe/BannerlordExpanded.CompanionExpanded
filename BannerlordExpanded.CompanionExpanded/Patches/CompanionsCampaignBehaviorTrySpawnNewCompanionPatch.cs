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
using TaleWorlds.Library;
using TaleWorlds.CampaignSystem;

namespace BannerlordExpanded.CompanionExpanded.Patches
{
    [HarmonyPatchCategory("CompanionSpawning")]
    [HarmonyPatch(typeof(CompanionsCampaignBehavior), "TrySpawnNewCompanion")]
    public static class CompanionsCampaignBehaviorTrySpawnNewCompanionPatch
    {
        [HarmonyPostfix]
        static void Postfix(CompanionsCampaignBehavior __instance)
        {
            MBReadOnlyList<Town> allTowns = Town.AllTowns;
            int aimCompanionsPerTown = (int)((float)(typeof(CompanionsCampaignBehavior).GetMethod("get__desiredTotalCompanionCount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(__instance, null)) / allTowns.Count);
            foreach (Town town in allTowns)
            {
                int wanderers = Enumerable.Where<Hero>(town.Settlement.HeroesWithoutParty, (Hero x) => x.IsWanderer && x.CompanionOf == null).ToList().Count();
                for (int i = 0; i < aimCompanionsPerTown - wanderers; ++i)
                {
                    typeof(CompanionsCampaignBehavior).GetMethod("CreateCompanionAndAddToSettlement", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(__instance, new object[] { town.Settlement });
                }
            }
        }
    }
}
