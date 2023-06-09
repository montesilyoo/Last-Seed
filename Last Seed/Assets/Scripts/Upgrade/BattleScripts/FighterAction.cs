using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAction : MonoBehaviour
{   
    private GameObject enemy;
    private GameObject hero;

    [SerializeField]
    private GameObject basicPrefab;

    [SerializeField]
    private GameObject skillPrefab;

    [SerializeField]
    private Sprite faceIcon;

    private GameObject currentAttack;
    private GameObject basicAttack;
    private GameObject skillAttack;

    void Awake() 
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void SelectAttack(string btn)
    {
        GameObject victim = hero;
        if(tag == "Hero")
        {
            victim = enemy;
        }
        if (btn.CompareTo("basic") == 0)
        {
            basicPrefab.GetComponent<AttackScript>().Attack(victim);
        }
        else if (btn.CompareTo("skill") == 0)
        {
            skillPrefab.GetComponent<AttackScript>().Attack(victim);
        }
        else if (btn.CompareTo("run") == 0)
        {
            Debug.Log("Run");
        }
    }
}

