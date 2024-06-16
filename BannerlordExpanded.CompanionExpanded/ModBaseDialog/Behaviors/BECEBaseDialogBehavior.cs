using BannerlordExpanded.CompanionExpanded.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace BannerlordExpanded.CompanionExpanded.ModBaseDialog.Behaviors
{
    public class BECEBaseDialogBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            //throw new NotImplementedException();
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, AddDialogs);
        }

        public override void SyncData(IDataStore dataStore)
        {
            //throw new NotImplementedException();
        }

        private void AddDialogs(CampaignGameStarter campaignGameStarter)
        {
            campaignGameStarter.AddPlayerLine("BECE_Dialog", "hero_main_options", "BECE_Dialog", "{=BECE_Dialog}I have something to talk you about.", new ConversationSentence.OnConditionDelegate(IsHeroPartOfClan), null);
            campaignGameStarter.AddDialogLine("BECE_Dialog_Start", "BECE_Dialog", "BECE_Dialog_Start", "{=BECE_Dialog_Start}What is it?", new ConversationSentence.OnConditionDelegate(IsHeroPartOfClan), null);
            campaignGameStarter.AddPlayerLine("BECE_Dialog_Cancel", "BECE_Dialog_Start", "lord_pretalk", "{=BECE_Dialog_Back}Nevermind.", null, null);
        }

        private bool IsHeroPartOfClan()
        {
            try
            {
                return CompanionsHelper.IsClanMember(Hero.OneToOneConversationHero);
            }
            catch
            {
                return false;
            }
        }
    }
}
