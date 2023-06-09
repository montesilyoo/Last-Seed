using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[CreateAssetMenu]
public class Character_Database : ScriptableObject
{
    public Character[] character;

    public int characterCount
    {
        get
        {
            return character.Length;
        }
    }

    public Character GetCharacter(int index)
    {
        return character[index];
    }

    public void UnlockCharacterByName(string characterName)
    {
        Character characterToUnlock = character.FirstOrDefault(c => c.characterName == characterName);
        if (characterToUnlock != null)
        {
            characterToUnlock.Unlock();
        }
    }
}

