using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    public static int year, month, day, hour, minute;
    public static float delta = 0.0f;
    public static float dayMinute = 1.2f, nightMinute = 0.8f; //(day + night) minutes per day

    //private data
    private float sumMinute;
    private float second;
    
    // Start is called before the first frame update
    void Start()
    {
        year = 2019;
        month = 6;
        day = 1;
        hour = 12;
        minute = 0;
        second = 0.0f;
        sumMinute = dayMinute + nightMinute;
        this.transform.position = 
            new Vector3(UnityEngine.Screen.width / 2, UnityEngine.Screen.height - 30, 10);
    }

    // Update is called once per frame
    void Update()
    {
        delta = Time.deltaTime;

        second += (delta / sumMinute * 1440.0f);
        minute += (int)(second / 60.0f); second = second - Mathf.Floor(second / 60.0f) * 60.0f;
        hour += minute / 60; minute %= 60;
        day += hour / 24; hour %= 24;
        if (day > 30) {
            month += day / 30; day = (day - 1) % 30 + 1;
            year += month / 12; month = (month - 1) % 12 + 1;
        }
        string timeString = year + "年" + month.ToString().PadLeft(2, '0') + "月" 
            + day.ToString().PadLeft(2, '0') + "日 "
            + hour.ToString().PadLeft(2, '0') + ":" +
            minute.ToString().PadLeft(2, '0');
        this.GetComponent<UnityEngine.UI.Text>().text = timeString;
    }
}
