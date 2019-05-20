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
            if (value < 0)
            {
                return;
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
            if (value >= 100 || value < 0)
                return;
            capacity = value;
        }
        get { return capacity; }
    }

    public float advertising//advertising will increase Block's reputation
    {
        set
        {
            if (value >= 100 || value < 0)
                return;
            advertising = value;
        }
        get { return advertising; }
    }

    public float effort//effort will increase quality of service
    {
        set
        {
            if (value > 100 || value < 0)
                return;
            effort = value;
        }
        get { return effort; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
