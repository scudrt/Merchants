using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ContractSendPanel : MonoBehaviour
{
    public GameObject contractTalentInfo;
    public GameObject contractBuildingInfo;

    //conpanies involved;
    public Company self;
    public Company other;

    private Contract contract; //contract edited

    private List<Talent> requireTalentsList;
    private List<Block> requireBlocksList;
    private List<Talent> offerTalentsList;
    private List<Block> offerBlocksList;
    private List<Talent> ownTalentsList;
    private List<Block> ownBlocksList;
    private List<Talent> otherTalentsList;
    private List<Block> otherBlocksList;

    private RectTransform requireTalents;
    private RectTransform requireBuildings;
    private RectTransform offerTalents;
    private RectTransform offerBuildings;
    private RectTransform ownTalents;
    private RectTransform ownBuildings;
    private RectTransform otherTalents;
    private RectTransform otherBuildings;

    private InputField fund;
    // Start is called before the first frame update
    void Start()
    {
        contractTalentInfo = (GameObject)Resources.Load("Prefabs/ContractTalentInfo");
        contractBuildingInfo = (GameObject)Resources.Load("Prefabs/ContractBuildingInfo");

        requireTalents = transform.Find("Require").Find("RequireTalents").Find("Content").GetComponent<RectTransform>();
        requireBuildings = transform.Find("Require").Find("RequireBuildings").Find("Content").GetComponent<RectTransform>();
        offerTalents = transform.Find("Offer").Find("OfferTalents").Find("Content").GetComponent<RectTransform>();
        offerBuildings = transform.Find("Offer").Find("OfferBuildings").Find("Content").GetComponent<RectTransform>();
        ownTalents = transform.Find("Offer").Find("OwnTalents").Find("Content").GetComponent<RectTransform>();
        ownBuildings = transform.Find("Offer").Find("OwnBuildings").Find("Content").GetComponent<RectTransform>();
        otherTalents = transform.Find("Require").Find("OtherTalents").Find("Content").GetComponent<RectTransform>();
        otherBuildings = transform.Find("Require").Find("OtherBuildings").Find("Content").GetComponent<RectTransform>();

        fund = transform.Find("Fund").Find("Fund").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOpen(Company self, Company other)
    {
        //init valuable
        this.self = self;
        this.other = other;
        contract = new Contract(self, other);

        ownTalentsList = new List<Talent>(self.talentList);
        ownBlocksList = new List<Block>(self.blockList);
        otherTalentsList = new List<Talent>(other.talentList);
        otherBlocksList = new List<Block>(other.blockList);
        requireTalentsList = new List<Talent>();
        requireBlocksList = new List<Block>();
        offerTalentsList = new List<Talent>();
        offerBlocksList = new List<Block>();

        DisplayRequireTalents();
        DisplayRequireBuildings();
        DisplayOwnTalents();
        DisplayOwnBuildings();
        DisplayOtherTalents();
        DisplayOtherBuildings();
        DisplayOfferTalents();
        DisplayOfferBuildings();
     }

    public void OnClose()
    {
        SendMessage("UIExit");
    }

    public void DisplayRequireTalents()
    {
        //destory previous objects
        for(int j = 0; j < requireTalents.childCount; j++)
        {
            Destroy(requireTalents.GetChild(j).gameObject);
        }

        GameObject talentInfo;
        RectTransform rectTransform;
        Button button;
        int i = 0;//i is the number of column

        foreach (Talent talent in requireTalentsList)
        {
            talentInfo = GameObject.Instantiate(contractTalentInfo, requireTalents.transform);
            rectTransform = talentInfo.GetComponent<RectTransform>();
            button = talentInfo.GetComponent<Button>();

            //set onClick listener
            button.onClick.AddListener(delegate ()
            {
                requireTalentsList.Remove(talent);
                otherTalentsList.Add(talent);
                DisplayRequireTalents();
                DisplayOtherTalents();
            });

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 50, 50);

            //set text
            talentInfo.transform.Find("Name").GetComponent<Text>().text = talent.name;
            talentInfo.transform.Find("Capacity").GetComponent<Text>().text = talent.capacity.ToString();
            talentInfo.transform.Find("Charm").GetComponent<Text>().text = talent.charm.ToString();
            talentInfo.transform.Find("Salary").GetComponent<Text>().text = talent.salary.ToString();

            i++;
        }

        //change content rect's height
        requireTalents.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);
    }

    public void DisplayRequireBuildings()
    {
        //destory previous objects
        for (int j = 0; j < requireBuildings.childCount; j++)
        {
            Destroy(requireBuildings.GetChild(j).gameObject);
        }

        GameObject blockInfo;
        RectTransform rectTransform;
        Button button;
        int i = 0;//i is the number of column

        foreach (Block block in requireBlocksList)
        {
            blockInfo = GameObject.Instantiate(contractBuildingInfo, requireBuildings.transform);
            rectTransform = blockInfo.GetComponent<RectTransform>();
            button = blockInfo.GetComponent<Button>();

            //set onClick listener
            button.onClick.AddListener(delegate ()
            {
                requireBlocksList.Remove(block);
                otherBlocksList.Add(block);
                DisplayRequireBuildings();
                DisplayOtherBuildings();
            });

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 50, 50);

            //set text
            if (!block.isEmpty)
            {
                blockInfo.transform.Find("Type").GetComponent<Text>().text = block.building.buildingType;
                blockInfo.transform.Find("Profit").GetComponent<Text>().text = block.building.totalProfit.ToString();
                blockInfo.transform.Find("Name").GetComponent<Text>().text = block.building.nickName;
            }
            else
            {
                blockInfo.transform.Find("Type").GetComponent<Text>().text = "暂无建筑";
                blockInfo.transform.Find("Profit").GetComponent<Text>().text = "";
                blockInfo.transform.Find("Name").GetComponent<Text>().text = "暂无建筑";
            }

            i++;
        }

        //change content rect's height
        requireBuildings.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);
    }

    public void DisplayOfferTalents()
    {
        //destory previous objects
        for (int j = 0; j < offerTalents.childCount; j++)
        {
            Destroy(offerTalents.GetChild(j).gameObject);
        }

        GameObject talentInfo;
        RectTransform rectTransform;
        Button button;
        int i = 0;//i is the number of column

        foreach (Talent talent in offerTalentsList)
        {
            talentInfo = GameObject.Instantiate(contractTalentInfo, offerTalents.transform);
            rectTransform = talentInfo.GetComponent<RectTransform>();
            button = talentInfo.GetComponent<Button>();

            //set onClick listener
            button.onClick.AddListener(delegate ()
            {
                offerTalentsList.Remove(talent);
                ownTalentsList.Add(talent);
                DisplayOfferTalents();
                DisplayOwnTalents();
            });

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 50, 50);

            //set text
            talentInfo.transform.Find("Name").GetComponent<Text>().text = talent.name;
            talentInfo.transform.Find("Capacity").GetComponent<Text>().text = talent.capacity.ToString();
            talentInfo.transform.Find("Charm").GetComponent<Text>().text = talent.charm.ToString();
            talentInfo.transform.Find("Salary").GetComponent<Text>().text = talent.salary.ToString();

            i++;
        }

        //change content rect's height
        offerTalents.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);
    }

    public void DisplayOfferBuildings()
    {
        //destory previous objects
        for (int j = 0; j < offerBuildings.childCount; j++)
        {
            Destroy(offerBuildings.GetChild(j).gameObject);
        }

        GameObject blockInfo;
        RectTransform rectTransform;
        Button button;
        int i = 0;//i is the number of column

        foreach (Block block in offerBlocksList)
        {
            blockInfo = GameObject.Instantiate(contractBuildingInfo, offerBuildings.transform);
            rectTransform = blockInfo.GetComponent<RectTransform>();
            button = blockInfo.GetComponent<Button>();

            //set onClick listener
            button.onClick.AddListener(delegate ()
            {
                offerBlocksList.Remove(block);
                ownBlocksList.Add(block);
                DisplayOfferBuildings();
                DisplayOwnBuildings();
            });

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 50, 50);

            //set text
            if (!block.isEmpty)
            {
                blockInfo.transform.Find("Type").GetComponent<Text>().text = block.building.buildingType;
                blockInfo.transform.Find("Profit").GetComponent<Text>().text = block.building.totalProfit.ToString();
                blockInfo.transform.Find("Name").GetComponent<Text>().text = block.building.nickName;
            }
            else
            {
                blockInfo.transform.Find("Type").GetComponent<Text>().text = "暂无建筑";
                blockInfo.transform.Find("Profit").GetComponent<Text>().text = "";
                blockInfo.transform.Find("Name").GetComponent<Text>().text = "暂无建筑";
            }

            i++;
        }

        //change content rect's height
        offerBuildings.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);
    }

    public void DisplayOwnTalents()
    {
        //destory previous objects
        for (int j = 0; j < ownTalents.childCount; j++)
        {
            Destroy(ownTalents.GetChild(j).gameObject);
        }

        GameObject talentInfo;
        RectTransform rectTransform;
        Button button;
        int i = 0;//i is the number of column

        foreach (Talent talent in ownTalentsList)
        {
            talentInfo = GameObject.Instantiate(contractTalentInfo, ownTalents.transform);
            rectTransform = talentInfo.GetComponent<RectTransform>();
            button = talentInfo.GetComponent<Button>();

            //set onClick listener
            button.onClick.AddListener(delegate ()
            {
                ownTalentsList.Remove(talent);
                offerTalentsList.Add(talent);
                DisplayOwnTalents();
                DisplayOfferTalents();
            });

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 50, 50);

            //set text
            talentInfo.transform.Find("Name").GetComponent<Text>().text = talent.name;
            talentInfo.transform.Find("Capacity").GetComponent<Text>().text = talent.capacity.ToString();
            talentInfo.transform.Find("Charm").GetComponent<Text>().text = talent.charm.ToString();
            talentInfo.transform.Find("Salary").GetComponent<Text>().text = talent.salary.ToString();

            i++;
        }

        //change content rect's height
        ownTalents.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);
    }

    public void DisplayOwnBuildings()
    {
        //destory previous objects
        for (int j = 0; j < ownBuildings.childCount; j++)
        {
            Destroy(ownBuildings.GetChild(j).gameObject);
        }

        GameObject blockInfo;
        RectTransform rectTransform;
        Button button;
        int i = 0;//i is the number of column

        foreach (Block block in ownBlocksList)
        {
            blockInfo = GameObject.Instantiate(contractBuildingInfo, ownBuildings.transform);
            rectTransform = blockInfo.GetComponent<RectTransform>();
            button = blockInfo.GetComponent<Button>();

            //set onClick listener
            button.onClick.AddListener(delegate ()
            {
                ownBlocksList.Remove(block);
                offerBlocksList.Add(block);
                DisplayOwnBuildings();
                DisplayOfferBuildings();
            });

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 50, 50);

            //set text
            if (!block.isEmpty)
            {
                blockInfo.transform.Find("Type").GetComponent<Text>().text = block.building.buildingType;
                blockInfo.transform.Find("Profit").GetComponent<Text>().text = block.building.totalProfit.ToString();
                blockInfo.transform.Find("Name").GetComponent<Text>().text = block.building.nickName;
            }
            else
            {
                blockInfo.transform.Find("Type").GetComponent<Text>().text = "暂无建筑";
                blockInfo.transform.Find("Profit").GetComponent<Text>().text = "";
                blockInfo.transform.Find("Name").GetComponent<Text>().text = "暂无建筑";
            }

            i++;
        }

        //change content rect's height
        ownBuildings.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);
    }

    public void DisplayOtherTalents()
    {
        //destory previous objects
        for (int j = 0; j < otherTalents.childCount; j++)
        {
            Destroy(otherTalents.GetChild(j).gameObject);
        }

        GameObject talentInfo;
        RectTransform rectTransform;
        Button button;
        int i = 0;//i is the number of column

        foreach (Talent talent in otherTalentsList)
        {
            talentInfo = GameObject.Instantiate(contractTalentInfo, otherTalents.transform);
            rectTransform = talentInfo.GetComponent<RectTransform>();
            button = talentInfo.GetComponent<Button>();

            //set onClick listener
            button.onClick.AddListener(delegate ()
            {
                otherTalentsList.Remove(talent);
                requireTalentsList.Add(talent);
                DisplayRequireTalents();
                DisplayOtherTalents();
            });

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 50, 50);

            //set text
            talentInfo.transform.Find("Name").GetComponent<Text>().text = talent.name;
            talentInfo.transform.Find("Capacity").GetComponent<Text>().text = talent.capacity.ToString();
            talentInfo.transform.Find("Charm").GetComponent<Text>().text = talent.charm.ToString();
            talentInfo.transform.Find("Salary").GetComponent<Text>().text = talent.salary.ToString();

            i++;
        }

        //change content rect's height
        otherTalents.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);
    }

    public void DisplayOtherBuildings()
    {
        //destory previous objects
        for (int j = 0; j < otherBuildings.childCount; j++)
        {
            Destroy(otherBuildings.GetChild(j).gameObject);
        }

        GameObject blockInfo;
        RectTransform rectTransform;
        Button button;
        int i = 0;//i is the number of column

        foreach (Block block in otherBlocksList)
        {
            blockInfo = GameObject.Instantiate(contractBuildingInfo, otherBuildings.transform);
            rectTransform = blockInfo.GetComponent<RectTransform>();
            button = blockInfo.GetComponent<Button>();

            //set onClick listener
            button.onClick.AddListener(delegate ()
            {
                otherBlocksList.Remove(block);
                requireBlocksList.Add(block);
                DisplayRequireBuildings();
                DisplayOtherBuildings();
            });

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 50, 50);

            //set text
            if (!block.isEmpty)
            {
                blockInfo.transform.Find("Type").GetComponent<Text>().text = block.building.buildingType;
                blockInfo.transform.Find("Profit").GetComponent<Text>().text = block.building.totalProfit.ToString();
                blockInfo.transform.Find("Name").GetComponent<Text>().text = block.building.nickName;
            }
            else
            {
                blockInfo.transform.Find("Type").GetComponent<Text>().text = "暂无建筑";
                blockInfo.transform.Find("Profit").GetComponent<Text>().text = "";
                blockInfo.transform.Find("Name").GetComponent<Text>().text = "暂无建筑";
            }

            i++;
        }

        //change content rect's height
        otherBuildings.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);
    }

    public void OnSendButtonClicked()
    {
        foreach(Talent talent in requireTalentsList)
        {
            contract.addTalent(talent);
        }

        foreach(Talent talent in offerTalentsList)
        {
            contract.addTalent(talent);
        }

        foreach(Block block in requireBlocksList)
        {
            contract.addBlock(block);
        }

        foreach(Block block in offerBlocksList)
        {
            contract.addBlock(block);
        }

        if (this.fund.text == "")
            this.fund.text = "0";
        float fund = (float)System.Convert.ToDouble(this.fund.text);

        contract.setOfferedFund(fund);

        contract.confirm();
        this.OnClose();
    }
}
