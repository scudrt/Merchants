using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public static float lastSecond = 0.0f, delta = 0.0f;
    public static float dayMinute = 0.6f, nightMinute = 0.4f; //(day + night) minutes per day

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
    }

    // Update is called once per frame
    void Update()
    {
        delta = Time.time - lastSecond;
        lastSecond = Time.time;
        
        second += (delta / sumMinute * 1440.0f);
        minute += (int)(second / 60.0f); second = second - Mathf.Floor(second / 60.0f) * 60.0f;
        hour += minute / 60; minute %= 60;
        day += hour / 24; hour %= 24;
        string timeString = "Day " + day + " " + hour + ":" + minute + ":" + (int)second;
        this.GetComponent<UnityEngine.UI.Text>().text = timeString;
    }
}
