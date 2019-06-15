using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    private bool isSendContract = false;

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

        //send contract to player
        if (company.blockList.Count > 1 && !isSendContract)
        {
            Debug.Log("Contract sent by ai");
            Contract contract = new Contract(company, City.currentCompany);
            contract.addBlock(company.blockList[0]);
            contract.offerFundBy(-10000);
            contract.confirm();
            isSendContract = true;
        }

        //deal with contract
        if (company.contract != null) {
            if (company.contract._offererId == company.id) {
                //it is the offerer, do nothing
            } else {
                //it is the target of contract
                float profit = company.contract.offeredFund;

                //analyze whether it can get profit
                foreach (int i in company.contract.offeredBlocks) {
                    profit += City.blockList[i].price;
                }
                foreach (int i in company.contract.requiredBlocks) {
                    profit -= City.blockList[i].price;
                }

                if (profit > 0) { //could obtain profit, then agree
                    company.contract.agree();
                } else {
                    company.contract.refuse();
                }
            }
        }

        //build something randomly
       int index = Random.Range(0, company.blockList.Count - 1);
       for (int i = index; i < company.blockList.Count; ++i) {
            Block block = company.blockList[i];

            //try four times
            for (int j = 0; j < 4; ++j) {
                int buildIndex = Random.Range(0, 8);
                if (block.isEmpty && company.buildOnBlock(block, City.buildingTypes[buildIndex])) {
                    //make decision and return
                    return;
                }
            }
        }

        //if couldn't build, then try buy a block
        //randomly
        index = Random.Range(0, City.blockList.Count - 1);
        for (int i = index; i < City.blockList.Count; ++i) {
            Block block = City.blockList[i];

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


        //
        
    }

    void Start(){
        companyIdList = new List<int>();
        currentHour = Timer.hour;
    }

    // Update is called once per frame
    void Update(){
        //make decision every two hours
        if (currentHour == Timer.hour || ((Timer.hour&1) == 0)) {
            return;
        }
        currentHour = Timer.hour;

        foreach (int index in companyIdList) {
            makeDecisionFor(City.companyList[index]);
        }
    }
}
