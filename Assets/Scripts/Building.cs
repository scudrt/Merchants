using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour{
    //Data Area
    public string nickName = "BuildingNickName"; //building's name made by player
    public string buildingType = "Building";
    public Block blockBelong = null;
    public int level = 1;
    public float price = 1000f, upgradePrice = 2000f;
    public float budget = 0f;
    public float ADBudgetProportion = 0.5f;
    //statistics data
    public float profitAmount = 0f;
    public int customerCount = 0;
    //private data
    private int currentHour = Timer.hour;
    private float attrackRate = 0.01f;
    private float profitEach = 3f;
    private int talentCountLimit = 1;
    private List<Talent> talentList; //index 0 is the building's manager
    /************common functions************/
    public float getADBudget() {
        return budget * ADBudgetProportion;
    }
    public float getOPBudget() {
        return budget * (1 - ADBudgetProportion);
    }
    public int getTalentCount() {
        return talentList.Count;
    }
    /************virtual functions************/
    public virtual void Update() {
        if (currentHour != Timer.hour) {
            currentHour = Timer.hour;
            makeMoney();
        }
    }
    public virtual bool upgrade() { //level up
        if (blockBelong.companyBelong.costMoney(upgradePrice)) {
            upgradePrice *= 2f;

            ++level;
            attrackRate = Mathf.Min(1.0f, attrackRate + 0.02f);
            talentCountLimit += 2;
            profitEach += 2;

            return true;
        } else { //no enough money
            return false;
        }
    }
    public virtual bool addTalent(Talent talent) {
        if (talent == null) {
            return false;
        }
        if (talentList.Count == talentCountLimit) {
            return false;
        }
        if (talentList.Contains(talent)) {
            return false;
        }
        talentList.Add(talent);
        return true;
    }
    public virtual bool removeTalent(Talent talent) {
        if (talentList.Contains(talent)) {
            talentList.Remove(talent);
            return true;
        } else {
            return false;
        }
    }
    public virtual void onGenerate() {
        //special buildings should override this function
    }
    public virtual void makeMoney() {
        float newProfit;
        int newCustomer;

        newCustomer = (int)(City.generateNormalDistribution(attrackRate, 0.005f) * Population.amount);
        customerCount += newCustomer;

        newProfit = profitEach * newCustomer;
        profitAmount += newProfit;

        if (blockBelong.companyBelong != null) {
            if (blockBelong.companyBelong == City.currentCompany) { //TO BE DONE
                //show money flowing effect, empty for now
            }
            blockBelong.companyBelong.earnMoney(newProfit);
        }
    }

    public virtual bool addBudget(float delta) {
        if (blockBelong == null || blockBelong.companyBelong == null) {
            return false;
        }
        if (blockBelong.companyBelong.costMoney(delta)) {
            budget += delta;
            return true;
        } else {
            return false;
        }
    }
}
