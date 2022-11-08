using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteAnswer = 20f;
    [SerializeField] float timeToShowAnswer = 10f;
    float timeValue;
    public bool isTesting=true;
    public bool isPreparing=false; 
    float fillFraction;

    void Start()
    {
        timeValue=timeToCompleteAnswer;
    }
    void Update()
    {
        updateTimer();
        clockAnimation();
        //Debug.Log(timeValue);
    }
    void updateTimer()
    {
        timeValue -=Time.deltaTime;
        if(isTesting)
        {
            if(timeValue>0)
            {
                fillFraction=timeValue/timeToCompleteAnswer;
            }
            else
            {
                timeValue=timeToShowAnswer;
                isTesting=false;
                isPreparing=true;
            }
        }
        else
        {
             if(timeValue>0)
            {
                fillFraction=timeValue/timeToShowAnswer;
            }
            else
            {
                timeValue=timeToCompleteAnswer;
                isTesting=true;
                isPreparing=false;
            }

        }

    }
    public void cancelTimer()
    {
        timeValue=0;
    }
    void clockAnimation()
    {
        GetComponent<Image>().fillAmount=fillFraction;
    }
}
