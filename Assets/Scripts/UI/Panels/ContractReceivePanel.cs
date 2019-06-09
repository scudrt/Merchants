using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractReceivePanel : MonoBehaviour
{
    public Contract contract { get; set; }//contract received

    public GameObject contractBuildingInfo;
    public GameObject contractTalentInfo;

    private RectTransform receive;
    private RectTransform offer;
    private Text fund;

    // Start is called before the first frame update
    void Start()
    {
        contractBuildingInfo = (GameObject)Resources.Load("Prefabs/ContractBuildingInfo");
        contractTalentInfo = (GameObject)Resources.Load("Prefabs/ContractTalentInfo");

        receive = transform.Find("Receive").Find("Content").GetComponent<RectTransform>();
        offer = transform.Find("Offer").Find("Content").GetComponent<RectTransform>();
        fund = transform.Find("Fund").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnOpen()
    {
        //clear the previous content
        GameObject child;
        for (int j = 0; j < receive.transform.childCount; j++)
        {
            child = receive.transform.GetChild(j).gameObject;
            Destroy(child);
        }

        for (int j = 0; j < offer.transform.childCount; j++)
        {
            child = offer.transform.GetChild(j).gameObject;
            Destroy(child);
        }

        //add item in receive and offer
        int receiveCount = 0;
        int offerCount = 0;

        GameObject blockInfo;
        RectTransform rectTransform;
        GameObject talentInfo;

        /******set receive's item******/
        foreach (Block block in contract.offeredBlocks)
        {
            blockInfo = GameObject.Instantiate(contractBuildingInfo, receive.transform);
            rectTransform = blockInfo.GetComponent<RectTransform>();

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, receiveCount * 50, 50);

            //set text
            if (!block.isEmpty)
            {
                blockInfo.transform.Find("Type").GetComponent<Text>().text = block.building.buildingType;
                blockInfo.transform.Find("Profit").GetComponent<Text>().text = block.building.totalProfit.ToString();
            }
            else
            {
                blockInfo.transform.Find("Type").GetComponent<Text>().text = "暂无建筑";
                blockInfo.transform.Find("Profit").GetComponent<Text>().text = "";
            }

            receiveCount++;
        }

        foreach(Talent talent in contract.offeredTalents)
        {
            talentInfo = GameObject.Instantiate(contractTalentInfo, receive.transform);
            rectTransform = talentInfo.GetComponent<RectTransform>();

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, receiveCount * 50, 50);

            //set text
            talentInfo.transform.Find("Name").GetComponent<Text>().text = talent.name;
            talentInfo.transform.Find("Capacity").GetComponent<Text>().text = talent.capacity.ToString();
            talentInfo.transform.Find("Charm").GetComponent<Text>().text = talent.charm.ToString();
            talentInfo.transform.Find("Salary").GetComponent<Text>().text = talent.salary.ToString();

            receiveCount++;
        }

        //change content rect's height
        receive.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, receiveCount * 50);
        /******set receive's item******/

        /******set offer's item******/
        foreach (Block block in contract.requiredBlocks)
        {
            blockInfo = GameObject.Instantiate(contractBuildingInfo, offer.transform);
            rectTransform = blockInfo.GetComponent<RectTransform>();

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, offerCount * 50, 50);

            //set text
            if (!block.isEmpty)
            {
                blockInfo.transform.Find("Type").GetComponent<Text>().text = block.building.buildingType;
                blockInfo.transform.Find("Profit").GetComponent<Text>().text = block.building.totalProfit.ToString();
            }
            else
            {
                blockInfo.transform.Find("Type").GetComponent<Text>().text = "暂无建筑";
                blockInfo.transform.Find("Profit").GetComponent<Text>().text = "";
            }

            offerCount++;
        }

        foreach (Talent talent in contract.requiredTalents)
        {
            talentInfo = GameObject.Instantiate(contractTalentInfo, offer.transform);
            rectTransform = talentInfo.GetComponent<RectTransform>();

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, receiveCount * 50, 50);

            //set text
            talentInfo.transform.Find("Name").GetComponent<Text>().text = talent.name;
            talentInfo.transform.Find("Capacity").GetComponent<Text>().text = talent.capacity.ToString();
            talentInfo.transform.Find("Charm").GetComponent<Text>().text = talent.charm.ToString();
            talentInfo.transform.Find("Salary").GetComponent<Text>().text = talent.salary.ToString();

            offerCount++;
        }

        //change content rect's height
        offer.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, receiveCount * 50);
        /******set offer's item******/

        //set fund
        fund.text = contract.offeredFund.ToString();
    }

    public void OnAcceptButtonClicked()
    {
        contract.agree();
    }

    public void OnRefuseButtonClicked()
    {
        contract.refuse();
    }
}
