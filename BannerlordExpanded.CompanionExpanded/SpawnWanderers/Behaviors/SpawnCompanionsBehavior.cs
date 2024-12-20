using BannerlordExpanded.CompanionExpanded.Settings;
using MCM.Abstractions.Base.Global;
using System;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Library;

namespace BannerlordExpanded.CompanionExpanded.SpawnWanderers.Behaviors
{
    internal class SpawnCompanionsBehavior : CampaignBehaviorBase
    {
        public SpawnCompanionsBehavior()
        {
            GlobalSettings<MCMSettings>.Instance.RefreshWanderers = RefreshCompanions;
            GlobalSettings<MCMSettings>.Instance.Cleanup = CleanUpDeadCompanions;
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
            if (MCMSettings.Instance.AutoRefreshWanderersEveryWeek)
            {
                RefreshCompanions();
            }
            else
                CleanUpDeadCompanions();
        }

        public void CleanUpDeadCompanions()
        {
            //InformationManager.DisplayMessage(new InformationMessage("Attempting cleanup"));
            foreach (Hero hero in Hero.DeadOrDisabledHeroes.ToList<Hero>())
            {
                if ((hero.Children == null || hero.Children.Count == 0) && hero.IsDead && hero.IsHeadman)
                {
                    //InformationManager.DisplayMessage(new InformationMessage("Removing " + hero.Name + " from game."));
                    typeof(CampaignObjectManager).GetMethod("UnregisterDeadHero", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(Campaign.Current.CampaignObjectManager, new object[] { hero });
                }
            }
        }

        public void RefreshCompanions()
        {
            CompanionsCampaignBehavior behavior = Campaign.Current.GetCampaignBehavior<CompanionsCampaignBehavior>();
            InformationManager.DisplayMessage(new InformationMessage("[Bannerlord Expanded]: Companions Refreshed"));
            foreach (Town townElement in Town.AllTowns)
            {
                Location targetLocation = townElement.Settlement.LocationComplex.GetLocationWithId("tavern");
                if (targetLocation == null)
                    continue;
                //InformationManager.DisplayMessage(new InformationMessage("Found tavern"));
                var allWanderers = townElement.Settlement.HeroesWithoutParty.Where<Hero>((x) => x.IsWanderer && x.CompanionOf == null).ToList();
                foreach (Hero heroElement in allWanderers)
                {
                    LeaveSettlementAction.ApplyForCharacterOnly(heroElement);
                    //heroElement.SetNewOccupation(Occupation.Headman);
                    heroElement.AddDeathMark(null, KillCharacterAction.KillCharacterActionDetail.Lost);
                    heroElement.ChangeState(Hero.CharacterStates.Dead);
                    typeof(CompanionsCampaignBehavior).GetMethod("RemoveFromAliveCompanions", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(behavior, new object[] { heroElement });
                    //InformationManager.DisplayMessage(new InformationMessage($"Killed{heroElement.Name}"));
                }
            }

            CleanUpDeadCompanions();

            int companions = (int)(float)typeof(CompanionsCampaignBehavior).GetMethod("get__desiredTotalCompanionCount", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(behavior, null);
            for (int i = 0; i < companions; ++i)
            {
                typeof(CompanionsCampaignBehavior).GetMethod("TrySpawnNewCompanion", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(behavior, null);
            }
            typeof(CompanionsCampaignBehavior).GetMethod("SwapCompanions", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(behavior, null);
        }


    }
}
