using BannerlordExpanded.CompanionExpanded.Settings;
using HarmonyLib;
using SandBox.CampaignBehaviors;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;

namespace BannerlordExpanded.CompanionExpanded.MoreFollowers.Patches
{
    [HarmonyPatchCategory("MoreFollowers")]
    [HarmonyPatch(typeof(ClanMemberRolesCampaignBehavior), "UpdateAccompanyingCharacters")]
    public static class MoreFollowersInTowns
    {
        [HarmonyPostfix]
        public static void Postfix(ClanMemberRolesCampaignBehavior __instance)
        {
            FieldInfo field = typeof(ClanMemberRolesCampaignBehavior).GetField("_isFollowingPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
            List<Hero> list = field.GetValue(__instance) as List<Hero>;

            foreach (TroopRosterElement troopRosterElement in MobileParty.MainParty.MemberRoster.GetTroopRoster())
            {
                if (list.Count >= MCMSettings.Instance.NumberOfFollowers)
                    break;
                if (troopRosterElement.Character.IsHero)
                {
                    Hero heroObject = troopRosterElement.Character.HeroObject;
                    if (heroObject != Hero.MainHero && !heroObject.IsPrisoner && !heroObject.IsWounded && heroObject.Age >= (float)Campaign.Current.Models.AgeModel.HeroComesOfAge && !list.Contains(heroObject))
                    {

                        list.Add(heroObject);

                    }
                }
            }
            field.SetValue(__instance, list);
        }
    }
}
