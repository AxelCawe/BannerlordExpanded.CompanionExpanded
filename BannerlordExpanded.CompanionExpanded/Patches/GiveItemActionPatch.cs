using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace BannerlordExpanded.CompanionExpanded.Patches
{
    [HarmonyPatchCategory("GiveGifts")]
    [HarmonyPatch(typeof(GiveItemAction), "ApplyInternal")]
    public static class GiveItemActionPatch
    {
        [HarmonyPrefix]
        static bool Prefix(Hero giver, Hero receiver, PartyBase giverParty, PartyBase receiverParty, in ItemRosterElement itemRosterElement)
        {
            if (receiver == null && receiverParty == null)
            {
                if (giverParty != null)
                {

                    giverParty.ItemRoster.AddToCounts(itemRosterElement.EquipmentElement, -itemRosterElement.Amount);


                    return false;
                }
                else if (giver != null)
                {
                    giver.PartyBelongedTo.ItemRoster.AddToCounts(itemRosterElement.EquipmentElement, -itemRosterElement.Amount);
                    return false;
                }

            }
            return true;
        }

        //[HarmonyPrefix]
        //private static bool Prefix(Hero giver, Hero receiver, PartyBase giverParty, PartyBase receiverParty, ItemObject item, int count)
        //{
        //    bool flag = receiver == null && receiverParty == null;
        //    if (flag)
        //    {
        //        bool flag2 = giverParty != null;
        //        if (flag2)
        //        {
        //            foreach (ItemRosterElement itemRosterElement in giverParty.ItemRoster)
        //            {
        //                bool flag3 = itemRosterElement.EquipmentElement.Item == item;
        //                if (flag3)
        //                {
        //                    giverParty.ItemRoster.AddToCounts(itemRosterElement.EquipmentElement, -count);
        //                }
        //            }
        //            return false;
        //        }
        //        bool flag4 = giver != null;
        //        if (flag4)
        //        {
        //            foreach (ItemRosterElement itemRosterElement2 in giver.PartyBelongedTo.ItemRoster)
        //            {
        //                bool flag5 = itemRosterElement2.EquipmentElement.Item == item;
        //                if (flag5)
        //                {
        //                    giver.PartyBelongedTo.ItemRoster.AddToCounts(itemRosterElement2.EquipmentElement, -count);
        //                }
        //            }
        //            return false;
        //        }
        //    }
        //    return true;
        //}
    }
}
