using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using System.Collections.Generic;
using System;

public class GameControllerTest : MonoBehaviour
{
     [SerializeField] private GameObject player1 = null;
     [SerializeField] private GameObject player2 = null;
     [SerializeField] private GameObject player3 = null;
     [SerializeField] private GameObject enemy1 = null;
     [SerializeField] private GameObject enemy2 = null;
     [SerializeField] private GameObject enemy3 = null;
     [SerializeField] private Slider playerHealth = null;
     [SerializeField] private Slider player2Health = null;
     [SerializeField] private Slider player3Health = null;
     [SerializeField] private Slider enemy1Health = null;
     [SerializeField] private Slider enemy2Health = null;
     [SerializeField] private Slider enemy3Health = null;
     [SerializeField] private Slider skillGauge = null;
     [SerializeField] private Slider p2skillGauge = null;
     [SerializeField] private Slider p3skillGauge = null;
     [SerializeField] private Button Attackbtn = null;
     [SerializeField] private Button Skillbtn = null;
     [SerializeField] private Button Defendbtn = null;
     [SerializeField] private Button Restbtn = null;
     [SerializeField] private Button Target1 = null;
     [SerializeField] private Button Target2 = null;
     [SerializeField] private Button Target3 = null;
     [SerializeField] private Button Back = null;
     [SerializeField] private Animator P1animator;
     [SerializeField] private Animator P2animator;
     [SerializeField] private Animator P3animator;
     
     [SerializeField] private Animator E1animator;
     [SerializeField] private Animator E2animator;
     [SerializeField] private Animator E3animator;

     private bool isPlayerTurn = true;
     private bool isDefending = false;
     private GameObject currentAttacker;
     private GameObject currentTarget;
     private GameObject[] Enemies;
     public List<GameObject> alliedUnits;
     private int currentUnitIndex;
     public GameObject Win;
     public GameObject GameOver;


     private void Start()
     {
          //players
          player1 = GameObject.Find("Player1");
          player2 = GameObject.Find("Player2");
          player3 = GameObject.Find("Player3");

          alliedUnits = new List<GameObject>();
          alliedUnits.Add(player1);
          alliedUnits.Add(player2);
          alliedUnits.Add(player3);
          Debug.Log(alliedUnits);

          //enemies
          enemy1 = GameObject.Find("Enemy1");
          enemy2 = GameObject.Find("Enemy2");
          enemy3 = GameObject.Find("Enemy3");

          //Animators
          P1animator = player1.GetComponent<Animator>();
          P2animator = player2.GetComponent<Animator>();
          P3animator = player3.GetComponent<Animator>();
          E1animator = enemy1.GetComponent<Animator>();
          E2animator = enemy2.GetComponent<Animator>();
          E3animator = enemy3.GetComponent<Animator>();

          //health values
          playerHealth.maxValue = player1.GetComponent<FighterStatsTest>().health;
          player2Health.maxValue = player2.GetComponent<FighterStatsTest>().health;
          player3Health.maxValue = player3.GetComponent<FighterStatsTest>().health;
          enemy1Health.maxValue = enemy1.GetComponent<FighterStatsTest>().health;
          enemy2Health.maxValue = enemy2.GetComponent<FighterStatsTest>().health;
          enemy3Health.maxValue = enemy3.GetComponent<FighterStatsTest>().health;
     
          //Listeners
          Target1.onClick.AddListener(SelectEnemy1);
          Target2.onClick.AddListener(SelectEnemy2);
          Target3.onClick.AddListener(SelectEnemy3);
          Back.onClick.AddListener(GoBack);

          //Win = GameObject.Find("Win");
          //GameOver = GameObject.Find("GameOver");

          currentUnitIndex = 0;
          StartNextTurn();    
     }

     private void StartNextTurn()
     {
          if (currentUnitIndex >= alliedUnits.Count)
          {
               Debug.Log("All units have taken their turns.");
               ChangeTurn();
               return;
          }
          CheckforVictory();
          currentAttacker = alliedUnits[currentUnitIndex];
          Debug.Log("It's " + currentAttacker.name + "'s turn.");     

          if (currentAttacker.name == "Player1")
               {
                    if (skillGauge.value < 5)
                    {
                         Skillbtn.interactable = false;
                    }
                    else
                    {
                         Skillbtn.interactable = true;
                    }
               }
          else if (currentAttacker.name == "Player2")
               {
                    if (p2skillGauge.value < 5)
                    {
                         Skillbtn.interactable = false;
                    }
                    else
                    {
                         Skillbtn.interactable = true;
                    }
               }
          else if (currentAttacker.name == "Player3")
               {
                    if (p3skillGauge.value < 5)
                    {
                         Skillbtn.interactable = false;
                    }
                    else
                    {
                         Skillbtn.interactable = true;
                    }
               }
          Attackbtn.interactable = true;
          Defendbtn.interactable = true;
          Restbtn.interactable = true;
     }

