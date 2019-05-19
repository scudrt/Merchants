using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static bool isActivate = false; //pause and continue switch
    public static float lastSecond = 0.0f, delta = 0.0f;
    public static float dayMinute = 0.6f, nightMinute = 0.4f; //(day + night) minutes per day

    //private data
    float sumMinute;
    float second;
    int day, hour, minute;
    // Start is called before the first frame update
    void Start()
    {
        day = 0;
        hour = 12;
        minute = 0;
        second = 0.0f;
        sumMinute = dayMinute + nightMinute;
        this.transform.position = new Vector3(UnityEngine.Screen.width / 2, UnityEngine.Screen.height - 30, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivate == false) //pause time lapsing
        {
            lastSecond = Time.time;
            return;
        }
        else
        {
            delta = Time.time - lastSecond;
            lastSecond = Time.time;
        }

        second += (delta / sumMinute * 1440.0f);
        minute += (int)(second / 60.0f); second = second - Mathf.Floor(second / 60.0f) * 60.0f;
        hour += minute / 60; minute %= 60;
        day += hour / 24; hour %= 24;
        string timeString = "Day " + day/10 + day%10 + " "
            + hour/10 + hour%10 + ":" + 
            minute/10 + minute%10 + ":" + 
            (int)second/10 + (int)second%10;
        this.GetComponent<UnityEngine.UI.Text>().text = timeString;
    }
}
