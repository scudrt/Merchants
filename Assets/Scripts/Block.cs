using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour {    

    public Building building;
    private Renderer blockRenderer;
    private Color defaultColor;
    //Block's attribute
    public float price { get; set; }
    public Company companyBelong { get; set; }
    public bool isOwned{
        get{
            return this.companyBelong != null;
        }
        private set {}
    }
    public bool isEmpty {
        get {
            return this.building == null;
        }
        private set{}
    }

    // Use this for initialization
    public void Awake() {
        this.onGnerate();
    }

    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void onGnerate() {
        blockRenderer = GetComponent<Renderer>();
        defaultColor = blockRenderer.material.color;

        this.isOwned = false;
        this.isEmpty = true;

        this.building = null;
        this.companyBelong = null;

        this.price = 100f;
        Debug.Log("Block onGenerate done");
    }
    
    public void build() {
        ;//??
    }

    public void sellBuilding() {
        companyBelong.fund += building.price;
    }
    private void OnMouseOver(){
        if (EventSystem.current.IsPointerOverGameObject())
        {
            blockRenderer.material.color = defaultColor;
        }
        else
        {
            blockRenderer.material.color = Color.red;
        }
    }

    private void OnMouseEnter(){
        if (!EventSystem.current.IsPointerOverGameObject()) {
            blockRenderer.material.color = Color.red;
        }
    }

    private void OnMouseExit(){
        if (!EventSystem.current.IsPointerOverGameObject())
            blockRenderer.material.color = defaultColor;
    }

    private void OnMouseDown(){
        if (EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        BlockUI blockUI = Canvas.FindObjectOfType<BlockUI>();
        blockUI.targetBlock = this;

        //display UI according to the Block's status
        if (this.isOwned){
            if (this.isEmpty){
                blockUI.SendMessage("BuildingPanelEntry");
            }
            else{
                blockUI.SendMessage("BoughtPanelEntry");
            }
        }
        else{
            if (this.isEmpty){
                blockUI.SendMessage("EmptyPanelEntry");
            }
            else{
                blockUI.SendMessage("NaturalBlockEntry");
            }
        }
    }
}