     private void Attack(GameObject target)
     {    
          if ((target == enemy1 || (target == enemy2) || (target == enemy3)))
          {
               if (target == enemy1)
               {
                    enemy1Health.value -= (currentAttacker.GetComponent<FighterStatsTest>().attack - currentTarget.GetComponent<FighterStatsTest>().defense);
                    if (currentAttacker.name == "Player1")
                    {
                         skillGauge.value ++;
                         P1animator.Play("Attack");
                    }
                    else if (currentAttacker.name == "Player2")
                    {
                         p2skillGauge.value ++;
                         P2animator.Play("Attack");
                    }
                    else if (currentAttacker.name == "Player3")
                    {
                         p3skillGauge.value ++;
                         P3animator.Play("Attack");
                    }

                    E1animator.Play("Damaged");
                    currentUnitIndex++;
                    StartNextTurn();

                    if(enemy1Health.value == 0)
                    {
                        Destroy(enemy1);
                        enemy1Health.gameObject.SetActive(false);
                        CheckforVictory();
                    }
               }
               if (target == enemy2)
               {
                    enemy2Health.value -= (currentAttacker.GetComponent<FighterStatsTest>().attack - currentTarget.GetComponent<FighterStatsTest>().defense);
                    if (currentAttacker.name == "Player1")
                    {
                         skillGauge.value ++;
                         P1animator.Play("Attack");
                    }
                    else if (currentAttacker.name == "Player2")
                    {
                         p2skillGauge.value ++;
                         P2animator.Play("Attack");
                    }
                    else if (currentAttacker.name == "Player3")
                    {
                         p3skillGauge.value ++;
                         P3animator.Play("Attack");
                    }

                    E2animator.Play("Damaged");
                    currentUnitIndex++;
                    StartNextTurn();

                    if(enemy2Health.value == 0)
                    {
                        Destroy(enemy2);
                        enemy2Health.gameObject.SetActive(false);
                        CheckforVictory();
                    }
               }
               
               if (target == enemy3)
               {
                    enemy3Health.value -= (currentAttacker.GetComponent<FighterStatsTest>().attack - currentTarget.GetComponent<FighterStatsTest>().defense);
                    if (currentAttacker.name == "Player1")
                    {
                         skillGauge.value ++;
                         P1animator.Play("Attack");
                    }
                    else if (currentAttacker.name == "Player2")
                    {
                         p2skillGauge.value ++;
                         P2animator.Play("Attack");
                    }
                    else if (currentAttacker.name == "Player3")
                    {
                         p3skillGauge.value ++;
                         P3animator.Play("Attack");
                    }

                    E3animator.Play("Damaged");
                    currentUnitIndex++;
                    StartNextTurn();

                    if(enemy3Health.value == 0)
                    {
                        Destroy(enemy3);
                        enemy3Health.gameObject.SetActive(false);
                        CheckforVictory();
                    }
               }
               CheckforVictory();
          }
          else
          {
               if (currentTarget.name == "Player1")
               {
                    if(currentTarget.GetComponent<FighterStatsTest>().isDefending == true)
                    {
                         playerHealth.value -= ((currentAttacker.GetComponent<FighterStatsTest>().attack - currentTarget.GetComponent<FighterStatsTest>().defense)/ 2);    
                         currentTarget.GetComponent<FighterStatsTest>().isDefending = false;
                    }
               else 
                    playerHealth.value -= currentAttacker.GetComponent<FighterStatsTest>().attack- currentTarget.GetComponent<FighterStatsTest>().defense;
               }
               
               if (currentTarget.name == "Player2")
               {
                    if(currentTarget.GetComponent<FighterStatsTest>().isDefending == true)
                    {
                         player2Health.value -= ((currentAttacker.GetComponent<FighterStatsTest>().attack - currentTarget.GetComponent<FighterStatsTest>().defense)/ 2);    
                         currentTarget.GetComponent<FighterStatsTest>().isDefending = false;
                    }
               else 
                    player2Health.value -= currentAttacker.GetComponent<FighterStatsTest>().attack- currentTarget.GetComponent<FighterStatsTest>().defense;
               }
               
               if (currentTarget.name == "Player3")
               {
                    if(currentTarget.GetComponent<FighterStatsTest>().isDefending == true)
                    {
                         player3Health.value -= ((currentAttacker.GetComponent<FighterStatsTest>().attack - currentTarget.GetComponent<FighterStatsTest>().defense)/ 2);    
                         currentTarget.GetComponent<FighterStatsTest>().isDefending = false;
                    }
               else 
                    player3Health.value -= currentAttacker.GetComponent<FighterStatsTest>().attack- currentTarget.GetComponent<FighterStatsTest>().defense;
               }
          }
     }

