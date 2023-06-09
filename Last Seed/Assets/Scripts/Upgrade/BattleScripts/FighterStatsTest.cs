using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterStatsTest : MonoBehaviour
{
    
    [SerializeField] Animator anim;

    [Header("Stats")]
    public float level;
    public float health;
    //public float magic;
    public float attack;
    //public float magicRange;
    public float defense;
    public float speed;
    //public float experience;

    public bool isDefending = false;
    public bool isStunned = false;
    public bool isAOEHealer = false;
    public bool isAOEDamage = false;
    public bool isDPS = false;

}
