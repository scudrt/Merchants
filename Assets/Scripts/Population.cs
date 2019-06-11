using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour{
    //public data
    private static int _amount = 100000;
    public static int amount { get {
            return _amount;
        }
        set {
            _amount = value;
            if (_amount < 0) {
                _amount = 0;
            }
        }
    }
    public static float avgGDP = 1000f;
    public static float GDP {
        get {
            return avgGDP * amount;
        }set {
            avgGDP = value / amount;
            if (avgGDP < 0f) {
                avgGDP = 0f;
            }
        }
    }

    //config data
    public static float annualBirthRateExpect, GDPGrowthRate;
    private static float dailyBirthRate, dailyGDPGrowthRate;

    private static int currentDay = Timer.day;
    void Start() {
        //initialise the amount of population and the average GDP
        currentDay = Timer.day;
        amount = (int)City.generateNormalDistribution(100000f, 20000f);
        avgGDP = City.generateNormalDistribution(1000f, 150f);

        annualBirthRateExpect = 0.015f;
        GDPGrowthRate = 0.018f;
        dailyBirthRate = annualBirthRateExpect / 365f;
        dailyGDPGrowthRate = GDPGrowthRate / 365f;
    }
    void Update(){
        if (currentDay != Timer.day) {
            //scale the annual growth rate to a day
            amount = (int)(amount * (1 + dailyBirthRate));
            GDP *= 1 + dailyGDPGrowthRate;
            currentDay = Timer.day;
        }
    }
}
