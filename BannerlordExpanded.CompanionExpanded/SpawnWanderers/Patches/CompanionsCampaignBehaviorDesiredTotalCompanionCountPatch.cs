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

namespace BannerlordExpanded.CompanionExpanded.SpawnWanderers.Patches
{
    [HarmonyPatchCategory("CompanionSpawning")]
    [HarmonyPatch(typeof(CompanionsCampaignBehavior), "get__desiredTotalCompanionCount")]
    public static class CompanionsCampaignBehaviorDesiredTotalCompanionCountPatch
    {
        [HarmonyPostfix]
        static void Postfix(ref float __result)
        {
            __result = MCMSettings.Instance.maxCompanionsPerTown * Town.AllTowns.Count;
        }
    }
}
