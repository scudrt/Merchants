using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    public static int year, month, day, hour;
    public static float delta = 0.0f;
    public static float dayMinute = 1.2f, nightMinute = 0.8f; //(day + night) minutes per day

    //private data
    private static int[] dayOn = {0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
    private float sumMinute;
    private float minute;

    public bool isCommonYearNow {
        get {
            return ((year & 3) != 0) || ((year % 100 == 0) && (year % 400 != 0));
        }
        private set { }
    }
    // Start is called before the first frame update
    void Start()
    {
        year = 2019;
        month = 6;
        day = 16;
        hour = 12;
        minute = 0.0f;
        sumMinute = dayMinute + nightMinute;
        this.transform.position = 
            new Vector3(UnityEngine.Screen.width / 2, UnityEngine.Screen.height - 30, 10);
        //show time's info
        this.GetComponent<UnityEngine.UI.Text>().text = year + "年" +
            month.ToString().PadLeft(2, '0') + "月"
            + day.ToString().PadLeft(2, '0') + "日"
            + hour.ToString().PadLeft(2, '0') + "时";
    }

    // Update is called once per frame
    void Update()
    {
        delta = Time.deltaTime;
        
        minute += (delta / sumMinute * 24f);
        if (minute >= 60f) { //need to update time string
            hour += (int)(minute / 60f); minute = minute - Mathf.Floor(minute / 60.0f) * 60.0f;
            day += hour / 24; hour %= 24;
            if (day > dayOn[month]) { //update the date
                if (!this.isCommonYearNow && day == 29) { //not common year and is on Feburary
                } else {
                    ++month; day = 1;
                    year += month / 12; month = (month - 1) % 12 + 1;
                }
            }
            //update time string
            this.GetComponent<UnityEngine.UI.Text>().text = year + "年" +
                month.ToString().PadLeft(2, '0') + "月"
            + day.ToString().PadLeft(2, '0') + "日"
            + hour.ToString().PadLeft(2, '0') + "时";
        }
    }
}
