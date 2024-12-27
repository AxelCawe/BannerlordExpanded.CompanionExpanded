using BannerlordExpanded.CompanionExpanded.WandererCreator.Template;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Core;

namespace BannerlordExpanded.CompanionExpanded.WandererCreator.Serializer
{
    public static class WandererSerializer
    {
        public static bool Serialize(List<NPCCharacter> allNpcs, string location)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("NPCCharacters");
            foreach (NPCCharacter character in allNpcs)
            {
                XmlElement xmlElement = doc.CreateElement("NPCCharacter");
                XmlAttribute xmlAttribute = doc.CreateAttribute("id");
                xmlAttribute.Value = character.Id;
                xmlElement.Attributes.Append(xmlAttribute);
                XmlAttribute xmlAttribute2 = doc.CreateAttribute("default_group");
                xmlAttribute2.Value = character.Default_group.ToString();
                xmlElement.Attributes.Append(xmlAttribute2);
                XmlAttribute xmlAttribute3 = doc.CreateAttribute("level");
                xmlAttribute3.Value = character.Level.ToString();
                xmlElement.Attributes.Append(xmlAttribute3);
                XmlAttribute xmlAttribute4 = doc.CreateAttribute("name");
                xmlAttribute4.Value = character.Name;
                xmlElement.Attributes.Append(xmlAttribute4);
                XmlAttribute xmlAttribute5 = doc.CreateAttribute("occupation");
                xmlAttribute5.Value = character.Occupation.ToString();
                xmlElement.Attributes.Append(xmlAttribute5);
                XmlAttribute xmlAttribute6 = doc.CreateAttribute("culture");
                xmlAttribute6.Value = "Culture." + character.Culture;
                xmlElement.Attributes.Append(xmlAttribute6);

                // Face
                XmlElement xmlElement2 = doc.CreateElement("face");
                XmlElement xmlElement3 = doc.CreateElement("BodyProperties");
                XmlAttribute xmlAttribute7 = doc.CreateAttribute("version");
                XmlAttribute xmlAttribute8 = doc.CreateAttribute("age");
                XmlAttribute xmlAttribute9 = doc.CreateAttribute("weight");
                XmlAttribute xmlAttribute10 = doc.CreateAttribute("build");
                XmlAttribute xmlAttribute11 = doc.CreateAttribute("key");
                xmlElement3.Attributes.Append(xmlAttribute7);
                xmlElement3.Attributes.Append(xmlAttribute8);
                xmlElement3.Attributes.Append(xmlAttribute9);
                xmlElement3.Attributes.Append(xmlAttribute10);
                xmlElement3.Attributes.Append(xmlAttribute11);
                xmlElement2.AppendChild(xmlElement3);
                XmlElement xmlElement4 = doc.CreateElement("BodyPropertiesMax");
                XmlAttribute xmlAttribute12 = doc.CreateAttribute("version");
                XmlAttribute xmlAttribute13 = doc.CreateAttribute("age");
                XmlAttribute xmlAttribute14 = doc.CreateAttribute("weight");
                XmlAttribute xmlAttribute15 = doc.CreateAttribute("build");
                XmlAttribute xmlAttribute16 = doc.CreateAttribute("key");
                xmlElement4.Attributes.Append(xmlAttribute12);
                xmlElement4.Attributes.Append(xmlAttribute13);
                xmlElement4.Attributes.Append(xmlAttribute14);
                xmlElement4.Attributes.Append(xmlAttribute15);
                xmlElement4.Attributes.Append(xmlAttribute16);
                xmlElement2.AppendChild(xmlElement3);
                xmlElement.AppendChild(xmlElement2);

                // Traits
                XmlElement traitsElement = doc.CreateElement("Traits");
                if (character.CalculatingTrait != NPCCharacter.CalculatingTraitEnum.None)
                {
                    XmlElement traitElement = doc.CreateElement("Trait");
                    XmlAttribute traitAttrId = doc.CreateAttribute("id");
                    traitAttrId.Value = "Calculating";
                    traitElement.Attributes.Append(traitAttrId);
                    XmlAttribute traitAttrValue = doc.CreateAttribute("value");
                    traitAttrId.Value = ((int)character.CalculatingTrait).ToString();
                    traitElement.Attributes.Append(traitAttrId);
                    traitsElement.AppendChild(traitElement);
                }
                if (character.GenerosityTrait != NPCCharacter.GenerosityTraitEnum.None)
                {
                    XmlElement traitElement = doc.CreateElement("Trait");
                    XmlAttribute traitAttrId = doc.CreateAttribute("id");
                    traitAttrId.Value = "Generosity";
                    traitElement.Attributes.Append(traitAttrId);
                    XmlAttribute traitAttrValue = doc.CreateAttribute("value");
                    traitAttrId.Value = ((int)character.GenerosityTrait).ToString();
                    traitElement.Attributes.Append(traitAttrId);
                    traitsElement.AppendChild(traitElement);
                }
                if (character.HonorTrait != NPCCharacter.HonorTraitEnum.None)
                {
                    XmlElement traitElement = doc.CreateElement("Trait");
                    XmlAttribute traitAttrId = doc.CreateAttribute("id");
                    traitAttrId.Value = "Honor";
                    traitElement.Attributes.Append(traitAttrId);
                    XmlAttribute traitAttrValue = doc.CreateAttribute("value");
                    traitAttrId.Value = ((int)character.HonorTrait).ToString();
                    traitElement.Attributes.Append(traitAttrId);
                    traitsElement.AppendChild(traitElement);
                }
                if (character.MercyTrait != NPCCharacter.MercyTraitEnum.None)
                {
                    XmlElement traitElement = doc.CreateElement("Trait");
                    XmlAttribute traitAttrId = doc.CreateAttribute("id");
                    traitAttrId.Value = "Mercy";
                    traitElement.Attributes.Append(traitAttrId);
                    XmlAttribute traitAttrValue = doc.CreateAttribute("value");
                    traitAttrId.Value = ((int)character.MercyTrait).ToString();
                    traitElement.Attributes.Append(traitAttrId);
                    traitsElement.AppendChild(traitElement);
                }
                if (character.ValorTrait != NPCCharacter.ValorTraitEnum.None)
                {
                    XmlElement traitElement = doc.CreateElement("Trait");
                    XmlAttribute traitAttrId = doc.CreateAttribute("id");
                    traitAttrId.Value = "Valor";
                    traitElement.Attributes.Append(traitAttrId);
                    XmlAttribute traitAttrValue = doc.CreateAttribute("value");
                    traitAttrId.Value = ((int)character.ValorTrait).ToString();
                    traitElement.Attributes.Append(traitAttrId);
                    traitsElement.AppendChild(traitElement);
                }
                xmlElement.AppendChild(traitsElement);


                // Skills
                XmlElement skillsElement = doc.CreateElement("skills");
                foreach (var skill in character.Skills)
                {
                    if (skill.Value != 0)
                    {
                        XmlElement skillElement = doc.CreateElement("skill");
                        XmlAttribute skillIdAttr = doc.CreateAttribute("id");
                        skillIdAttr.Value = skill.Key;
                        skillElement.Attributes.Append(skillIdAttr);
                        XmlAttribute skillValueAttr = doc.CreateAttribute("value");
                        skillValueAttr.Value = skill.Value.ToString();
                        skillElement.Attributes.Append(skillValueAttr);
                        skillsElement.AppendChild(skillElement);
                    }
                }
                xmlElement.AppendChild(skillsElement);

                XmlElement xmlElement5 = doc.CreateElement("Equipments");
                {
                    foreach (Equipment equipmentRoster in character.CombatEquipmentRosters)
                    {
                        XmlElement equipmentRosterElement = doc.CreateElement("EquipmentRoster");
                        XmlAttribute equipmentRosterAttribute = doc.CreateAttribute("civilian");
                        equipmentRosterAttribute.Value = "false";
                        AddItemToNode(equipmentRoster, EquipmentIndex.Weapon0, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Weapon1, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Weapon2, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Weapon3, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Head, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Body, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Leg, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Gloves, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Cape, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Horse, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.HorseHarness, doc, equipmentRosterElement);

                        xmlElement5.AppendChild(equipmentRosterElement);
                    }
                    foreach (Equipment equipmentRoster in character.CivilianEquipmentRosters)
                    {
                        XmlElement equipmentRosterElement = doc.CreateElement("EquipmentRoster");
                        XmlAttribute equipmentRosterAttribute = doc.CreateAttribute("civilian");
                        equipmentRosterAttribute.Value = "true";
                        AddItemToNode(equipmentRoster, EquipmentIndex.Weapon0, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Weapon1, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Weapon2, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Weapon3, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Head, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Body, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Leg, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Gloves, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Cape, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.Horse, doc, equipmentRosterElement);
                        AddItemToNode(equipmentRoster, EquipmentIndex.HorseHarness, doc, equipmentRosterElement);

                        xmlElement5.AppendChild(equipmentRosterElement);
                    }
                }

                xmlElement.AppendChild(xmlElement5);

            }


