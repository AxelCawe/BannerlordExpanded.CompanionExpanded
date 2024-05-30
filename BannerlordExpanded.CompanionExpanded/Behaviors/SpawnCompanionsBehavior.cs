using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;
using TaleWorlds.LinQuick;
using MCM.Abstractions.Base.Global;
using TaleWorlds.Library;
using Helpers;
using System.Reflection;
using BannerlordExpanded.CompanionExpanded.Settings;
using TaleWorlds.CampaignSystem.CampaignBehaviors;

namespace BannerlordExpanded.CompanionExpanded.Behaviors
{
    internal class SpawnCompanionsBehavior : CampaignBehaviorBase
    {
        public SpawnCompanionsBehavior()
        {
            GlobalSettings<MCMSettings>.Instance.RefreshWanderers = this.RefreshCompanions;
            GlobalSettings<MCMSettings>.Instance.Cleanup = this.CleanUpDeadCompanions;
        }

        public override void RegisterEvents()
        {
            CampaignEvents.WeeklyTickEvent.AddNonSerializedListener(this, new Action(WeeklyTick));
        }

        public override void SyncData(IDataStore dataStore)
        {
            //throw new NotImplementedException();
        }

        private void WeeklyTick()
        {
            CleanUpDeadCompanions();
        }

        public void CleanUpDeadCompanions()
        {
            //InformationManager.DisplayMessage(new InformationMessage("Attempting cleanup"));
            foreach (Hero hero in Hero.DeadOrDisabledHeroes.ToList<Hero>())
            {
                if ((hero.Children == null || hero.Children.Count == 0) && hero.IsDead && ((hero.IsHeadman)))
                {
                    //InformationManager.DisplayMessage(new InformationMessage("Removing " + hero.Name + " from game."));
                    typeof(CampaignObjectManager).GetMethod("UnregisterDeadHero", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(Campaign.Current.CampaignObjectManager, new object[] {hero});
                }
            }
        }

        public void RefreshCompanions()
        {
            InformationManager.DisplayMessage(new InformationMessage("[Bannerlord Expanded]: Companions Refreshed"));
            foreach (Town townElement in Town.AllTowns)
            {

                Location targetLocation = townElement.Settlement.LocationComplex.GetLocationWithId("tavern");
                if (targetLocation == null)
                    continue;
                //InformationManager.DisplayMessage(new InformationMessage("Found tavern"));
                var allWanderers = Enumerable.Where<Hero>(townElement.Settlement.HeroesWithoutParty, (Hero x) => x.IsWanderer && x.CompanionOf == null).ToList();
                foreach (Hero heroElement in allWanderers)
                {
                    LeaveSettlementAction.ApplyForCharacterOnly(heroElement);
                    heroElement.SetNewOccupation(Occupation.Headman);
                    heroElement.AddDeathMark(null, KillCharacterAction.KillCharacterActionDetail.Lost);
                    heroElement.ChangeState(Hero.CharacterStates.Dead);
                    //InformationManager.DisplayMessage(new InformationMessage($"Killed{heroElement.Name}"));
                }
            }

            CleanUpDeadCompanions();
            CompanionsCampaignBehavior behavior = Campaign.Current.GetCampaignBehavior<CompanionsCampaignBehavior>();
            int companions = (int)((float)(typeof(CompanionsCampaignBehavior).GetMethod("get__desiredTotalCompanionCount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(behavior, null)));
            for (int i = 0; i < companions; ++i)
            {
                typeof(CompanionsCampaignBehavior).GetMethod("TrySpawnNewCompanion", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(behavior, null);
            }
            typeof(CompanionsCampaignBehavior).GetMethod("SwapCompanions", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(behavior, null);
        }

      
    }
}
