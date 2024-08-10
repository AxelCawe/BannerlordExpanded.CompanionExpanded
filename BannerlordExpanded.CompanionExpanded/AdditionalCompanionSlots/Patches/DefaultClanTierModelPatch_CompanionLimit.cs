using BannerlordExpanded.CompanionExpanded.Settings;
using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;

namespace BannerlordExpanded.CompanionExpanded.AdditionalCompanionSlots.Patches
{
    [HarmonyPatchCategory("AdditionalCompanionSlots")]
    [HarmonyPatch(typeof(DefaultClanTierModel), "GetCompanionLimitFromTier")]
    public static class DefaultClanTierModelPatch_CompanionLimit
    {

        [HarmonyPostfix]
        static void Postfix(ref int __result, int clanTier)
        {
            __result += clanTier * MCMSettings.Instance.AdditionalCompanionsPerClanTier;
        }
    }
}
