using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatModifier : MonoBehaviour
{
    public CharacterCycler characterCycler;
    public Button button;

    private void Start()
    {
        characterCycler = GetComponent<CharacterCycler>();

        // Add button click listener
        button.onClick.AddListener(ModifyStats);
    }

    public void ModifyStats()
    {
        Character selectedCharacter = characterCycler.GetSelectedCharacter();
        if (selectedCharacter != null && !selectedCharacter.isLocked)
        {
            FighterStatsTest FighterStatsTest = selectedCharacter.characterPrefab.GetComponent<FighterStatsTest>();
            if (FighterStatsTest != null)
            {
                FighterStatsTest.level += 1;
                FighterStatsTest.health += 5;
                FighterStatsTest.attack += 5;
                FighterStatsTest.defense += 5;
                FighterStatsTest.speed += 5;
            }
        }
    }
}
