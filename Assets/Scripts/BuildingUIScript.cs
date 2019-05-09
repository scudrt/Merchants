using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BuildingUIScript : MonoBehaviour
{

    public GameObject Building;//which building's information to display

    private GameObject emptyPanel;
    private GameObject buildingPanel;
    private GameObject boughtPanel; //UI when the building is bought but not constructed any building

    // Start is called before the first frame update
    void Start()
    {
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
        emptyPanel.GetComponent<UIPanelScript>().UIEntry();

        //set the information
        emptyPanel.transform.Find("LandPrice").GetComponent<Text>().text
            = Building.GetComponent<BuildingScript>().LandPrice.ToString();
    }

    public void BuildingPanelEntry()
    {
        //display UI
        buildingPanel.GetComponent<UIPanelScript>().UIEntry();

        //set the information
    }

    public void BoughtPanelEntry()
    {
        //display UI
        boughtPanel.GetComponent<UIPanelScript>().UIEntry();

        //set the information
    }
}
