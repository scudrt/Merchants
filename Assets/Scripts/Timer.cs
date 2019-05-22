using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float delta = 0.0f;
    public static float dayMinute = 1.2f, nightMinute = 0.8f; //(day + night) minutes per day

    //private data
    float sumMinute;
    float second;
    int day, hour, minute;

    private void Awake() {
        enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        day = 0;
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
        string timeString = "Day " + day / 10 + day % 10 + " "
            + hour / 10 + hour % 10 + ":" +
            minute / 10 + minute % 10;
        this.GetComponent<UnityEngine.UI.Text>().text = timeString;
    }
}
