using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour {

    public GameObject building; //building located on this block

    public Canvas UI;//Canvas displayed when the building is clicked

    //building's attribute
    public bool IsEmpty { get; set; }
    public double LandPrice { get; set; }
    public double BuildingPrice { get; set; }

    private Renderer renderer;
    private Color defaultColor;

    

    // Use this for initialization
    void Start () {
        renderer = GetComponent<Renderer>();
        defaultColor = renderer.material.color;

        LandPrice = 100.1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseEnter()
    {
        renderer.material.color = Color.red; 
    }

    void OnMouseExit()
    {
        renderer.material.color = defaultColor;
    }

    void OnMouseDown()
    {
        EmptyBuildingUIScript script = UI.GetComponent<EmptyBuildingUIScript>();
        script.Building = gameObject;
        script.SendMessage("UIEntry");
    }
}
