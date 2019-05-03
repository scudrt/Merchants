using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    float delta, day, night;
    // Start is called before the first frame update
    void Start()
    {
        day = TimerScript.dayMinute;
        night = TimerScript.nightMinute;
    }

    // Update is called once per frame
    void Update()
    {
        delta = TimerScript.delta;
        if (this.transform.localEulerAngles.x <= 180.0f) //day
        {
            this.transform.Rotate(delta * 3.0f / day, 0, 0);
        }
        else //night
        {
            this.transform.Rotate(delta * 3.0f / night, 0, 0);
        }
    }
}
