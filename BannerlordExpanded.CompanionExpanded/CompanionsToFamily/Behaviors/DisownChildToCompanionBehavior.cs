using BannerlordExpanded.CompanionExpanded.Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;

namespace BannerlordExpanded.CompanionExpanded.CompanionsToFamily.Behaviors
{
    internal class DisownChildToCompanionBehavior : CampaignBehaviorBase
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

        void AddDialogs(CampaignGameStarter gameStarter)
        {
            gameStarter.AddPlayerLine("BECE_Dialog_disownchildtocompanion_0", "BECE_Dialog_Start", "BECE_Dialog_disownchildtocompanion_1", "{=BECE_Dialog_DisownChildToCompanion_Start}You are no longer worthy of the family's name!", () => IsClanMemberAndRelatedChild(), null);
            gameStarter.AddDialogLine("BECE_Dialog_disownchildtocompanion_1", "BECE_Dialog_disownchildtocompanion_1", "BECE_Dialog_disownchildtocompanion_2", "{=BECE_Dialog_CompanionToFamily_Reply}W-what? I ask that you reconsider milord!", null, null);
            gameStarter.AddPlayerLine("BECE_Dialog_disownchildtocompanion_2_yes", "BECE_Dialog_disownchildtocompanion_2", "BECE_Dialog_disownchildtocompanion_3", "{=BECE_Dialog_DisownChildToCompanion_AreYouSureYes}I have already thought this through. You are not worthy to be my child!", null, () => DisownChildToCompanion());
            gameStarter.AddPlayerLine("BECE_Dialog_disownchildtocompanion_2_yes", "BECE_Dialog_disownchildtocompanion_2", "lord_pretalk", "{=BECE_Dialog_DisownChildToCompanion_AreYouSureNo}Calm down, I was only joking!", null, null);
            gameStarter.AddDialogLine("BECE_Dialog_disownchildtocompanion_3", "BECE_Dialog_disownchildtocompanion_3", "lord_pretalk", "{=BECE_Dialog_DisownChildToCompanion_End}I see...", null, null);

        }

        void DisownChildToCompanion()
        {
            Hero targetHero = Hero.OneToOneConversationHero;
            if (targetHero.Father != null)
            {
                if (targetHero.Father.Children.Contains(targetHero))
                    targetHero.Father.Children.Remove(targetHero);
                targetHero.Father = null;
            }
            if (targetHero.Mother != null)
            {
                if (targetHero.Mother.Children.Contains(targetHero))
                    targetHero.Mother.Children.Remove(targetHero);
                targetHero.Mother = null;
            }
            targetHero.SetNewOccupation(Occupation.Wanderer);
            //targetHero.CompanionOf = null;
            targetHero.Clan = null;
            AddCompanionAction.Apply(Clan.PlayerClan, targetHero);
            //typeof(Hero).GetField("_clan", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(targetHero, null);
        }

        bool IsClanMemberAndRelatedChild()
        {
            if (CompanionsHelper.IsClanMemberAndFamily(Hero.OneToOneConversationHero))
            {
                return Hero.MainHero.Children.Contains(Hero.OneToOneConversationHero);
            }
            else
                return false;
        }
    }
}
