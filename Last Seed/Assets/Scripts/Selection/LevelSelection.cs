using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked; //default value = false
    public Image unlockImage;
    //public GameObject[] stars;

    private void Update() 
    {
        UpdateLevelImage();
        UpdateLevelStatus();
    }

    private void UpdateLevelStatus()
    {
        int previousLevelNum = int.Parse(gameObject.name) - 1;
        if (PlayerPrefs.GetInt("Lv" + previousLevelNum.ToString()) > 0)
        {
            unlocked = true;
        }
    }

    private void UpdateLevelImage()
    {
        if(!unlocked)//marker if unlock is false means lvl is locked
        {
            unlockImage.gameObject.SetActive(true);
            //for(int i=0; 1< stars.Length; i++)
            //{
            //        stars[i].gameObject.SetActive(false);
            //}
        }
        else//if unlock is true u can play
        {
            unlockImage.gameObject.SetActive(false);
            //for (int i = 0; 1 < stars.Length; i++)
            //{
            //    stars[i].gameObject.SetActive(true);
            //}
        }
    }

    public void PressSelection(string _LeveName)
    {
        if(unlocked)
        {
            SceneManager.LoadScene(_LeveName);
        }
    }

}
