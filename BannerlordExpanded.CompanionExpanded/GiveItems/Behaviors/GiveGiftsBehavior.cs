using BannerlordExpanded.CompanionExpanded.Settings;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.BarterSystem;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace BannerlordExpanded.CompanionExpanded.GiveItems.Behaviors
{
    public class GiveGiftsBehavior : CampaignBehaviorBase
    {
        public MBReadOnlyDictionary<Hero, CampaignTime> GiftsCooldown { get { return _giftsCooldown.GetReadOnlyDictionary(); } }
        private Dictionary<Hero, CampaignTime> _giftsCooldown;

        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, AddDialogs);
        }

        public override void SyncData(IDataStore dataStore)
        {
            //throw new NotImplementedException();
            if (dataStore.IsSaving)
            {
                dataStore.SyncData("BECE_GiveGifts", ref _giftsCooldown);
            }
            else if (dataStore.IsLoading)
            {
                _giftsCooldown = new Dictionary<Hero, CampaignTime>();
                dataStore.SyncData("BECE_GiveGifts", ref _giftsCooldown);
            }
        }

        private void AddDialogs(CampaignGameStarter campaignGameStarter)
        {
            campaignGameStarter.AddPlayerLine("BECE_Dialog_givegifts_0", "BECE_Dialog_Start", "BECE_Dialog_givegifts_1", "{=BECE_Dialog_Gift}I have prepared a gift for you!", null, null, 100, new ConversationSentence.OnClickableConditionDelegate(CanGiftToHero));
            campaignGameStarter.AddDialogLine("BECE_Dialog_givegifts_1", "BECE_Dialog_givegifts_1", "BECE_Dialog_givegifts_2", "{=BECE_Dialog_GiftResponse}Oh? What is it?", null, OpenTradeBarter);
            campaignGameStarter.AddDialogLine("BECE_Dialog_givegifts_2_success", "BECE_Dialog_givegifts_2", "lord_pretalk", "{=BECE_Dialog_GiftResponseSuccessful}Thank you!", () => TradeBarterSuccess(), null);
            campaignGameStarter.AddDialogLine("BECE_Dialog_givegifts_2_failure", "BECE_Dialog_givegifts_2", "lord_pretalk", "{=BECE_Dialog_GiftResponseFailure}There was nothing...", () => !TradeBarterSuccess(), null);
        }

        private bool TradeBarterSuccess()
        {
            bool success = Campaign.Current.BarterManager.LastBarterIsAccepted;
            if (success)
            {
                Hero targetHero = Hero.OneToOneConversationHero;
                if (GiftsCooldown.ContainsKey(targetHero))
                    _giftsCooldown[targetHero] = CampaignTime.Now;
                else
                    _giftsCooldown.Add(targetHero, CampaignTime.Now);
            }
            return success;
        }

        private bool CanGiftToHero(out TextObject reply)
        {
            Hero targetHero = Hero.OneToOneConversationHero;
            if (_giftsCooldown == null)
                _giftsCooldown = new Dictionary<Hero, CampaignTime>();
            if (GiftsCooldown.ContainsKey(targetHero))
            {
                if (GiftsCooldown[targetHero].ElapsedDaysUntilNow >= MCMSettings.Instance.GiftCooldown)
                {
                    reply = new TextObject();
                    return true;
                }
                else
                {
                    reply = new TextObject("{=BECE_Dialog_GiftCooldown}You just gave {HERO_NAME} a gift. You will need to wait for {COOLDOWN_DAYS} days before giving another gift!");
                    reply.SetTextVariable("HERO_NAME", targetHero.FirstName);
                    reply.SetTextVariable("COOLDOWN_DAYS", TaleWorlds.Library.MathF.Ceiling(MCMSettings.Instance.GiftCooldown - GiftsCooldown[targetHero].ElapsedDaysUntilNow).ToString());
                    return false;
                }


            }
            // We will return true if there is no data stored about that target hero. Since there is no history of this hero, we can assume the player has never interacted with the player
            reply = new TextObject();
            return true;
        }

        private void OpenTradeBarter()
        {
            //Settlement closestSettlement = GetClosestSettlements(PartyBase.MainParty.Position2D);
            //List<Barterable> allBarterables = new List<Barterable>();
            //for (int i = 0; i < PartyBase.MainParty.ItemRoster.Count; i++)
            //{
            //    ItemRosterElement elementCopyAtIndex = PartyBase.MainParty.ItemRoster.GetElementCopyAtIndex(i);
            //    if (elementCopyAtIndex.Amount > 0)
            //    {
            //        int averageValueOfItemInNearbySettlements = CalculateItemValueInNearbySettlement(elementCopyAtIndex.EquipmentElement, PartyBase.MainParty, closestSettlement);
            //        Barterable barterable = new ItemBarterable(Hero.MainHero, Hero.OneToOneConversationHero, PartyBase.MainParty, PartyBase.MainParty, elementCopyAtIndex, averageValueOfItemInNearbySettlements);
            //        allBarterables.Add(barterable);
            //    }
            //}
            //allBarterables.Add(new GoldBarterable(Hero.MainHero, Hero.OneToOneConversationHero, PartyBase.MainParty, PartyBase.MainParty, Hero.MainHero.Gold));
            //BarterManager.Instance.StartBarterOffer(Hero.MainHero, Hero.OneToOneConversationHero, PartyBase.MainParty, null, null, null, 0, false, allBarterables);
            BarterManager instance = BarterManager.Instance;
            Hero mainHero = Hero.MainHero;
            Hero oneToOneConversationHero = Hero.OneToOneConversationHero;
            PartyBase mainParty = PartyBase.MainParty;
            MobileParty partyBelongedTo = Hero.OneToOneConversationHero.PartyBelongedTo;
            BarterData args = new BarterData(mainHero, oneToOneConversationHero, mainParty, null);
            args.AddBarterGroup(new ItemBarterGroup());
            {
                Settlement closestSettlement = GetClosestSettlements(PartyBase.MainParty.Position2D);
                for (int i = 0; i < PartyBase.MainParty.ItemRoster.Count; i++)
                {
                    ItemRosterElement elementCopyAtIndex = PartyBase.MainParty.ItemRoster.GetElementCopyAtIndex(i);
                    if (elementCopyAtIndex.Amount > 0)
                    {
                        int averageValueOfItemInNearbySettlements = CalculateItemValueInNearbySettlement(elementCopyAtIndex.EquipmentElement, PartyBase.MainParty, closestSettlement);
                        Barterable barterable = new ItemBarterable(Hero.MainHero, null, PartyBase.MainParty, null, elementCopyAtIndex, -averageValueOfItemInNearbySettlements);
                        args.AddBarterable<ItemBarterGroup>(barterable);
                    }
                }
            }

            instance.BeginPlayerBarter(args);
        }

        private Settlement GetClosestSettlements(Vec2 position)
        {
            float smallestDistance = float.MaxValue;
            Settlement closestSettlement = null;
            MBReadOnlyList<Settlement> all = Settlement.All;
            int count = all.Count;
            for (int i = 0; i < count; i++)
            {
                Settlement settlement = all[i];
                if (settlement.IsTown)
                {
                    if (closestSettlement == null)
                    {
                        smallestDistance = position.DistanceSquared(settlement.GatePosition);
                        closestSettlement = settlement;
                    }
                    else
                    {
                        float distance = position.DistanceSquared(settlement.GatePosition);
                        if (distance < smallestDistance)
                        {
                            smallestDistance = distance;
                            closestSettlement = settlement;
                        }
                    }

                }
            }


            return closestSettlement;
        }
        private int CalculateItemValueInNearbySettlement(EquipmentElement itemRosterElement, PartyBase involvedParty, Settlement nearbySettlement)
        {

            return nearbySettlement.Town.GetItemPrice(itemRosterElement, involvedParty.MobileParty, true);
        }

    }
}
