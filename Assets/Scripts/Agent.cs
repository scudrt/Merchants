using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {
    public bool initDone = false;
    private int numOfPlayer;
    private AIController AIPlayer;
    public Company distributePlayer() {
        ++numOfPlayer;

        Company newPlayer;
        if (numOfPlayer <= City.numOfCompany) {
            newPlayer = City.companyList[numOfPlayer - 1];
            AIPlayer.removeCompany(newPlayer);
        } else {
            newPlayer = City.generateNewCompany();
        }

        newPlayer.isHuman = true;
        return newPlayer;
    }

    private IEnumerator onGenerate() {

        numOfPlayer = 0;
        AIPlayer = GameObject.FindObjectOfType<AIController>();
        
        //wait for company init done
        yield return new WaitUntil(()=> { return City.companyList != null &&
            City.companyList.Count == City.numOfCompany; });
        int tick = 0;
        foreach (Company company in City.companyList) {
            ++tick;
            if (company.isHuman == false) {
                AIPlayer.addCompany(company);
            }
        }

        yield return new WaitUntil(() => { return tick == City.companyList.Count; });
        initDone = true;
    }
    void Start() {
        StartCoroutine(onGenerate());
    }
    void Update() {

    }
}
