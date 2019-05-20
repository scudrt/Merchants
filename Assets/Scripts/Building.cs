using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour {

    public GameObject building; //building located on this block

    public Canvas UI;//Canvas displayed when the building is clicked

    public enum Status { EMPTY, BOUGHT, BUILT }

    //building's attribute
    public bool IsEmpty { get; set; }
    public double LandPrice { get; set; }
    public double BuildingPrice { get; set; }
    public Status status { get; set; }
    private Renderer renderer;
    private Color defaultColor;

    

    // Use this for initialization
    void Start () {
        status = Status.EMPTY;
        renderer = GetComponent<Renderer>();
        defaultColor = renderer.material.color;

        LandPrice = 100.1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            renderer.material.color = defaultColor;
        }
        else
        {
            renderer.material.color = Color.red;
        }
    }

    void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            renderer.material.color = Color.red; 
    }

    void OnMouseExit()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            renderer.material.color = defaultColor;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        BuildingUI script = UI.GetComponent<BuildingUI>();
        script.Building = gameObject;

        //display UI according to the building's status
        switch (status)
        {
            case Status.EMPTY: script.SendMessage("EmptyPanelEntry"); break;
            case Status.BUILT: script.SendMessage("BuildingPanelEntry"); break;
            case Status.BOUGHT: script.SendMessage("BoughtPanelEntry"); break;
        }   
    }
}
