using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour{
    //public data
    public static int amount;//size of population in this city

    //config data
    public static float naturalBirthRate { get; set; }

    private static int currentDay = Timer.day;
    void Start(){
        currentDay = Timer.day;
        amount = (int)City.generateNormalDistribution(100000f, 20000f);
    }
    void Update(){
        if (currentDay != Timer.day) {
            //scale the annual growth rate to a day
            naturalBirthRate = City.generateNormalDistribution(0.015f, 0.04f) / 365;
            amount = (int)(amount * (1 + naturalBirthRate));
            currentDay = Timer.day;
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