            return true;
        }

        private static void AddItemToNode(Equipment equipment, EquipmentIndex equipmentIndex, XmlDocument doc, XmlElement parentNode)
        {
            string slot = "";
            string id = "";
            EquipmentElement equipmentFromSlot = equipment.GetEquipmentFromSlot(equipmentIndex);
            if (equipmentFromSlot.Item != null)
            {
                string str;
                switch (equipmentIndex)
                {
                    case EquipmentIndex.Weapon0:
                        str = "Item0";
                        break;
                    case EquipmentIndex.Weapon1:
                        str = "Item1";
                        break;
                    case EquipmentIndex.Weapon2:
                        str = "Item2";
                        break;
                    case EquipmentIndex.Weapon3:
                        str = "Item3";
                        break;
                    case EquipmentIndex.ExtraWeaponSlot:
                        str = "Item4";
                        break;
                    case EquipmentIndex.Head:
                        str = "Head";
                        break;
                    case EquipmentIndex.Body:
                        str = "Body";
                        break;
                    case EquipmentIndex.Leg:
                        str = "Leg";
                        break;
                    case EquipmentIndex.Gloves:
                        str = "Gloves";
                        break;
                    case EquipmentIndex.Cape:
                        str = "Cape";
                        break;
                    case EquipmentIndex.Horse:
                        str = "Horse";
                        break;
                    case EquipmentIndex.HorseHarness:
                        str = "HorseHarness";
                        break;
                    default:
                        str = equipmentIndex.ToString();
                        break;
                }
                slot = str;
                id = equipmentFromSlot.Item.StringId;
            }
            XmlElement element = doc.CreateElement("equipment");
            XmlAttribute slotAttr = doc.CreateAttribute("slot");
            slotAttr.Value = slot;
            XmlAttribute idAttr = doc.CreateAttribute("id");
            idAttr.Value = "Item." + id;
            element.Attributes.Append(slotAttr);
            element.Attributes.Append(idAttr);
            parentNode.AppendChild(element);
        }
    }
}
