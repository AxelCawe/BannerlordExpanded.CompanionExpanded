using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace BannerlordExpanded.CompanionExpanded.WandererCreator.Template
{
    public class NPCCharacter
    {
        public string Id;
        public FormationClass Default_group;
        public int Level;
        public string Name;
        public Occupation Occupation = Occupation.Wanderer;
        public string Culture;
        public string Face_key_template;
        public List<Equipment> CombatEquipmentRosters;
        public List<string> CombatEquipmentSets;
        public List<Equipment> CivilianEquipmentRosters;
        public List<string> CivilianEquipmentSets;
        public BodyProperties BodyProperties;
        public BodyProperties BodyPropertiesMax;

        public enum CalculatingTraitEnum : int
        {
            HotHeaded = -2,
            Impulsive = -1,
            None = 0,
            Calculating = 1,
            Cerebral = 2,
        }
        public CalculatingTraitEnum CalculatingTrait;

        public enum GenerosityTraitEnum : int
        {
            Sadistic = -2,
            Cruel = -1,
            None = 0,
            Merciful = 1,
            Compassionate = 2,
        }
        public GenerosityTraitEnum GenerosityTrait;

        public enum HonorTraitEnum : int
        {
            Deceitful = -2,
            Devious = -1,
            None = 0,
            Honest = 1,
            Honorable = 2,
        }
        public HonorTraitEnum HonorTrait;

        public enum MercyTraitEnum : int
        {
            Tightfisted = -2,
            Closefisted = -1,
            None = 0,
            Generous = 1,
            Compassionate = 2,
        }
        public MercyTraitEnum MercyTrait;

        public enum ValorTraitEnum : int
        {
            VeryCautious = -2,
            Cautious = -1,
            None = 0,
            Daring = 1,
            Munificent = 2,
        }
        public ValorTraitEnum ValorTrait;

        public Dictionary<string, int> Skills = new Dictionary<string, int>();
    }
}
