
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.GameComponents;
using HarmonyLib.BUTR.Extensions;
using HarmonyLib;

namespace BannerlordExpanded.CompanionExpanded.Helpers
{
    public class CompanionsHelper
    {
        public static bool IsClanMember(Hero hero)
        {
            if (Clan.PlayerClan != null)
            {
                return Clan.PlayerClan.Heroes.Contains(hero);
            }
            else return false;
        }


        public static bool IsClanMemberAndNotFamily(Hero hero)
        {
            return Clan.PlayerClan.Heroes.Contains(hero) && hero.Occupation == Occupation.Wanderer;

        }

        public static bool IsClanMemberAndFamily(Hero hero)
        {
            return Clan.PlayerClan.Heroes.Contains(hero) && hero.Occupation == Occupation.Lord;

        }
   
        private static IEnumerable<Hero> DiscoverAncestors(Hero hero, int n)
        {
            if (hero != null)
            {
                yield return hero;
                if (n > 0)
                {
                    foreach (Hero hero2 in DiscoverAncestors(hero.Mother, n - 1))
                    {
                        yield return hero2;
                    }
                    IEnumerator<Hero> enumerator = null;
                    foreach (Hero hero3 in DiscoverAncestors(hero.Father, n - 1))
                    {
                        yield return hero3;
                    }
                    enumerator = null;
                }
            }
            yield break;
        }
    }
}
