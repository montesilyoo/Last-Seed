using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiTween : MonoBehaviour
{

    public GameObject GachaBag;
    public GameObject cardExpand;
    bool isButtonClicked = false;

    void Start()
    {
        LeanTween.rotateZ(GachaBag, 15, .5f).setEaseInOutSine().setLoopPingPong();
    }

    public void ExpandCards()
    {
        if(isButtonClicked == false)
        {
            LeanTween.moveX(cardExpand, -5.5f, .5f);
            isButtonClicked = true;
        }
        else
        {
            LeanTween.moveX(cardExpand, -12.5f, .5f);
            isButtonClicked = false;
        }

    }

}
