using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManagement : MonoBehaviour
{
    public GameObject blockInfoPrefab;

    public Block targetBlock;

    private ScrollRect scrollrect;//blocks' scroll view
    private GameObject content;//content contains talent informations
    private RectTransform contentTR;
    private Transform details;

    private Color defaultColor;

    //objects in blockInfo
    private int serial;//displayed talent's serial number in blocks list
    private Text buildingName;
    private Text industry;
    private Text customer;
    private InputField budget;
    private Text profit;
    private Text advertising; // cost on advertising
    private Text operating; //cost of running the building
    private Slider budgetUse;

    // Start is called before the first frame update
    void Start()
    {
        blockInfoPrefab = (GameObject)Resources.Load("Prefabs/BlockInfo");
        defaultColor = blockInfoPrefab.GetComponent<Image>().color;

        scrollrect = transform.Find("Blocks").GetComponent<ScrollRect>();
        content = transform.Find("Blocks").transform.Find("Content").gameObject;
        contentTR = content.GetComponent<RectTransform>();
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

        details = transform.Find("Details");
        buildingName = details.Find("Name").GetComponent<Text>();
        industry = details.Find("Industry").GetComponent<Text>();
        budget = details.Find("BudgetInput").GetComponent<InputField>();
        profit = details.Find("Profit").GetComponent<Text>();
        advertising = details.Find("Advertising").GetComponent<Text>();
        operating = details.Find("Operating").GetComponent<Text>();
        budgetUse = details.Find("BudgetUse").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroyItemInfo()
    {

    }

    public void OnOpen(Block targetBlock = null)
    {
        details.gameObject.SetActive(false);

        //clear the previous content
        BroadcastMessage("DestroyItemInfo");

        List<Block> blocks = City.currentCompany.blockList;
        GameObject blockInfo;
        RectTransform rectTransform;
        ItemInfo script;
        int i = 0;//i is the number of column

        foreach (Block block in blocks)
        {
            //add talent's information
            blockInfo = GameObject.Instantiate(blockInfoPrefab, content.transform);
            rectTransform = blockInfo.GetComponent<RectTransform>();
            script = blockInfo.GetComponent<ItemInfo>();

            script.serial = i;

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 50, 50);

            //set text
            string nameText;
            string profitText;
            if (block.isEmpty)
            {
                nameText = "暂无建筑";
                profitText = "0";
            }
            else
            {
                nameText = block.building.nickName;
                profitText = block.building.monthlyProfit.ToString();
            }
            blockInfo.transform.Find("Name").GetComponent<Text>().text = nameText;
            blockInfo.transform.Find("Profit").GetComponent<Text>().text = profitText;

            i++;
        }

        //change content rect's height
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);

        //if exist target block, display details
        if (targetBlock != null)
        {
            serial = City.currentCompany.blockList.IndexOf(targetBlock);
            DisplayItemInfo(serial);
        }
    }

    public void DisplayItemInfo(int _serial)
    {
        this.serial = _serial;

        //change color of the item selected 
        for (int i = 0; i < contentTR.childCount; i++)
        {
            if (i != serial)
            {
                contentTR.GetChild(i).GetComponent<Image>().color = defaultColor;
            }
            else
            {
                contentTR.GetChild(i).GetComponent<Image>().color = Color.red;
            }
        }

        Block block = City.currentCompany.blockList[serial];
        if (block.isEmpty)
        {
            Debug.Log("Is Empty");
            return;
        }

        details.gameObject.SetActive(true);

        buildingName.text = block.building.nickName;
        industry.text = block.building.buildingType;
        budget.text = block.building.budget.ToString();
        profit.text = block.building.annualProfit.ToString();
        advertising.text = (budgetUse.value).ToString() + "%";
        operating.text = (100 - budgetUse.value).ToString() + "%";
    }

    public void OnBudgetUseValueChanged()
    {
        Block block = City.currentCompany.blockList[serial];
        int value = (int)budgetUse.value;

        block.building.ADBudgetProportion = (float)value / 100;
        advertising.text = (budgetUse.value).ToString() + "%";
        operating.text = (100 - budgetUse.value).ToString() + "%";
    }

    public void OnChangeBudgetButtonClicked()
    {
        Block block = City.currentCompany.blockList[serial];
        float value = (float)System.Convert.ToDouble(budget.text);
        block.building.addBudget(value - block.building.budget);
    }
}
