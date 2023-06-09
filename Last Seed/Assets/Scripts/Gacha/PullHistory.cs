using UnityEngine;
using System.IO;

[System.Serializable]
public class PullRecord
{
    public string itemName;
    public System.DateTime pullTime;

    public PullRecord(string itemName)
    {
        this.itemName = itemName;
        this.pullTime = System.DateTime.Now;
    }
}

public class PullHistory : MonoBehaviour
{
    private string filePath;
    private int pullCount;
    private StreamWriter writer;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/pull_history.txt";
        pullCount = 0;
        writer = new StreamWriter(filePath, true);
        writer.WriteLine("=== New Session ===");
    }

    private void OnDestroy()
    {
        writer.Close();
    }

    public void RecordPull(GameObject item)
    {
        PullRecord record = new PullRecord(item.name);
        string json = JsonUtility.ToJson(record);
        writer.WriteLine(json);
        pullCount++;
        Debug.LogFormat("Recorded pull #{0}: {1}", pullCount, json);
    }
}
