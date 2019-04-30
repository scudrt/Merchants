using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    float lastTime;
    float dayMinute; //x minutes per day
    // Start is called before the first frame update
    void Start()
    {
        lastTime = 0.0f;
        dayMinute = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.time - lastTime;
        lastTime = Time.time;
        this.transform.Rotate(delta * 6.0f / dayMinute, 0, 0);
    }
}
