using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class ItemToSpawn
{
    public GameObject item;
    public float spawnRate;
    [HideInInspector] public float minSpawnProb, maxSpawnProb;
}

public class LootSystem : MonoBehaviour
{
    public Character_Database characterDB; // Serialized field for referencing Character_Database asset

    public ItemToSpawn[] itemToSpawn;
    private GameObject instantiatedItem;
    private GameObject deleteItem;
    public Button spawnButton;

    private List<string> pullHistory = new List<string>();

    void Start()
    {
        spawnButton.onClick.AddListener(Spawnner);
        for (int i = 0; i < itemToSpawn.Length; i++)
        {
            if (i == 0)
            {
                itemToSpawn[i].minSpawnProb = 0;
                itemToSpawn[i].maxSpawnProb = itemToSpawn[i].spawnRate - 1;
            }
            else
            {
                itemToSpawn[i].minSpawnProb = itemToSpawn[i - 1].maxSpawnProb + 1;
                itemToSpawn[i].maxSpawnProb = itemToSpawn[i].minSpawnProb + itemToSpawn[i].spawnRate - 1;
            }
        }
    }

    void Spawnner()
    {
        spawnButton.enabled = false;
        if (instantiatedItem != null)
        {
            deleteItem = instantiatedItem;

            LeanTween.moveLocal(deleteItem, new Vector3(0f, 1f, 0f), .5f).setEase(LeanTweenType.easeInOutCubic)
                .setOnComplete(delegate ()
                {
                    LeanTween.moveLocal(deleteItem, new Vector3(0f, 10f, 0f), .5f).setEase(LeanTweenType.easeInOutCubic)
                        .setOnComplete(delegate ()
                        {
                            Destroy(deleteItem);
                            spawnButton.enabled = true;
                        });
                });
        }
        else if (deleteItem == null)
        {
            spawnButton.enabled = true;
        }

        float randomNum = Random.Range(0, 100);

        for (int i = 0; i < itemToSpawn.Length; i++)
        {
            if (randomNum >= itemToSpawn[i].minSpawnProb && randomNum <= itemToSpawn[i].maxSpawnProb)
            {
                instantiatedItem = Instantiate(itemToSpawn[i].item, transform.position, Quaternion.identity);
                LeanTween.scale(instantiatedItem, new Vector3(1f, 1f, 1f), 2f).setDelay(1f).setEase(LeanTweenType.easeOutElastic);
                
                pullHistory.Add(instantiatedItem.name);
                SavePullHistoryToFile();
                break;
            }
        }
    }

    void SavePullHistoryToFile()
    {
        string itemName = instantiatedItem.name.Replace("(Clone)", "");
        string path = Application.dataPath + "/PullHistory.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(itemName);
        writer.Close();

        characterDB.UnlockCharacterByName(itemName);
    }
}
