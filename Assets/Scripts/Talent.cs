using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent
{

    public float satisfication
    {
        set
        {
            if (value > 100 || satisfication < 0)
                return;
            satisfication = value;
        }
        get { return satisfication; }
    }

    public float salary
    {
        set
        {
            if (value < 0) {
                value = 0f;
            }
            salary = value;
        }
        get { return salary; }
    }

    //talent's qualities
    public float capacity//capacity will increase speed of serving a customer
    {
        set
        {
            if (value > 100) {
                value = 100;
            }else if (value < 0) {
                value = 0;
            }
            capacity = value;
        }
        get { return capacity; }
    }

    public float charm//charm will increase Block's reputation
    {
        set {
            if (value > 100) {
                value = 100;
            } else if (value < 0) {
                value = 0;
            }
            charm = value;
        }
        get { return charm; }
    }
    
}
