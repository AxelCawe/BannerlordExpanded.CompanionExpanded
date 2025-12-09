using BannerlordExpanded.CompanionExpanded.Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace BannerlordExpanded.CompanionExpanded.CompanionsToFamily.Behaviors
{
    public class CompanionToFamilyBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, AddDialogs);
        }

        public override void SyncData(IDataStore dataStore)
        {
            //throw new NotImplementedException();
        }

        void AddDialogs(CampaignGameStarter gameStarter)
        {
            gameStarter.AddPlayerLine("BECE_Dialog_companiontofamily_0_male", "BECE_Dialog_Start", "BECE_Dialog_companiontofamily_1", "{=BECE_Dialog_CompanionToFamily_Start_male}You have done well to be worthy of my family's name. I want you to be my son.", () => IsClanMemberAndNotFamily(Hero.OneToOneConversationHero) && !Hero.OneToOneConversationHero.IsFemale, null);
            gameStarter.AddPlayerLine("BECE_Dialog_companiontofamily_0_female", "BECE_Dialog_Start", "BECE_Dialog_companiontofamily_1", "{=BECE_Dialog_CompanionToFamily_Start_female}You have done well to be worthy of my family's name. I want you to be my daughter.", () => IsClanMemberAndNotFamily(Hero.OneToOneConversationHero) && Hero.OneToOneConversationHero.IsFemale, null);
            gameStarter.AddDialogLine("BECE_Dialog_companiontofamily_1", "BECE_Dialog_companiontofamily_1", "BECE_Dialog_companiontofamily_2", "{=BECE_Dialog_CompanionToFamily_AreYouSure}I am honored that you think so highly of me. But are you sure I can earn your family's name?", null, null);
            gameStarter.AddPlayerLine("BECE_Dialog_companiontofamily_2_yes", "BECE_Dialog_companiontofamily_2", "BECE_Dialog_companiontofamily_3", "{=BECE_Dialog_CompanionToFamily_AreYouSureReplyYes}Of course!", null, () => ConvertCompanionToFamily());
            gameStarter.AddPlayerLine("BECE_Dialog_companiontofamily_2_no", "BECE_Dialog_companiontofamily_2", "lord_pretalk", "{=BECE_Dialog_CompanionToFamily_AreYouSureReplyNo}I will need to rethink about it.", null, null);
            gameStarter.AddDialogLine("BECE_Dialog_companiontofamily_3", "BECE_Dialog_companiontofamily_3", "lord_pretalk", "{=BECE_Dialog_CompanionToFamily_End}I will gladly join your family!", null, null);


        }

        void ConvertCompanionToFamily()
        {
            Hero targetHero = Hero.OneToOneConversationHero;
            if (!Hero.MainHero.IsFemale)
                targetHero.Father = Hero.MainHero;
            else
                targetHero.Mother = Hero.MainHero;
            targetHero.SetNewOccupation(Occupation.Lord);
            RemoveCompanionAction.ApplyAfterQuest(Clan.PlayerClan, targetHero);
            targetHero.Clan = Clan.PlayerClan;
            MobileParty.MainParty.AddElementToMemberRoster(targetHero.CharacterObject, 1);
            TextObject announcement = new TextObject("{=BECE_Announcement_CompanionToFamily}{HERO_NAME} has been made a noble and is now part of your family!");
            announcement.SetTextVariable("HERO_NAME", Hero.OneToOneConversationHero.Name);
            MBInformationManager.AddQuickInformation(announcement, 0, null, null, "event:/ui/notification/relation");
        }


        bool IsClanMemberAndNotFamily(Hero hero)
        {
            return CompanionsHelper.IsClanMemberAndNotFamily(hero);
        }
    }
}
