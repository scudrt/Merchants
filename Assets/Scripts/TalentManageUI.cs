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

    // Start is called before the first frame update
    void Start()
    {
        currentCompany = City.companyList[0];
        talents = transform.Find("Talents").GetComponent<ScrollRect>();
        scrollbar = talents.transform.Find("Scrollbar Vertical").GetComponent<Scrollbar>();
        viewport = transform.Find("Viewport").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameVisible()
    {
        foreach(Talent talent in currentCompany.talentList)
        {
            GameObject talentinfo = (GameObject)GameObject.Instantiate(talentInfoPrefab,viewport.transform);
            talentinfo.transform.Find("Name").GetComponent<Text>().text = talent.name;
        }
    }

    private void OnBecameInvisible()
    {
        
    }
}
