using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Siege;

namespace BannerlordExpanded.SpousesExpanded.PregnancyAge.Patches
{

    [HarmonyPatch(typeof(DefaultTroopSacrificeModel), "GetLostTroopCount", typeof(MobileParty), typeof(SiegeEvent))]
    public static class TroopLostCountPatch
    {
        [HarmonyPostfix]
        public static void Postfix(DefaultTroopSacrificeModel __instance, MobileParty party, ref int __result)
        {
            if (__result > 0 && party.MemberRoster.TotalManCount > __result && party.MemberRoster.TotalRegulars < __result)
            {
                __result = party.MemberRoster.TotalRegulars;
            }
        }


    }
}
