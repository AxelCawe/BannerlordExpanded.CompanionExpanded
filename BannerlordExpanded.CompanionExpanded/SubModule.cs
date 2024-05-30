using BannerlordExpanded.CompanionExpanded.Behavior;
using BannerlordExpanded.CompanionExpanded.Behaviors;
using BannerlordExpanded.CompanionExpanded.Patches;
using BannerlordExpanded.CompanionExpanded.Settings;
using HarmonyLib;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;


namespace BannerlordExpanded.CompanionExpanded
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();

        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            Harmony harmony = new Harmony("BannerlordExpanded.CompanionExpanded");
            if (MCMSettings.Instance.CompanionSpawningActive)
                harmony.PatchCategory(Assembly.GetExecutingAssembly(), "CompanionSpawning");
            if (MCMSettings.Instance.GiveGiftsActive)
                harmony.PatchCategory(Assembly.GetExecutingAssembly(), "GiveGifts");
            if (MCMSettings.Instance.CompanionsInTown)
                harmony.PatchCategory(Assembly.GetExecutingAssembly(), "CompanionInTown");
            harmony.PatchAllUncategorized(Assembly.GetExecutingAssembly());
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            base.OnGameStart(game, gameStarter);
            AddBehaviors(gameStarter as CampaignGameStarter);
        }

        private void AddBehaviors(CampaignGameStarter gameStarter)
        {
            //gameStarter.AddBehavior(new SaveDataBehaviour());
            if (gameStarter != null)
            {
                if (MCMSettings.Instance.CompanionSpawningActive)
                    gameStarter.AddBehavior(new SpawnCompanionsBehavior());
                
                gameStarter.AddBehavior(new BECEBaseDialogBehavior());
                if (MCMSettings.Instance.WandererRequestToJoin)
                    gameStarter.AddBehavior(new CompanionRequestToJoinBehavior());
                if (MCMSettings.Instance.CompanionToChildActive)
                    gameStarter.AddBehavior(new CompanionToFamilyBehavior());
                if (MCMSettings.Instance.CompanionToSiblingActive)    
                    gameStarter.AddBehavior(new CompanionToSiblingBehavior());
                if (MCMSettings.Instance.DisownChildrenActive)
                    gameStarter.AddBehavior(new DisownChildToCompanionBehavior());
                if (MCMSettings.Instance.GiveGiftsActive)
                    gameStarter.AddBehavior(new GiveGiftsBehavior());
            }
        }
    }
}