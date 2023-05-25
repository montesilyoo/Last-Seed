using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="GameObjects/ShopItem", order = 0)]
public class ShopItem : ScriptableObject
{
    public string Name = "Default";
    public string Description = "Description";
    public int Level;
    public int Price;
    public CurrencyType Currency;
    public ObjectType Type;
    public Sprite Icon;
    public GameObject Prefab;

}

public enum ObjectType
{
        Buildings,
        Excavate,
        Crystals
}
