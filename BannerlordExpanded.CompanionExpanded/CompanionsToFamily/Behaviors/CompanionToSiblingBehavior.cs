using BannerlordExpanded.CompanionExpanded.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace BannerlordExpanded.CompanionExpanded.CompanionsToFamily.Behaviors
{
    public class CompanionToSiblingBehavior : CampaignBehaviorBase
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
            gameStarter.AddPlayerLine("BECE_Dialog_companiontosibling_0_male", "BECE_Dialog_Start", "BECE_Dialog_companiontosibling_1", "{=BECE_Dialog_CompanionToSibling_Start_male}You have done well to be worthy of my family's name. I want you to be my brother.", () => IsClanMemberAndNotSibling(Hero.OneToOneConversationHero) && !Hero.OneToOneConversationHero.IsFemale, null);
            gameStarter.AddPlayerLine("BECE_Dialog_companiontosibling_0_female", "BECE_Dialog_Start", "BECE_Dialog_companiontosibling_1", "{=BECE_Dialog_CompanionToSibling_Start_female}You have done well to be worthy of my family's name. I want you to be my sister.", () => IsClanMemberAndNotSibling(Hero.OneToOneConversationHero) && Hero.OneToOneConversationHero.IsFemale, null);
            gameStarter.AddDialogLine("BECE_Dialog_companiontosibling_1", "BECE_Dialog_companiontosibling_1", "BECE_Dialog_companiontosibling_2", "{=BECE_Dialog_CompanionToSibling_AreYouSure}I am honored that you think so highly of me. But are you sure I can earn your family's name?", null, null);
            gameStarter.AddPlayerLine("BECE_Dialog_companiontosibling_2_yes", "BECE_Dialog_companiontosibling_2", "BECE_Dialog_companiontosibling_3", "{=BECE_Dialog_CompanionToSibling_AreYouSureReplyYes}Of course!", null, () => ConvertCompanionToSibling());
            gameStarter.AddPlayerLine("BECE_Dialog_companiontosibling_2_no", "BECE_Dialog_companiontosibling_2", "lord_pretalk", "{=BECE_Dialog_CompanionToSibling_AreYouSureReplyNo}I will need to rethink about it.", null, null);
            gameStarter.AddDialogLine("BECE_Dialog_companiontosibling_3", "BECE_Dialog_companiontosibling_3", "lord_pretalk", "{=BECE_Dialog_CompanionToSibling_End}I will gladly join your family!", null, null);


        }

        void ConvertCompanionToSibling()
        {
            Hero targetHero = Hero.OneToOneConversationHero;
            if (Hero.MainHero.Father != null)
                targetHero.Father = Hero.MainHero.Father;
            if (Hero.MainHero.Mother != null)
                targetHero.Mother = Hero.MainHero.Mother;

            targetHero.SetNewOccupation(Occupation.Lord);
            RemoveCompanionAction.ApplyAfterQuest(Clan.PlayerClan, targetHero);
            targetHero.Clan = Clan.PlayerClan;
            MobileParty.MainParty.AddElementToMemberRoster(targetHero.CharacterObject, 1);
            TextObject announcement = new TextObject("{=BECE_Announcement_CompanionToSibling}{HERO_NAME} has been made a noble and is now part of your family!");
            announcement.SetTextVariable("HERO_NAME", Hero.OneToOneConversationHero.Name);
            MBInformationManager.AddQuickInformation(announcement, 0, null, "event:/ui/notification/relation");
        }


        bool IsClanMemberAndNotSibling(Hero hero)
        {
            return CompanionsHelper.IsClanMemberAndNotFamily(hero);
        }
    }
}
