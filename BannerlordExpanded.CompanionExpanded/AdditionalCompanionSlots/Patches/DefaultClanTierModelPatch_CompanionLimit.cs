using BannerlordExpanded.CompanionExpanded.Settings;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;

namespace BannerlordExpanded.CompanionExpanded.AdditionalCompanionSlots.Patches
{
    [HarmonyPatchCategory("AdditionalCompanionSlots")]
    [HarmonyPatch(typeof(DefaultClanTierModel), "GetCompanionLimit")]
    public static class DefaultClanTierModelPatch_CompanionLimit
    {

        [HarmonyPostfix]
        static void Postfix(ref int __result, Clan clan)
        {
            __result += clan.Tier * MCMSettings.Instance.AdditionalCompanionsPerClanTier;
        }
    }
}