   private void AOEHeal(GameObject target, float amount)
     {
          if (currentAttacker.name == "Player1")
               {
                    P1animator.Play("Skill");
                    skillGauge.value = 0;  
               }
          else if (currentAttacker.name == "Player2")
               {
                    P2animator.Play("Skill");
                    p2skillGauge.value = 0;  
               }
          else if (currentAttacker.name == "Player3")
               {
                    P3animator.Play("Skill");
                    p3skillGauge.value = 0;  
               }
               playerHealth.value += amount;
               player2Health.value += amount;
               player3Health.value += amount;

          currentUnitIndex++;
          StartNextTurn();
     }

     private void AOEDamage(GameObject target, float amount)
     {
          if (currentAttacker.name == "Player1")
               {
                    P1animator.Play("Skill");
                    skillGauge.value = 0;  
               }
          else if (currentAttacker.name == "Player2")
               {
                    P2animator.Play("Skill");
                    p2skillGauge.value = 0;  
               }
          else if (currentAttacker.name == "Player3")
               {
                    P3animator.Play("Skill");
                    p3skillGauge.value = 0;  
               }

               enemy1Health.value -= amount;
               enemy2Health.value -= amount;
               enemy3Health.value -= amount;
               E1animator.Play("Damaged");
               E2animator.Play("Damaged");
               E3animator.Play("Damaged");

               if(enemy1Health.value == 0)
                    {
                        Destroy(enemy1);
                        enemy1Health.gameObject.SetActive(false);
                    }
               if(enemy2Health.value == 0)
                    {
                        Destroy(enemy2);
                        enemy2Health.gameObject.SetActive(false);
                    }
               if(enemy3Health.value == 0)
                    {
                        Destroy(enemy3);
                        enemy3Health.gameObject.SetActive(false);
                    }


          currentUnitIndex++;
          StartNextTurn();
     }

     private void Defend()
     {
          currentAttacker.GetComponent<FighterStatsTest>().isDefending = true;
          if (currentAttacker.name == "Player1")
               {
                    skillGauge.value ++;
               }
          else if (currentAttacker.name == "Player2")
               {
                    p2skillGauge.value ++;  
               }
          else if (currentAttacker.name == "Player3")
               {
                    p3skillGauge.value ++;  
               }

          currentUnitIndex++;
          StartNextTurn();
     }

     private void Rest()
     {    
          if (currentAttacker.name == "Player1")
               {
                   skillGauge.value += 2;
               }
          else if (currentAttacker.name == "Player2")
               {
                    p2skillGauge.value += 2;
               }
          else if (currentAttacker.name == "Player3")
               {
                    p3skillGauge.value += 2;
               }

          currentUnitIndex++;
          StartNextTurn();
     }

     public void BtnAttack()
     {
          //interact on
           if(enemy1Health.value == 0)
          {
              Target1.interactable = false;
          }
          else
               Target1.interactable = true;
          
          if(enemy2Health.value == 0)
          {
              Target2.interactable = false;
          }
          else
               Target2.interactable = true;
          if(enemy3Health.value == 0)
          {
              Target3.interactable = false;
          }
          else
               Target3.interactable = true;

          Back.interactable = true;

          //hide off
          Target1.gameObject.SetActive(true);
          Target2.gameObject.SetActive(true);
          Target3.gameObject.SetActive(true);
          Back.gameObject.SetActive(true);

          //uninteract other buttons
          Skillbtn.interactable = false;
          Defendbtn.interactable = false;
          Restbtn.interactable = false;
          //Attack(enemy1);
          //P1animator.Play("attack");
     }

     public void SelectEnemy(int enemyNumber)
     {
          switch (enemyNumber)
          {
               case 1:
                    currentTarget = enemy1;
                    Attack(enemy1);
                    break;
               case 2:
                    currentTarget = enemy2;
                    Attack(enemy2);
                    break;
               case 3:
                    currentTarget = enemy3;
                    Attack(enemy3);
                    break;
               default:
               break;
          }

          Target1.gameObject.SetActive(false);
          Target2.gameObject.SetActive(false);
          Target3.gameObject.SetActive(false);
          Back.gameObject.SetActive(false);
     }

