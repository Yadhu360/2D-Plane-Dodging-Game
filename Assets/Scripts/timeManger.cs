using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeManger : MonoBehaviour
{
    public static float timeToDisplay;
    public Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        timeToDisplay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlaneBehaviour.isGameOver)
        {
            timeToDisplay += Time.deltaTime;
            DisplayTime(timeToDisplay);
        }
        
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
