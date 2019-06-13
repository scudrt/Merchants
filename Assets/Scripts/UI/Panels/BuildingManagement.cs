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
    private ButtonGroup buttonGroup;

    private bool isDisplayed;
    private List<Text> profits;

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
        buttonGroup = contentTR.GetComponent<ButtonGroup>();

        isDisplayed = false;
        profits = new List<Text>();

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
        if (isDisplayed)
        {
            int i = 0;
            foreach(Text text in profits)
            {
                if (!City.currentCompany.blockList[i].isEmpty)
                    text.text = City.currentCompany.blockList[i].building.monthlyProfit.ToString();
                else
                    text.text = "0";
                i++;
            }
            if (details.gameObject.activeSelf)
            {
                if (!City.currentCompany.blockList[serial].isEmpty)
                    profit.text = City.currentCompany.blockList[serial].building.monthlyProfit.ToString();
                else
                    profit.text = "0";
            }
        }
        
    }

    private void DestroyItemInfo()
    {

    }

    public void OnOpen(Block targetBlock = null)
    {
        isDisplayed = true;
        profits.Clear();
        details.gameObject.SetActive(false);

        //clear the previous content
        BroadcastMessage("DestroyItemInfo");
        buttonGroup.buttons.Clear();

        List<Block> blocks = City.currentCompany.blockList;
        GameObject blockInfo;
        RectTransform rectTransform;
        ItemInfo script;
        Button button;
        int i = 0;//i is the number of column

        foreach (Block block in blocks)
        {
            //add talent's information
            blockInfo = GameObject.Instantiate(blockInfoPrefab, content.transform);
            rectTransform = blockInfo.GetComponent<RectTransform>();
            script = blockInfo.GetComponent<ItemInfo>();
            button = blockInfo.GetComponent<Button>();

            buttonGroup.buttons.Add(button);

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
            Text temp = blockInfo.transform.Find("Profit").GetComponent<Text>();
            temp.text = profitText;
            profits.Add(temp);

            i++;
        }

        //change content rect's height
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);

        //if target block exist, display details
        if (targetBlock != null)
        {
            serial = City.currentCompany.blockList.IndexOf(targetBlock);
            buttonGroup.setSelect(serial);
            DisplayItemInfo(serial);
        }
        Debug.Log("Open end");
    }

    public void DisplayItemInfo(int _serial)
    {
        this.serial = _serial;

        //set color
        buttonGroup.setSelect(serial);

        Block block = City.currentCompany.blockList[serial];
        if (block.isEmpty)
        {
            details.gameObject.SetActive(false);
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
