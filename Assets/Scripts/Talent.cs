using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent
{
    public string name { get; set; }
    public Building workPlace;
    private float _satisfication;
    public float satisfication
    {
        get
        {
            return _satisfication;
        }
        set
        {
            if (value > 100)
                value = 100;
            if (value < 0)
                value = 0;
            _satisfication = value;
        }
    }
    
    private float _salary;
    public float salary
    {
        get
        {
            return _salary;
        }
        set
        {
            if (value < 0)
                value = 0;
            _salary = value;
        }
    }

    private float _capacity;//capacity will increase speed of serving a customer
    public float capacity
    {
        get
        {
            return _satisfication;
        }
        set
        {
            if (value > 100)
                value = 100;
            if (value < 0)
                value = 0;
            _satisfication = value;
        }
    }

    private float _charm;//charm will increase Block's reputation
    public float charm
    {
        get
        {
            return _satisfication;
        }
        set
        {
            if (value > 100)
                value = 100;
            if (value < 0)
                value = 0;
            _satisfication = value;
        }
    }
}
