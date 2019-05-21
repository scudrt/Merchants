﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BlockUI : MonoBehaviour
{
    public Block targetBlock;//which Block's information to display

    private GameObject emptyPanel;
    private GameObject buildingPanel;
    private GameObject boughtPanel; //UI when the Block is bought but not constructed any Block

    // Start is called before the first frame update
    void Start()
    {
        targetBlock = null;
        emptyPanel = transform.Find("EmptyPanel").gameObject;
        buildingPanel = transform.Find("BuildingPanel").gameObject;
        boughtPanel = transform.Find("BoughtPanel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EmptyPanelEntry()
    {
        //display UI
        emptyPanel.GetComponent<UIPanel>().UIEntry();

        //set the information
        emptyPanel.transform.Find("BlockPrice").GetComponent<Text>().text
            = targetBlock.price.ToString();
    }

    public void BuildingPanelEntry()
    {
        //display UI
        buildingPanel.GetComponent<UIPanel>().UIEntry();

        //set the information
    }

    public void BoughtPanelEntry()
    {
        //display UI
        boughtPanel.GetComponent<UIPanel>().UIEntry();

        //set the information
    }
}