using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelSystem : MonoBehaviour
{
    private int XPNow;
    public static int Level;
    private int xpToNext;

    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject lvlWindowPrefab;

    [SerializeField] private Slider XPSlider;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private Image starImage;

    private static bool initialized;
    private static Dictionary<int, int> xpToNextLevel = new Dictionary<int, int>();
    private static Dictionary<int, int[]> lvlReward = new Dictionary<int, int[]>();

    private void Awake()
    {
        if(!initialized)
        {
            Initialize();
        }

        xpToNextLevel.TryGetValue(Level, out xpToNext);
    }

    private static void Initialize()
    {
        try
        {
            //path to the csv file
            string path = "Leveling";

            TextAsset textAsset = Resources.Load<TextAsset>(path);
            string[] lines = textAsset.text.Split('\n');

            xpToNextLevel = new Dictionary <int, int>(lines.Length - 1);

            for(int i = 1; i < lines[i].Length - 1; i++)
            {
                string[] columns = lines[i].Split(',');

                int lvl = -1;
                int xp = -1;
                int curr1 = -1;
                int curr2 = -1;

                int.TryParse(columns[0], out lvl);
                int.TryParse(columns[1], out xp);
                int.TryParse(columns[2], out curr1);
                int.TryParse(columns[3], out curr2);

                if(lvl >=0 && xp > 0)
                {
                    if(!xpToNextLevel.ContainsKey(lvl))
                    {
                        xpToNextLevel.Add(lvl, xp);
                        lvlReward.Add(lvl, new[]{curr1, curr2});
                    }
                }
                
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

        initialized = true;
    }

    void Start()
    {
        EventManager.Instance.AddListener<XPAddedGameEvent>(OnXPAdded);
        EventManager.Instance.AddListener<LevelChangedGameEvent>(OnLevelChanged);

        UpdateUI();
    }


    private void UpdateUI()
    {
        float fill = (float)XPNow / xpToNext;
        XPSlider.value = fill;
        xpText.text = XPNow + "/" + xpToNext;
    }

    private void OnXPAdded(XPAddedGameEvent info)
    {
        XPNow += info.amount;

        UpdateUI();

        if(XPNow >= xpToNext)
        {
            Level++;
            Debug.Log(Level);
            LevelChangedGameEvent levelChange = new LevelChangedGameEvent(Level);
            EventManager.Instance.QueueEvent(levelChange);
        }
    }

    private void OnLevelChanged(LevelChangedGameEvent info)
    {
        XPNow -= xpToNext;
        Debug.Log(info.newLvl);
        xpToNext = xpToNextLevel[info.newLvl];
        lvlText.text = (info.newLvl + 1).ToString();
        UpdateUI();

        GameObject window = Instantiate(lvlWindowPrefab, GameManager.current.canvas.transform);
    
        //initialize texts and images here

        window.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
        {
            Destroy(window);
        });

        CurrencyChangeGameEvent currencyInfo = 
            new CurrencyChangeGameEvent(lvlReward[info.newLvl][0], CurrencyType.Coins);
        EventManager.Instance.QueueEvent(currencyInfo);

        currencyInfo = 
            new CurrencyChangeGameEvent(lvlReward[info.newLvl][1], CurrencyType.Crystals);
        EventManager.Instance.QueueEvent(currencyInfo);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    
}
