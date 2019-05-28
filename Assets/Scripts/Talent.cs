using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent
{
    private static float generateNormalDistribution(float expectation, float radius) {
        float ret = Random.Range(expectation - radius, expectation);
        return ret + Random.Range(0, radius);
    }
    private static float floorOf(float x) {
        return (float)((int)x);
    }
    public static Talent generateTalent() {
        //return a randomly generated talent
        //generate name
        Talent _talent = new Talent();

        int lenOfName = Random.Range(3, 6);
        string _name = "" + (char)Random.Range(65, 90); //'A' - 'Z'
        for (int i = 1; i < lenOfName; ++i) {
            _name += (char)Random.Range(97, 122); //'a' - 'z'
        }
        _talent.name = _name;
        //generate other value
        float _charm = floorOf(generateNormalDistribution(50f, 50f)),
            _capacity = floorOf(generateNormalDistribution(50f, 50f));
        _talent.capacity = _capacity;
        _talent.charm = _charm;

        _talent.satisfaction = 50f;
        _talent.salary = _charm + _capacity + floorOf(generateNormalDistribution(50, 25));
        return _talent;
    }
    public string name { get; set; }
    public Building workPlace = null;
    private float _satisfaction;
    public float satisfaction
    {
        get
        {
            return _satisfaction;
        }
        set
        {
            if (value > 100)
                value = 100;
            if (value < 0)
                value = 0;
            _satisfaction = value;
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
            return _capacity;
        }
        set
        {
            if (value > 100)
                value = 100;
            if (value < 0)
                value = 0;
            _capacity = value;
        }
    }

    private float _charm;//charm will increase Block's reputation
    public float charm
    {
        get
        {
            return _charm;
        }
        set
        {
            if (value > 100)
                value = 100;
            if (value < 0)
                value = 0;
            _charm = value;
        }
    }
}
