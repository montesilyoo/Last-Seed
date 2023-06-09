using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    public Character_Database characterDB;

    public TextMeshProUGUI leftText;
    public TextMeshProUGUI middleText;
    public TextMeshProUGUI rightText;

    public SpriteRenderer leftSprite;
    public SpriteRenderer middleSprite;
    public SpriteRenderer rightSprite;

    private int selectedOption1 = 0;
    private int selectedOption2 = 0;
    private int selectedOption3 = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption1"))
        {
            selectedOption1 = 0;
        }
        else
        {
            Load();
        }

        if (!PlayerPrefs.HasKey("selectedOption2"))
        {
            selectedOption2 = 0;
        }
        else
        {
            Load();
        }

        if (!PlayerPrefs.HasKey("selectedOption3"))
        {
            selectedOption3 = 0;
        }
        else
        {
            Load();
        }

        UpdateCharacter(selectedOption1, 1);
        UpdateCharacter(selectedOption2, 2);
        UpdateCharacter(selectedOption3, 3);
    }

    public void NextOptionLeft()
    {
        selectedOption1++;
        if (selectedOption1 >= characterDB.characterCount)
        {
            selectedOption1 = 0;
        }

        while ((selectedOption1 != 0 && (selectedOption1 == selectedOption2 || selectedOption1 == selectedOption3)) ||
               characterDB.GetCharacter(selectedOption1).isLocked)
        {
            selectedOption1++;
            if (selectedOption1 >= characterDB.characterCount)
            {
                selectedOption1 = 0;
            }
        }

        UpdateCharacter(selectedOption1, 1);
        Save();
    }

    public void BackOptionLeft()
    {
        selectedOption1--;
        if (selectedOption1 < 0)
        {
            selectedOption1 = characterDB.characterCount - 1;
        }

        while ((selectedOption1 != 0 && (selectedOption1 == selectedOption2 || selectedOption1 == selectedOption3)) ||
               characterDB.GetCharacter(selectedOption1).isLocked)
        {
            selectedOption1--;
            if (selectedOption1 < 0)
            {
                selectedOption1 = characterDB.characterCount - 1;
            }
        }

        UpdateCharacter(selectedOption1, 1);
        Save();
    }


    public void NextOptionMiddle()
    {
        selectedOption2++;
        if (selectedOption2 >= characterDB.characterCount)
        {
            selectedOption2 = 0;
        }

        while ((selectedOption2 != 0 && (selectedOption2 == selectedOption1 || selectedOption2 == selectedOption3)) ||
               characterDB.GetCharacter(selectedOption2).isLocked)
        {
            selectedOption2++;
            if (selectedOption2 >= characterDB.characterCount)
            {
                selectedOption2 = 0;
            }
        }

        UpdateCharacter(selectedOption2, 2);
        Save();
    }

    public void BackOptionMiddle()
    {
        selectedOption2--;
        if (selectedOption2 < 0)
        {
            selectedOption2 = characterDB.characterCount - 1;
        }

        while ((selectedOption2 != 0 && (selectedOption2 == selectedOption1 || selectedOption2 == selectedOption3)) ||
               characterDB.GetCharacter(selectedOption2).isLocked)
        {
            selectedOption2--;
            if (selectedOption2 < 0)
            {
                selectedOption2 = characterDB.characterCount - 1;
            }
        }

        UpdateCharacter(selectedOption2, 2);
        Save();
    }



    public void NextOptionRight()
    {
        selectedOption3++;
        if (selectedOption3 >= characterDB.characterCount)
        {
            selectedOption3 = 0;
        }

        while ((selectedOption3 != 0 && (selectedOption3 == selectedOption1 || selectedOption3 == selectedOption2)) ||
               characterDB.GetCharacter(selectedOption3).isLocked)
        {
            selectedOption3++;
            if (selectedOption3 >= characterDB.characterCount)
            {
                selectedOption3 = 0;
            }
        }

        UpdateCharacter(selectedOption3, 3);
        Save();
    }

    public void BackOptionRight()
    {
        selectedOption3--;
        if (selectedOption3 < 0)
        {
            selectedOption3 = characterDB.characterCount - 1;
        }

        while ((selectedOption3 != 0 && (selectedOption3 == selectedOption1 || selectedOption3 == selectedOption2)) ||
               characterDB.GetCharacter(selectedOption3).isLocked)
        {
            selectedOption3--;
            if (selectedOption3 < 0)
            {
                selectedOption3 = characterDB.characterCount - 1;
            }
        }

        UpdateCharacter(selectedOption3, 3);
        Save();
    }

    private void UpdateCharacter(int selectedOption, int sprite)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        switch (sprite)
        {
            case 1:
                leftSprite.sprite = character.characterSprite;
                leftText.text = character.characterName;
                break;
            case 2:
                middleSprite.sprite = character.characterSprite;
                middleText.text = character.characterName;
                break;
            case 3:
                rightSprite.sprite = character.characterSprite;
                rightText.text = character.characterName;
                break;
        }
    }

    private void Load()
    {
        selectedOption1 = PlayerPrefs.GetInt("selectedOption1");
        selectedOption2 = PlayerPrefs.GetInt("selectedOption2");
        selectedOption3 = PlayerPrefs.GetInt("selectedOption3");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption1", selectedOption1);
        PlayerPrefs.SetInt("selectedOption2", selectedOption2);
        PlayerPrefs.SetInt("selectedOption3", selectedOption3);
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene("Upgrade Character");
    }
}
