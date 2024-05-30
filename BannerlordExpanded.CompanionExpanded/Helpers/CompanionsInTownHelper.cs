using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Settlements;

namespace BannerlordExpanded.CompanionExpanded.Helpers
{
    public class CompanionsInTownHelper
    {
        public static LocationCharacter CreateWandererLocationCharacter(Hero wanderer)
        {
            
            Monster monsterWithSuffix = FaceGen.GetMonsterWithSuffix(wanderer.CharacterObject.Race, "_settlement");
            string actionSetCode = ActionSetCode.GenerateActionSetNameWithSuffix(monsterWithSuffix, wanderer.IsFemale, "_warrior_in_tavern");
            return new LocationCharacter(new AgentData(new PartyAgentOrigin(null, wanderer.CharacterObject, -1, default(UniqueTroopDescriptor), false)).Monster(monsterWithSuffix).NoHorses(true), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddOutdoorWandererBehaviors), "npc_common", true, LocationCharacter.CharacterRelations.Neutral, actionSetCode, true, false, null, false, false, true);
            
        }
    }
}
