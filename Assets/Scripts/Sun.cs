using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    float delta, day, night;
    // Start is called before the first frame update
    private void Awake() {

    }
    void Start()
    {
        day = Timer.dayMinute / 3.0f;
        night = Timer.nightMinute / 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        delta = Timer.delta;
        if (this.transform.localEulerAngles.x <= 180.0f) //day
        {
            this.transform.Rotate(delta / day, 0, 0);
        }
        else //night
        {
            this.transform.Rotate(delta / night, 0, 0);
        }
    }
}
