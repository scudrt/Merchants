using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Population : MonoBehaviour{
    //public data
    public static int amount = 10000; //size of population in this city

    //config data
    public static float naturalBirthRate { get; set; }

    private static int currentHour = 12;
    void Start(){
        currentHour = Timer.hour;
    }
    void Update(){
        if (currentHour != Timer.hour) {
            naturalBirthRate = City.generateNormalDistribution(0.015f, 0.04f);
            amount = (int)(amount * (1 + naturalBirthRate));
            currentHour = Timer.hour;
        }
    }
    public static bool setPopulation(int count) {
        //return true if legal
        if (count < 0) {
            return false;
        }
        return true;
    }
    public static bool changePopulation(int delta) {
        return true;
    }
}
