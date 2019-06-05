using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TalentManageUI : MonoBehaviour
{
    public GameObject talentInfoPrefab;
    public GameObject blockInfoPrefab;

    private ScrollRect scrollrect;//talents' scroll view
    private Scrollbar scrollbar;//bar controlling the talents' scroll view
    private GameObject content;//content contains talent informations
    private RectTransform contentTR;

    private Building targetBuilding;//talent's distribution building

    private GameObject blocks;
    private GameObject blocksContent;
    private RectTransform blocksContentTr;

    //details panel's objects
    private int serial;//displayed talent's serial number in talents list
    private Transform details;
    private Text satisfaction;
    private Text talentName;
    private Text capacity;
    private Text charm;
    private Text salary;
    private Slider salaryController;
    private InputField salaryInput;
    private Text inputText;
    private Text status;
    
    // Start is called before the first frame update
    void Start()
    {
        talentInfoPrefab = (GameObject) Resources.Load("Prefabs/TalentContent");
        blockInfoPrefab = (GameObject)Resources.Load("Prefabs/BlockInfo");

        scrollrect = transform.Find("Talents").GetComponent<ScrollRect>();
        scrollbar = transform.Find("Talents").Find("Scrollbar").GetComponent<Scrollbar>();
        content = transform.Find("Talents").transform.Find("Content").gameObject;
        contentTR = content.GetComponent<RectTransform>();
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

        blocks = transform.Find("Blocks").gameObject;
        blocksContent = transform.Find("Blocks").Find("Content").gameObject;
        blocksContentTr = blocksContent.GetComponent<RectTransform>();

        details = transform.Find("Details");
        satisfaction = details.Find("Satisfaction").GetComponent<Text>();
        talentName = details.Find("Name").GetComponent<Text>();
        capacity = details.Find("Capacity").GetComponent<Text>();
        charm = details.Find("Charm").GetComponent<Text>();
        salary = details.Find("Salary").GetComponent<Text>();
        salaryController = details.Find("SalaryController").GetComponent<Slider>();
        salaryInput = details.Find("SalaryInput").GetComponent<InputField>();
        inputText = salaryInput.transform.Find("Placeholder").GetComponent<Text>();
        status = details.Find("Status").GetComponent<Text>();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroyItemInfo()
    {

    }

    // Init information when open the panel
    public void OnOpen()
    {
        //disable the details panel
        details.gameObject.SetActive(false);
        blocks.SetActive(false);

        //clear the previous content
        GameObject child;
        for(int j = 0; j < content.transform.childCount; j++)
        {
            child = content.transform.GetChild(j).gameObject;
            Destroy(child);
        }

        List<Talent> talents = City.currentCompany.talentList;
        GameObject talentInfo;
        RectTransform rectTransform;
        ItemInfo script;
        int i = 0;//i is the number of column

        foreach(Talent talent in talents)
        {
            //add talent's information
            talentInfo = GameObject.Instantiate(talentInfoPrefab, content.transform);
            rectTransform = talentInfo.GetComponent<RectTransform>();
            script = talentInfo.GetComponent<ItemInfo>();

            script.serial = i;

            //set position
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * 50, 50);

            //set text
            talentInfo.transform.Find("Name").GetComponent<Text>().text = talent.name;
            if (talent.workPlace != null)
                talentInfo.transform.Find("WorkingPlace").GetComponent<Text>().text = talent.workPlace.buildingType;
            else
                talentInfo.transform.Find("WorkingPlace").GetComponent<Text>().text = "待分配";

            i++;
        }

        //change content rect's height
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);
    }

    public void DisplayBlocks()
    {
        blocks.SetActive(true);

        //clear the previous content
        GameObject child;
        for (int j = 0; j < blocksContent.transform.childCount; j++)
        {
            child = blocksContent.transform.GetChild(j).gameObject;
            Destroy(child);
        }

        List<Block> blockList = City.currentCompany.blockList;
        GameObject blockInfo;
        RectTransform rectTransform;
        ItemInfo script;
        int i = 0;//i is the number of column

        foreach (Block block in blockList)
        {
            //add talent's information
            blockInfo = GameObject.Instantiate(blockInfoPrefab, blocksContent.transform);
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
                profitText = block.building.profitAmount.ToString();
            }
            blockInfo.transform.Find("Name").GetComponent<Text>().text = nameText;
            blockInfo.transform.Find("Profit").GetComponent<Text>().text = profitText;

            i++;
        }

        //change content rect's height
        blocksContentTr.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i * 50);
    }

    public void DisplayItemInfo(int _serial)
    {
        this.serial = _serial;
        details.gameObject.SetActive(true);

        Talent talent = City.currentCompany.talentList[_serial];

        talentName.text = talent.name;
        satisfaction.text = talent.satisfaction.ToString();
        capacity.text = talent.capacity.ToString();
        charm.text = talent.charm.ToString();
        salary.text = talent.salary.ToString();
        //status' content has not been decided yet
        salaryController.value = 5;
        salaryInput.text = salary.text;
    }

    public void OnSalaryControllValueChanged()
    {
        double salaryValue = System.Convert.ToDouble(salary.text);
        double currentValue = System.Convert.ToDouble(salaryInput.text);
        double delta = salaryValue * 0.1;//changed by 10 percents

        salaryInput.text = (salaryValue + (delta * (salaryController.value - 5))).ToString();
    }

    public void OnDecideButtonClicked()
    {
        Talent talent = City.currentCompany.talentList[serial];
        talent.salary = (float) System.Convert.ToDouble(salaryInput.text);
        this.DisplayItemInfo(serial);
    }

    public void OnFireButtonClicked()
    {
        City.currentCompany.talentList.RemoveAt(serial);
        this.OnOpen();
    }

    public void OnReturnButtonClicked()
    {
        blocks.SetActive(false);
    }
}
