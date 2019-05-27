using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TalentManageUI : MonoBehaviour
{
    public GameObject talentInfoPrefab;

    private ScrollRect scrollrect;//talents' scroll view
    private Scrollbar scrollbar;//bar controlling the talents' scroll view
    private GameObject content;//content contains talent informations
    private RectTransform contentTR;

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

        scrollrect = transform.Find("Talents").GetComponent<ScrollRect>();
        scrollbar = transform.Find("Talents").Find("Scrollbar").GetComponent<Scrollbar>();
        content = transform.Find("Talents").transform.Find("Content").gameObject;
        contentTR = content.GetComponent<RectTransform>();
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

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

    // Init information when open the panel
    public void OnOpen()
    {
        //disable the details panel
        details.gameObject.SetActive(false);

        //clear the previous content
        BroadcastMessage("DestroyTalentInfo");

        List<Talent> talents = City.currentCompany.talentList;
        GameObject talentInfo;
        RectTransform rectTransform;
        TalentInfo script;
        int i = 0;//i is the number of column

        foreach(Talent talent in talents)
        {
            //add talent's information
            talentInfo = GameObject.Instantiate(talentInfoPrefab, content.transform);
            rectTransform = talentInfo.GetComponent<RectTransform>();
            script = talentInfo.GetComponent<TalentInfo>();

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

        Debug.Log(contentTR.sizeDelta);
    }

    public void DisplayTalentInfo(int _serial)
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
        this.DisplayTalentInfo(serial);
    }

    public void OnFireButtonClicked()
    {
        City.currentCompany.talentList.RemoveAt(serial);
        this.OnOpen();
    }
}
