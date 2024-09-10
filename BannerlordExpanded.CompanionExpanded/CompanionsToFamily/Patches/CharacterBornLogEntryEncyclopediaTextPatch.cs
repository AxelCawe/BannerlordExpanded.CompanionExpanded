using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.Localization;

namespace BannerlordExpanded.CompanionExpanded.CompanionsToFamily.Patches
{
    [HarmonyPatchCategory("DisownChildToCompanion")]
    [HarmonyPatch(typeof(CharacterBornLogEntry), "GetEncyclopediaText")]
    public static class CharacterBornLogEntryEncyclopediaTextPatch
    {
        [HarmonyFinalizer]
        static Exception Finalizer(Exception __exception, CharacterBornLogEntry __instance, ref TextObject __result)
        {
            Hero BornCharacter = typeof(CharacterBornLogEntry).GetField("BornCharacter", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(__instance) as Hero;
            if (BornCharacter.Father == null || BornCharacter.Mother == null)
            {
                TextObject textObject = new TextObject("");
                __result = textObject;
                return null;
            }
            return __exception;
        }
        /*  [HarmonyPrefix]
          static bool Prefix(ref CharacterBornLogEntry __instance, ref TextObject __result)
          {
              Hero BornCharacter = typeof(CharacterBornLogEntry).GetField("BornCharacter", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(__instance) as Hero;
              if (BornCharacter.Father == null || BornCharacter.Mother == null)
              {
                  TextObject textObject = GameTexts.FindText("str_notification_character_born", null);
                  StringHelpers.SetCharacterProperties("HERO", BornCharacter.CharacterObject, textObject, false);
                  if (BornCharacter.Mother == null)
                      StringHelpers.SetCharacterProperties("MOTHER", BornCharacter.CharacterObject, textObject, false);
                  else
                      StringHelpers.SetCharacterProperties("MOTHER", BornCharacter.CharacterObject, textObject, false);
                  //StringHelpers.SetCharacterProperties("MOTHER", BornCharacter.CharacterObject, textObject, false);
                  //StringHelpers.SetCharacterProperties("MOTHER", BornCharacter.Mother.CharacterObject, textObject, false);
                  if (BornCharacter.Father == null)
                      StringHelpers.SetCharacterProperties("FATHER", BornCharacter.CharacterObject, textObject, false);
                  else
                      StringHelpers.SetCharacterProperties("FATHER", BornCharacter.CharacterObject, textObject, false);
                  //StringHelpers.SetCharacterProperties("FATHER", BornCharacter.Father.CharacterObject, textObject, false);
                  __result = textObject;
                  return false;
              }
              return true;
          }*/

    }
}
