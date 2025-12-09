using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Siege;

namespace BannerlordExpanded.SpousesExpanded.PregnancyAge.Patches
{

    [HarmonyPatch(typeof(DefaultTroopSacrificeModel), "GetLostTroopCount", typeof(MobileParty), typeof(SiegeEvent), typeof(bool))]
    public static class TroopLostCountPatch
    {
        [HarmonyPostfix]
        public static void Postfix(DefaultTroopSacrificeModel __instance, MobileParty party, ref ExplainedNumber __result)
        {
            if (__result.BaseNumber > 0 && party.MemberRoster.TotalManCount > __result.BaseNumber && party.MemberRoster.TotalRegulars < __result.BaseNumber)
            {
                __result = new ExplainedNumber(party.MemberRoster.TotalRegulars, false, null);
            }
        }


    }
}
