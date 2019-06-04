using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour{
    //Data Area
    public string nickName = "BuildingNickName"; //building's name made by player
    public string buildingType = "Building";
    public Block blockBelong = null;
    public int level = 1;
    public float price = 1000f;
    public float budget = 0f;
    public float ADBudgetProportion = 0.5f;
    //statistics data
    public float profitAmount = 0f;
    public int customerCount = 0;
    //private data
    private int currentHour = Timer.hour;
    private float attrackRate = 0.01f;
    private float profitEach = 3f;
    private int talentCountLimit;
    private List<Talent> talentList; //index 0 is the building's manager
    //common functions
    public float getADBudget() {
        return budget * ADBudgetProportion;
    }
    public float getOPBudget() {
        return budget * (1 - ADBudgetProportion);
    }
    public int getTalentCount() {
        return talentList.Count;
    }
    //virtual functions
    public virtual void Update() {
        if (currentHour != Timer.hour) {
            currentHour = Timer.hour;
            makeMoney();
        }
    }
    public virtual void upgrade() {
        ++level;
        attrackRate = Mathf.Min(1.0f, attrackRate + 0.02f);
        profitEach += 2;
    }
    public virtual bool addTalent(Talent talent) {
        if (talent == null) {
            return false;
        }
        if (talentList.Count == talentCountLimit) {
            return false;
        }
        talentList.Add(talent);
        return true;
    }
    public virtual bool removeTalent(int talentId) {
        for (int i = 0; i < talentList.Count; ++i) {
            if (talentList[i].id == talentId) {
                //remove the talent from this building
                talentList.RemoveAt(i);
                return true;
            }
        }
        return false;
    }
    public virtual void onGenerate() {
        //special buildings should override this function
    }
    public virtual void makeMoney() {
        float newProfit;
        int newCustomer;

        newCustomer = (int)(attrackRate * Population.amount);
        customerCount += newCustomer;

        newProfit = profitEach * newCustomer;
        profitAmount += newProfit;

        if (blockBelong.companyBelong != null) {
            if (blockBelong.companyBelong == City.currentCompany) {
                //show money flowing effect, empty for now
            }
            blockBelong.companyBelong.earnMoney(newProfit);
        }
    }
}
