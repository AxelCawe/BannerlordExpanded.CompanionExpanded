using BannerlordExpanded.CompanionExpanded.Settings;
using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace BannerlordExpanded.CompanionExpanded.Behaviors
{
    public class CompanionRequestToJoinBehavior : CampaignBehaviorBase
    {
        CampaignTime _lastAsked = CampaignTime.Now;

        bool _menuOpened = false;

        public override void RegisterEvents()
        {
            CampaignEvents.SettlementEntered.AddNonSerializedListener(this, OnSettlementEnter);
            CampaignEvents.OnMissionStartedEvent.AddNonSerializedListener(this, OnMissionStarted);
        }

        public override void SyncData(IDataStore dataStore)
        {
            //throw new NotImplementedException();
            dataStore.SyncData("BECE_RequestToJoin", ref _lastAsked);

        }

        void OnMissionStarted(IMission mission)
        {
            if (_menuOpened)
            {
                if (InformationManager.IsAnyInquiryActive())
                {
                    _menuOpened = false;
                    InformationManager.HideInquiry();
                }
            }
        }

        void OnSettlementEnter(MobileParty party, Settlement settlement, Hero hero)
        {
            if (party != MobileParty.MainParty || hero != Hero.MainHero)
                return;


            if (ShouldMakeARequest())
            {
                MakeARequest();
            }
        }

        void MakeARequest()
        {
            _menuOpened = true;
            InformationManager.ShowInquiry(new InquiryData(new TextObject("{=BECE_Dialog_InviteRequest_MenuTitle}A person walks to you...").ToString(), new TextObject("{=BECE_Dialog_InviteRequest_MenuDesc}The person waves and signals for your attention. Do you want to respond?").ToString(), true, true, new TextObject("{=BECE_Dialog_InviteRequest_MenuYes}Yes").ToString(), new TextObject("{=BECE_Dialog_InviteRequest_MenuNo}No").ToString(), RequestAccepted, null));
        }

        void RequestAccepted()
        {
            var allWanderers = Enumerable.Where(Settlement.CurrentSettlement.HeroesWithoutParty, (Hero x) => x.IsWanderer && x.CompanionOf == null).ToList();
            Hero randomHero = allWanderers[new Random().Next(0, allWanderers.Count)];
            CampaignMapConversation.OpenConversation(new ConversationCharacterData(Hero.MainHero.CharacterObject), new ConversationCharacterData(randomHero.CharacterObject));
        }

        bool ShouldMakeARequest()
        {
            if (Settlement.CurrentSettlement == null)
                return false;
            var allWanderers = Enumerable.Where(Settlement.CurrentSettlement.HeroesWithoutParty, (Hero x) => x.IsWanderer && x.CompanionOf == null).ToList();
            if (allWanderers.Count == 0)
                return false;
            if (GameStateManager.Current == null)
                return false;
            if (_lastAsked.ElapsedDaysUntilNow >= MCMSettings.Instance.requestInterval)
            {
                int chance = MCMSettings.Instance.chanceForARequestPerClanTier * Clan.PlayerClan.Tier;
                return new Random().Next(0, 100) <= chance;
            }
            else return false;
        }
    }
}
