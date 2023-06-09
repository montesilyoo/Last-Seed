using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string characterName;
    public string characterType;
    public Sprite characterSprite;
    public GameObject characterPrefab;
    public bool isLocked = true;

    public void Unlock()
    {
        isLocked = false;
    }
}