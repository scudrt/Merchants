using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour{
    //public data
    public static int amount;//size of population in this city

    //config data
    public static float annualBirthRateExpect = 0.015f;

    private static int currentDay = Timer.day;
    void Start() {
        currentDay = Timer.day;
        amount = (int)City.generateNormalDistribution(100000f, 20000f);
    }
    void Update(){
        if (currentDay != Timer.day) {
            //scale the annual growth rate to a day
            float birthRate = City.generateNormalDistribution(annualBirthRateExpect, 0.04f) / 365;
            amount = (int)(amount * (1 + birthRate));
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
