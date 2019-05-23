using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TalentManageUI : MonoBehaviour
{
    public GameObject talentInfoPrefab;

    private Company currentCompany;//player's company
    private ScrollRect talents;//talents' scroll view
    private Scrollbar scrollbar;//bar controlling the talents' scroll view
    private GameObject viewport;//viewport contain talents' information

    private void Awake() {
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        talentInfoPrefab = (GameObject)Resources.Load("Prefabs/TalentContent");
        currentCompany = City.currentCompany;
        talents = transform.Find("Talents").GetComponent<ScrollRect>();
        scrollbar = talents.transform.Find("Scrollbar Vertical").GetComponent<Scrollbar>();
        viewport = transform.Find("Viewport").gameObject;

        Debug.Log("OnTalnetManageStart:" + currentCompany);

        /********test code*********/
        Talent test = new Talent();
        test.name = "DRT";
        test.workPlace = null;
        currentCompany.talentList.Add(test);
        /********test code*********/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOpen()
    {
        //init the talents list scroll view
        foreach(Talent talent in currentCompany.talentList)
        {
            GameObject talentinfo = (GameObject)GameObject.Instantiate(talentInfoPrefab,viewport.transform);
            talentinfo.transform.Find("Name").GetComponent<Text>().text = talent.name;
            if (talent.workPlace != null)
                talentinfo.transform.Find("WorkingPlace").GetComponent<Text>().text = talent.workPlace.buildingType;
            else
                talentinfo.transform.Find("WorkingPlace").GetComponent<Text>().text = "暂未分配";
        }
    }

    private void OnBecameInvisible()
    {
        
    }
}
