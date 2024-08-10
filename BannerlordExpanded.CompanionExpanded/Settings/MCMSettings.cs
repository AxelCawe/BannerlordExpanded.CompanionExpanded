using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using System;

namespace BannerlordExpanded.CompanionExpanded.Settings
{
    internal class MCMSettings : AttributeGlobalSettings<MCMSettings>
    {
        public override string Id => "BannerlordExpanded.CompanionExpanded";

        public override string DisplayName => "BE - Companion Expanded";

        public override string FolderName => "BannerlordExpanded.CompanionExpanded";

        public override string FormatType => "xml";


        #region COMPANION_SPAWNING

        [SettingPropertyGroup("{=BE_CE_Settings_Header_CompanionSpawning}Companion Spawning", GroupOrder = 0)]
        [SettingPropertyBool("{=BE_CE_Settings_Header_CompanionSpawning}Companion Spawning", IsToggle = true, RequireRestart = true)]
        public bool CompanionSpawningActive { get; set; } = true;

        [SettingPropertyInteger("{=BE_CE_Settings_maxCompanions}Maximum number of wanderers in town", minValue: 1, maxValue: 100, Order = 1, HintText = "{=BE_CE_Settings_maxCompanions_Desc}The maximum number of wanderers that can be in a town at once. Expect lag/temporary freezes if you set a high number.", RequireRestart = false)]
        [SettingPropertyGroup("{=BE_CE_Settings_Header_CompanionSpawning}Companion Spawning", GroupOrder = 0)]
        public int maxCompanionsPerTown { get; set; } = 5;

        //[SettingPropertyBool("{=BE_CE_Settings_refreshWeekly}Auto-refresh Wanderers weekly", Order = 2, HintText = "{=BE_CE_Settings_RefreshWeekly_Desc}Automatically deletes old wanderers and spawn new wanderers at the start of every week. May cause lag!",RequireRestart = false)]
        //[SettingPropertyGroup("{=BE_CE_Settings_Header_CompanionSpawning}Companion Spawning", GroupOrder = 0)]
        //public bool autoRefreshEveryWeek { get; set; } = false;

        [SettingPropertyButton("{=BE_CE_Settings_refresh}Refresh Wanderers", Content = "{=BE_CE_Settings_Refresh_ButtonContent}Press to refresh", Order = 3, RequireRestart = false, HintText = "{=BE_CE_Settings_Refresh_Desc}Removes all current wanderers and spawn new ones. MAY CAUSE TEMPORARY FREEZE, NO NEED TO WORRY! :D")]
        [SettingPropertyGroup("{=BE_CE_Settings_Header_CompanionSpawning}Companion Spawning", GroupOrder = 0)]
        public Action RefreshWanderers { get; set; }

        [SettingPropertyButton("{=BE_CE_Settings_cleanup}Cleanup Wanderers [Uninstallation]", Content = "{=BE_CE_Settings_Cleanup_ButtonContent}Press to remove dead wanderers from game", Order = 4, RequireRestart = false, HintText = "{=BE_CE_Settings_Cleanup_Desc}Removes all dead wanderers from game.")]
        [SettingPropertyGroup("{=BE_CE_Settings_Header_CompanionSpawning}Companion Spawning", GroupOrder = 0)]
        public Action Cleanup { get; set; }
        #endregion


        // Gift Invite Settings
        [SettingPropertyGroup("{=BE_CE_Settings_Header_Gift}Give Gifts", GroupOrder = 1)]
        [SettingPropertyBool("{=BE_CE_Settings_Header_Gift}Give Gifts", IsToggle = true, RequireRestart = true)]
        public bool GiveGiftsActive { get; set; } = true;

        [SettingPropertyInteger("{=BE_CE_Settings_GiftCooldown}Gift Cooldown (Days)", minValue: 1, maxValue: 30, valueFormat: "0 days", Order = 1, HintText = "{=BE_CE_Settings_GiftCooldown_Desc}The minimum time interval (days) before you can give another gift to the same person.", RequireRestart = false)]
        [SettingPropertyGroup("{=BE_CE_Settings_Header_Gift}Give Gifts", GroupOrder = 1)]
        public int GiftCooldown { get; set; } = 7;

        // Wanderer requests to join
        [SettingPropertyGroup("{=BE_CE_Settings_Header_Request}Wanderer Join Request", GroupOrder = 2)]
        [SettingPropertyBool("{=BE_CE_Settings_Header_Request}Wanderer Join Request", IsToggle = true, RequireRestart = true)]
        public bool WandererRequestToJoin { get; set; } = true;

