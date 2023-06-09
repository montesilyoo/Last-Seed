using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CharacterCycler : MonoBehaviour
{
    public static CharacterCycler current;

    public Character_Database characterDB;
    public Button nextButton;
    public Button previousButton;
    public Button upgradeButton;
    public GameObject characterPrefab;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI characterTypeText;
    private int selectedIndex = 0;
    private Character selectedCharacter;
    public CharacterStatsDisplay statsDisplay;

    public event Action<Character> CharacterSelected;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        statsDisplay = GameObject.FindObjectOfType<CharacterStatsDisplay>();

        SelectCharacter(1);
        statsDisplay.UpdateStatsDisplay();

        nextButton.onClick.AddListener(NextCharacter);
        previousButton.onClick.AddListener(PreviousCharacter);
        upgradeButton.onClick.AddListener(UpgradeCharacter);
    }


    private void OnDestroy()
    {
        nextButton.onClick.RemoveAllListeners();
        previousButton.onClick.RemoveAllListeners();
    }

    private void UpgradeCharacter()
    {
        statsDisplay.UpdateStatsDisplay();
    }

    private void NextCharacter()
    {
        do
        {
            selectedIndex++;
            if (selectedIndex >= characterDB.characterCount)
            {
                selectedIndex = 1; // Skip the first character and start from the second character
            }

            selectedCharacter = characterDB.GetCharacter(selectedIndex);
        } while (selectedCharacter.isLocked);

        SelectCharacter(selectedIndex);
        statsDisplay.UpdateStatsDisplay();
    }

    private void PreviousCharacter()
    {
        do
        {
            selectedIndex--;
            if (selectedIndex < 1) // Skip the first character and loop back to the last character
            {
                selectedIndex = characterDB.characterCount - 1;
            }

            selectedCharacter = characterDB.GetCharacter(selectedIndex);
        } while (selectedCharacter.isLocked);

        SelectCharacter(selectedIndex);
        statsDisplay.UpdateStatsDisplay();
    }


    private void SelectCharacter(int index)
    {
        selectedCharacter = characterDB.GetCharacter(index);

        CharacterSelected?.Invoke(selectedCharacter);

        if (characterPrefab != null)
        {
            SpriteRenderer spriteRenderer = characterPrefab.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && selectedCharacter.characterSprite != null)
            {
                spriteRenderer.sprite = selectedCharacter.characterSprite;
            }
        }

        if (characterNameText != null)
        {
            characterNameText.text = selectedCharacter.characterName;
        }
        if (characterTypeText != null)
        {
            characterTypeText.text = selectedCharacter.characterType;
        }
    }

    public Character GetSelectedCharacter()
    {
        return selectedCharacter;
    }

}
