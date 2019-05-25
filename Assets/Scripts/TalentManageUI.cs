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
    private void Awake() {
    }
    // Start is called before the first frame update
    void Start()
    {
        //talentInfoPrefab = (GameObject) Resources.Load("Prefabs/TalentContent");

        scrollrect = transform.Find("Talents").GetComponent<ScrollRect>();
        scrollbar = transform.Find("Talents").Find("Scrollbar").GetComponent<Scrollbar>();
        content = transform.Find("Talents").transform.Find("Content").gameObject;
        contentTR = content.GetComponent<RectTransform>();
        contentTR.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        Debug.Log("Talent management complete");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Init information when open the panel
    public void OnOpen()
    {
        List<Talent> talents = City.currentCompany.talentList;
        GameObject talentInfo;
        RectTransform rectTransform;
        int i = 0;
        Debug.Log(content == null);
        Debug.Log(scrollrect == null);
        foreach(Talent talent in talents)
        {
            talentInfo = GameObject.Instantiate(talentInfoPrefab, content.transform);
            rectTransform = talentInfo.GetComponent<RectTransform>();
            rectTransform.anchoredPosition.Set(i * 50, 0);
            talentInfo.transform.Find("Name").GetComponent<Text>().text = talent.name;
            if (talent.workPlace != null)
                talentInfo.transform.Find("WorkingPlace").GetComponent<Text>().text = talent.workPlace.buildingType;
            else
                talentInfo.transform.Find("WorkingPlace").GetComponent<Text>().text = "待分配";
        }
    }

    private void OnBecameInvisible()
    {
        
    }
}
