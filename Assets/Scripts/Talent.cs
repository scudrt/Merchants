using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent
{

    public double satisfication
    {
        set
        {
            if (value > 100 || satisfication < 0)
                return;
            satisfication = value;
        }
        get { return satisfication; }
    }

    public double salary
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
    public double management//management will increase speed of serving a customer
    {
        set
        {
            if (value >= 100 || value < 0)
                return;
            management = value;
        }
        get { return management; }
    }

    public double advertising//advertising will increase building's reputation
    {
        set
        {
            if (value >= 100 || value < 0)
                return;
            advertising = value;
        }
        get { return advertising; }
    }

    public double effort//effort will increase quality of service
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
