using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour{
    //public data
    public static int totalAmount = 0;
    public static int[] amountAtAge;

    //config data
    private static int AGELIMIT = 100;
    public static float birthRate { get; set; }
    public static float deathRate { get; set; }

    // Start is called before the first frame update
    void Start(){
        birthRate = 0.03f;
        deathRate = 0.01f;
        amountAtAge = new int[AGELIMIT + 1];
        for (int i = 0; i <= AGELIMIT; ++i){
            amountAtAge[i] = 0;
        }
    }
    void Update(){
        
    }
    public static bool setPopulation(int age, int count) {
        //return true if legal
        if (age < 0 || age > AGELIMIT || count < 0) {
            return false;
        }
        amountAtAge[age] = count;
        return true;
    }
    public static bool changePopulation(int age, int delta) {
        if (age < 0 || age > AGELIMIT || amountAtAge[age]+delta < 0) {
            return false;
        }
        amountAtAge[age] += delta;
        return true;
    }
}
