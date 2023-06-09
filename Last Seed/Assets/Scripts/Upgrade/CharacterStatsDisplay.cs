using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatsDisplay : MonoBehaviour
{
    public Text healthText;
    public Text lvlText;
    public Text attackText;
    //public Text magicRangeText;
    public Text defenseText;
    public Text speedText;

    private void Start()
    {
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        Character selectedCharacter = CharacterCycler.current.GetSelectedCharacter();
        if (selectedCharacter != null)
        {
            FighterStatsTest FighterStatsTest = selectedCharacter.characterPrefab.GetComponent<FighterStatsTest>();
            if (FighterStatsTest != null)
            {
                healthText.text = FighterStatsTest.health.ToString();
                lvlText.text = FighterStatsTest.level.ToString();
                attackText.text = FighterStatsTest.attack.ToString();
                //magicRangeText.text = FighterStatsTest.magicRange.ToString();
                defenseText.text = FighterStatsTest.defense.ToString();
                speedText.text = FighterStatsTest.speed.ToString();
            }
        }
    }

    // Call this method to manually update the character stats display
    public void UpdateStatsDisplay()
    {
        UpdateCharacterStats();
    }
}

