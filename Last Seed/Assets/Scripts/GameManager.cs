using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    public GameObject canvas;
    
    private void Awake()
    {
        current = this;

        ShopItemDrag.canvas = canvas.GetComponent<Canvas>();
    }

    public void GetXP(int amount)
    {
        XPAddedGameEvent info = new XPAddedGameEvent(amount);
        EventManager.Instance.QueueEvent(info);
    }

    public void GetCoins(int amount)
    {
        CurrencyChangeGameEvent info = new CurrencyChangeGameEvent(amount, CurrencyType.Cheese);

        EventManager.Instance.QueueEvent(info);
    }

    public void BattleSelect(string _LevelName)
    {
        SceneManager.LoadScene(_LevelName);
    }
}
