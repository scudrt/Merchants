using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    private int currentHour;
    private List<int> companyIdList;
    public void addCompany(int id) {
        if (companyIdList.Contains(id)) {
            return;
        }
        companyIdList.Add(id);
    }
    public void addCompany(Company company) {
        addCompany(company.id);
    }
    public void removeCompany(int id) {
        if (companyIdList.Contains(id)) {
            companyIdList.Remove(id);
        }
    }

    public void removeCompany(Company company) {
        removeCompany(company.id);
    }

    private void makeDecisionFor(Company company) {
        if (company.isAlive == false) {
            return;
        }

        //build restaurant first
        foreach (Block block in company.blockList) {
            if (block.isEmpty && company.buildOnBlock(block, "Restaurant")) {
                //make decision and return
                return;
            }
        }
        Debug.Log(company.id + " no building");
        //if couldn't build, then try buy a block
        foreach (Block block in City.blockList) {
            if (block.isOwned) {
                continue;
            }
            if (company.buyBlock(block)) { //desicion made, return
                return;
            } else { //buying failed
                if (block.isEmpty) { //couldn't afford the cheapest block
                    return;
                }
            }
        }
        Debug.Log(company.id + " no buying");
    }

    void Start(){
        companyIdList = new List<int>();
        currentHour = Timer.hour;
    }

    // Update is called once per frame
    void Update(){
        //make decision every hour
        if (currentHour == Timer.hour) {
            return;
        }
        currentHour = Timer.hour;

        foreach (int index in companyIdList) {
            Debug.Log("AI:index=" + index);
            makeDecisionFor(City.companyList[index]);
        }
    }
}
