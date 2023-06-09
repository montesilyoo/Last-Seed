using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Character_Database characterDB;

    public Transform leftSpawnPoint;
    public Transform middleSpawnPoint;
    public Transform rightSpawnPoint;

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

        UpdateCharacter(selectedOption1, selectedOption2, selectedOption3);
    }

    private void UpdateCharacter(int selectedOption1, int selectedOption2, int selectedOption3)
    {
        Character character1 = characterDB.GetCharacter(selectedOption1);
        Character character2 = characterDB.GetCharacter(selectedOption2);
        Character character3 = characterDB.GetCharacter(selectedOption3);

        if (selectedOption1 != 0)
        {
            GameObject leftPrefab = character1.characterPrefab;
            Instantiate(leftPrefab, leftSpawnPoint.position, leftSpawnPoint.rotation).name = "Player1";
        }

        if (selectedOption2 != 0)
        {
            GameObject middlePrefab = character2.characterPrefab;
            Instantiate(middlePrefab, middleSpawnPoint.position, middleSpawnPoint.rotation).name = "Player2";
        }

        if (selectedOption3 != 0)
        {
            GameObject rightPrefab = character3.characterPrefab;
            Instantiate(rightPrefab, rightSpawnPoint.position, rightSpawnPoint.rotation).name = "Player3";
        }
    }


    private void Load()
    {
        selectedOption1 = PlayerPrefs.GetInt("selectedOption1");
        selectedOption2 = PlayerPrefs.GetInt("selectedOption2");
        selectedOption3 = PlayerPrefs.GetInt("selectedOption3");
    }
}
