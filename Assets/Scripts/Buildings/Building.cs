using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour {
    //Data Area
    public static float PRICE = 0f;
    public string nickName = "看不见的店铺"; //building's name made by player
    public string buildingType = "Building";
    public Block blockBelong = null;
    public int level = 1;
    public float price { get {
            return basicPrice + budget;
        } private set { } }
    public float basicPrice, upgradePrice = 150000f;
    public float budget = 0f;
    public float ADBudgetProportion = 0.5f;
    //statistics data
    public float monthlyProfit = 0f, annualProfit = 0f;
    public float totalProfit = 0f;
    public int customerCount = 0;

    //private data
    private int currentHour = Timer.hour, currentMonth = Timer.month, currentYear = Timer.year;

    /*****test*****/
    private float _attrackRate = 0.01f;
    public float attrackRate { get
        {
            return _attrackRate;
        }
        set
        {
            if (value < 0 || value > 1)
                return;
            _attrackRate = value;
        }
    }


    private float profitEach = 3f;
    private int talentCountLimit = 1;
    private List<Talent> talentList = new List<Talent>(); //index 0 is the building's manager

    /************common functions************/
    public void clearRecord() {
        //clear statistics for new owner
        monthlyProfit = 0f;
        annualProfit = 0f;
        totalProfit = 0f;
        customerCount = 0;
    }
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
        talent.workPlace = this;
        return true;
    }
    public virtual bool removeTalent(Talent talent) {
        if (talent == null) {
            return false;
        }
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
        if (currentMonth != Timer.month) {
            currentMonth = Timer.month;
            monthlyProfit = 0f;
        }
        if (currentYear != Timer.year) {
            currentYear = Timer.year;
            annualProfit = 0f;
        }
        totalProfit += newProfit;
        monthlyProfit += newProfit;
        annualProfit += newProfit;

        if (blockBelong.isOwned) {
            if (blockBelong.companyBelong == City.currentCompany) { //TO BE DONE
                //show money flowing effect, empty for now
            }
            blockBelong.companyBelong.earnMoney(newProfit);
        }
    }

    public virtual bool addBudget(float delta) {
        if (blockBelong == null || blockBelong.isOwned == false) {
            return false;
        }
        if (blockBelong.companyBelong.costMoney(delta)) {
            budget += delta;
            return true;
        } else {
            return false;
        }
    }

    public void onDestory() {
        //return money
        blockBelong.companyBelong.earnMoney(this.budget + this.basicPrice / 2f);
        //send talents to company
        foreach(Talent talent in talentList) {
            talent.workPlace = null;
        }
        //destory its parents
        GameObject.DestroyImmediate(this.gameObject);
    }
}