     public void SelectEnemy1()
     {
          SelectEnemy(1);
     }
     public void SelectEnemy2()
     {
          SelectEnemy(2);
     }
     public void SelectEnemy3()
     {
          SelectEnemy(3);
     }
     public void BtnSkill()
     {
          if (currentAttacker.GetComponent<FighterStatsTest>().isAOEHealer == true)
               
               AOEHeal(player1,(currentAttacker.GetComponent<FighterStatsTest>().health /2));

          else if (currentAttacker.GetComponent<FighterStatsTest>().isAOEDamage == true)
               AOEDamage(player1,Convert.ToInt32(currentAttacker.GetComponent<FighterStatsTest>().attack *1.2));

          CheckforVictory();
     }

     public void BtnDefend()
     {
        Defend();
     }

     public void BtnRest()
     {
        Rest();
     }

     public void GoBack()
     {
          //disable target selection and reenable other buttons
          //interact on
          Target1.interactable = false;
          Target2.interactable = false;
          Target3.interactable = false;
          Back.interactable = false;

          //hide off
          Target1.gameObject.SetActive(false);
          Target2.gameObject.SetActive(false);
          Target3.gameObject.SetActive(false);
          Back.gameObject.SetActive(false);

          Attackbtn.interactable = true;
          Defendbtn.interactable = true;
          Restbtn.interactable = true;
     }

     private void ChangeTurn()
     {

          CheckforVictory();
          isPlayerTurn = !isPlayerTurn;

     if(!isPlayerTurn)
          {
               StartCoroutine(EnemyTurn());
          }
     }

     private IEnumerator EnemyTurn()
     {
          Attackbtn.interactable = false;
          Skillbtn.interactable = false;
          Defendbtn.interactable = false;
          Restbtn.interactable = false;
          GameObject[] Enemies = {enemy1, enemy2, enemy3};
          var low = 0;
          var up = alliedUnits.Count;

          if(enemy1Health.value != 0)
          {
               yield return new WaitForSeconds(2);
               currentAttacker = enemy1;
               currentTarget = alliedUnits[UnityEngine.Random.Range(low,up)];
               Attack(currentTarget);
               E1animator.Play("Attack");
               if (currentTarget.name == "Player1")
                    {   
                         P1animator.Play("Damaged");
                    }
               else if (currentTarget.name == "Player2")
                    {
                         P2animator.Play("Damaged");
                    }
               else if (currentTarget.name == "Player3")
                    {
                         P3animator.Play("Damaged");
                    }

          }

          if(enemy2Health.value != 0)
          {
               yield return new WaitForSeconds(2);
               currentAttacker = enemy2;
               currentTarget = alliedUnits[UnityEngine.Random.Range(low,up)];
               Debug.Log(currentTarget);
               Attack(currentTarget);
               E2animator.Play("Attack");
               if (currentTarget.name == "Player1")
                    {   
                         P1animator.Play("Damaged");
                    }
               else if (currentTarget.name == "Player2")
                    {
                         P2animator.Play("Damaged");
                    }
               else if (currentTarget.name == "Player3")
                    {
                         P3animator.Play("Damaged");
                    }
          }

          if(enemy3Health.value != 0)
          {
               yield return new WaitForSeconds(2);
               currentAttacker = enemy3;
               currentTarget = alliedUnits[UnityEngine.Random.Range(low,up)];
               Attack(currentTarget);
               E3animator.Play("Attack");
               if (currentTarget.name == "Player1")
                    {   
                         P1animator.Play("Damaged");
                    }
               else if (currentTarget.name == "Player2")
                    {
                         P2animator.Play("Damaged");
                    }
               else if (currentTarget.name == "Player3")
                    {
                         P3animator.Play("Damaged");
                    }
          }

          if(playerHealth.value == 0)
          {
               Destroy(player1);
               playerHealth.gameObject.SetActive(false);
               skillGauge.gameObject.SetActive(false);
               alliedUnits.Remove(player1);
          }
          if(player2Health.value == 0)
          {
               Destroy(player2);
               player2Health.gameObject.SetActive(false);
               p2skillGauge.gameObject.SetActive(false);
               alliedUnits.Remove(player2);
          }
          if(player3Health.value == 0)
          {
               Destroy(player3);
               player3Health.gameObject.SetActive(false);
               p3skillGauge.gameObject.SetActive(false);
               alliedUnits.Remove(player3);
          }
          
          CheckforVictory();
          currentUnitIndex = 0;
          ChangeTurn();
          StartNextTurn();
     }

    private void CheckforVictory()
     {
          if ((enemy1Health.value == 0) && (enemy2Health.value == 0) && (enemy3Health.value == 0))
          {
               UnityEngine.Debug.Log("You Win");
               Win.gameObject.SetActive(true);
          }
               
          else if ((playerHealth.value == 0) && (player2Health.value == 0) && (player3Health.value == 0))
          {
               UnityEngine.Debug.Log("You Lose");
               GameOver.gameObject.SetActive(true);
          }
               
     }
}
