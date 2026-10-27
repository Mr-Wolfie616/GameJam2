using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // These feilds allow us to edit in the inspector in unity
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime = 60f ;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // This code tells itself if remainingTime is bigger then 0 carry on
        if (remainingTime > 0 )
        {
            remainingTime -= Time.deltaTime;
        }
        // This tells the code the if it isnt bigger then 0 make time 0 and 
        // log Game Over in the console
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            Debug.Log ("Game Over");
        }
        // this piece of code creates the integers for minutes and seconds and 
        //formats them into a timer
        int Minutes = Mathf.FloorToInt(remainingTime / 60);
        int Seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", Minutes, Seconds);
    }
}