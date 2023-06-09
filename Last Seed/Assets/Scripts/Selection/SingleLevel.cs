using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleLevel : MonoBehaviour
{
    public int currentStarsNum = 0;
    public int levelIndex;

    public void BackButton()
    {
        SceneManager.LoadScene("Character Selection");
    }

    public void PressStarButton(int _starsNum)
    {
        currentStarsNum = _starsNum;

        if(currentStarsNum > PlayerPrefs.GetInt("Lv" + levelIndex))
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, _starsNum);
            SceneManager.LoadScene("Character Selection");
        }
        //PlayerPrefs.SetInt("Lv" + levelIndex, _starsNum);
        //BackButton();
        Debug.Log(PlayerPrefs.GetInt("Lv" + levelIndex, _starsNum));
        SceneManager.LoadScene("Character Selection");
    }
}
