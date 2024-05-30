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

namespace BannerlordExpanded.CompanionExpanded.Patches
{
    [HarmonyPatchCategory("CompanionSpawning")]
    [HarmonyPatch(typeof(CompanionsCampaignBehavior), "OnNewGameCreated")]
    public static class CompanionsCampaignBehaviorNewGameCreatedPatch
    {
        [HarmonyPrefix]
        static bool Prefix(CompanionsCampaignBehavior __instance)
        {
            typeof(CompanionsCampaignBehavior).GetMethod("InitializeCompanionTemplateList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(__instance, null);
            List<Town> list = Town.AllTowns.ToListQ<Town>();
            list.Shuffle<Town>();
            float desiredTotalCompanionCount = (float)(typeof(CompanionsCampaignBehavior).GetMethod("get__desiredTotalCompanionCount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(__instance, null));
            int companionsPerSettlement = (int)(desiredTotalCompanionCount / list.Count);
            
            foreach (Town town in list) 
            {
                for (int i = 0; i < companionsPerSettlement; ++i)
                {
                    typeof(CompanionsCampaignBehavior).GetMethod("CreateCompanionAndAddToSettlement", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(__instance, new object[] { town.Settlement });
                    //InformationManager.DisplayMessage(new InformationMessage($"Added{desiredTotalCompanionCount}/{companionsPerSettlement}"));
                }
                
            }
            return false;
        }
    }
}
