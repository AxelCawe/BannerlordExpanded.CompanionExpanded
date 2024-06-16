using BannerlordExpanded.CompanionExpanded.Helpers;
using HarmonyLib;
using SandBox.CampaignBehaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;

namespace BannerlordExpanded.CompanionExpanded.WanderersInTown.Patches
{
    [HarmonyPatchCategory("CompanionInTown")]
    [HarmonyPatch(typeof(CommonTownsfolkCampaignBehavior), "AddPeopleToTownCenter")]
    public static class CompanionsInTownPatch
    {
        [HarmonyPostfix]
        static void Postfix(Settlement settlement, Dictionary<string, int> unusedUsablePointCount, bool isDayTime)
        {
            if (!settlement.IsTown)
                return;

            Location locationWithId = settlement.LocationComplex.GetLocationWithId("center");
            foreach (var character in settlement.HeroesWithoutParty)
            {
                if (character.IsWanderer && character.CompanionOf == null && character.DeathMark != KillCharacterAction.KillCharacterActionDetail.Lost)
                {
                    locationWithId.AddCharacter(CompanionsInTownHelper.CreateWandererLocationCharacter(character));

                }
            }
        }
    }
}
