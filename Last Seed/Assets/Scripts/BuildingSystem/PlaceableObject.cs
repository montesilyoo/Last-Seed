using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public static PlaceableObject current;
    public bool Placed {get; private set;}
    private Vector3 origin;

    public BoundsInt area;
    

    private void Awake()
    {
        current = this;
    }

    public bool CanBePlaced()
    {
        Vector3Int positionInt = BuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if(BuildingSystem.current.CanTakeArea(areaTemp))
        {
            return true;
        }

        return false;
    }

    public void Place()
    {
        Debug.Log("doinkers");
        Vector3Int positionInt = BuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        Placed = true;

        BuildingSystem.current.TakeArea(areaTemp);
    }

    public void CheckPlacement()
    {
        Debug.Log("doinkers");
        if(CanBePlaced())
        {
            Place();
            origin = transform.position;
        }

        else
        {
            Destroy(transform.gameObject);
        }

        ShopManager.current.ShopButton_Click();

    }
}
