using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Company companyBelong;
    public Talent workingTalent;
    public float price;
    public float earnRate;

    public string buildingType;
    public void makeMoney() {
        if (companyBelong == null) {
            return;
        }
        companyBelong.fund += this.earnRate;
    }
    void Start() {
        price = 50f;
        earnRate = 0.01f;

        workingTalent = null;
        companyBelong = null;
        Debug.Log("Building Start done");
    }
    void Update(){
        makeMoney();
    }
}