        [SettingPropertyInteger("{=BE_CE_Settings_RequestInterval}Wanderer Request Cooldown (Days)", minValue: 0, maxValue: 30, valueFormat: "0 days", Order = 1, HintText = "{=BE_CE_Settings_RequestInterval_Desc}The minimum time interval (days) before you can receive another request for a clan member to join.", RequireRestart = false)]
        [SettingPropertyGroup("{=BE_CE_Settings_Header_Request}Wanderer Join Request", GroupOrder = 2)]
        public int RequestInterval { get; set; } = 1;
        [SettingPropertyInteger("{=BE_CE_Settings_RequestChance}Chance of Wanderer Request per Clan Tier", minValue: 0, maxValue: 100, valueFormat: "0 percent", Order = 2, HintText = "{=BE_CE_Settings_RequestChance_Desc}The chance of receiving a request per tier. If your clan tier is 3, the chance of receiving a request is 3x of the value your provided.", RequireRestart = false)]
        [SettingPropertyGroup("{=BE_CE_Settings_Header_Request}Wanderer Join Request", GroupOrder = 2)]
        public int ChanceForARequestPerClanTier { get; set; } = 5;

        // Companions in Town
        [SettingPropertyBool("{=BE_CE_Settings_Header_CompanionsInTown}Wanderers will roam on Town Center", IsToggle = true, RequireRestart = true)]
        [SettingPropertyGroup("{=BE_CE_Settings_Header_CompanionsInTown}Wanderers will roam on Town Center", GroupOrder = 3)]
        public bool CompanionsInTown { get; set; } = true;

        // Companion To Sibling Relationships
        [SettingPropertyGroup("{=BE_CE_Settings_CompanionToFamily_Header}Companion To Family Conversations", GroupOrder = 4)]
        [SettingPropertyBool("{=BE_CE_Settings_CompanionToChild}Enable Companion To Son/Daughter Conversation", RequireRestart = true)]
        public bool CompanionToChildActive { get; set; } = true;

        [SettingPropertyGroup("{=BE_CE_Settings_CompanionToFamily_Header}Companion To Family Conversations", GroupOrder = 4)]
        [SettingPropertyBool("{=BE_CE_Settings_CompanionToSibling}Enable Companion To Sibling Conversation", RequireRestart = true)]
        public bool CompanionToSiblingActive { get; set; } = true;

        [SettingPropertyGroup("{=BE_CE_Settings_CompanionToFamily_Header}Companion To Family Conversations", GroupOrder = 4)]
        [SettingPropertyBool("{=BE_CE_Settings_DisownSon}Enable Disown Children Conversation", RequireRestart = true)]
        public bool DisownChildrenActive { get; set; } = true;

        // More Followers
        [SettingPropertyGroup("{=BE_CE_Settings_MoreFollowers_Header}More Followers", GroupOrder = 5)]
        [SettingPropertyBool("{=BE_CE_Settings_CompanionToSibling}Enable More Followers Module", IsToggle = true, RequireRestart = true)]
        public bool MoreFollowersActive { get; set; } = true;
        [SettingPropertyGroup("{=BE_CE_Settings_MoreFollowers_Header}More Followers", GroupOrder = 5)]
        [SettingPropertyInteger("{=BE_CE_Settings_MoreFollowers_NumOfFollowers}Number of Followers", 1, 1000, HintText = "{=BE_CE_Settings_MoreFollowers_NumOfFollowersDesc}Number of family members & companions that will follow you whenever you visit settlements.", RequireRestart = false)]
        public int NumberOfFollowers { get; set; } = 3;


        // Player Companion Limit
        [SettingPropertyGroup("{=BE_CE_Settings_CompanionLimit_Header}Additional Companion Slots", GroupOrder = 6)]
        [SettingPropertyBool("{=BE_CE_Settings_CompanionLimit_Header}Additional Companion Slots", IsToggle = true, RequireRestart = true)]
        public bool AdditionalCompanionSlotsActive { get; set; } = true;
        [SettingPropertyGroup("{=BE_CE_Settings_CompanionLimit_Header}Additional Companion Slots", GroupOrder = 6)]
        [SettingPropertyInteger("{=BE_CE_Settings_CompanionLimit_AdditionalSlots}Extra Companion Slots Per Clan Tier", 1, 1000, HintText = "{=BE_CE_Settings_CompanionLimit_AdditionalSlotsDesc}Number of additional companion slots per clan tier.", RequireRestart = false)]
        public int AdditionalCompanionsPerClanTier { get; set; } = 1;
    }
}
