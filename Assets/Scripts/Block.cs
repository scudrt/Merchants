using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour {

    public GameObject block; //Block located on this block

    public Canvas UI;//Canvas displayed when the Block is clicked

    //Block's attribute
    public float price { get; set; }
    public int belong { get; set; }
    public bool isOwned{
        get{
            return this.belong != 0;
        }
        set {
            if (value == false){
                this.belong = 0;
            }
        }
    }
    public bool isEmpty {
        get {
            return this.building == null;
        }
        private set{}
    }

    public Building building;
    private Renderer renderer;
    private Color defaultColor;

    

    // Use this for initialization
    void Start () {
        renderer = GetComponent<Renderer>();
        defaultColor = renderer.material.color;

        this.isOwned = this.isEmpty = false;
        this.belong = 0;
        this.price = 100f;
        this.building = null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool canBeOwned(){
        return this.belong == 0;
    }

    public bool canBuild() {
        return this.isEmpty;
    }

    public void build() {
        ;//??
    }
    private void OnMouseOver(){
        if (EventSystem.current.IsPointerOverGameObject())
        {
            renderer.material.color = defaultColor;
        }
        else
        {
            renderer.material.color = Color.red;
        }
    }

    private void OnMouseEnter(){
        if (!EventSystem.current.IsPointerOverGameObject())
            renderer.material.color = Color.red; 
    }

    private void OnMouseExit(){
        if (!EventSystem.current.IsPointerOverGameObject())
            renderer.material.color = defaultColor;
    }

    private void OnMouseDown(){
        if (EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        BlockUI script = UI.GetComponent<BlockUI>();
        script.Block = gameObject;

        //display UI according to the Block's status
        if (this.isOwned){
            if (this.isEmpty){
                script.SendMessage("BuildingPanelEntry");
            }
            else{
                script.SendMessage("BoughtPanelEntry");
            }
        }
        else{
            if (this.isEmpty){
                script.SendMessage("EmptyPanelEntry");
            }
            else{
                script.SendMessage("NaturalBlockEntry");
            }
        }
    }
}
