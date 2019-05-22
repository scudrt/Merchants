using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Company companyBelong;
    public float price;
    public float earnRate;
    public Talent workingTalent;

    public string buildingType;
    public void makeMoney() {
        if (companyBelong == null) {
            return;
        }
        companyBelong.fund += this.earnRate;
    }
    public void Awake() {
        price = 50f;
        earnRate = 0.01f;

        workingTalent = null;
        companyBelong = null;
        Debug.Log("Building Awake done");
    }
    void Start(){
    }
    void Update(){
        makeMoney();
    }
}
