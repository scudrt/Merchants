using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour {
    string buildingPrefabName { get; set; }
    public Building building;
    private Renderer blockRenderer;
    private Color blockColor;
    public Color color { get {
            return this.blockColor;
        }
        set {
            this.blockRenderer.material.color = this.blockColor = value;
        }
    }
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
        this.onGenerate();
    }

    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void onGenerate() {
        blockRenderer = GetComponent<Renderer>();
        color = blockRenderer.material.color;

        this.building = null;
        this.companyBelong = null;

        this.buildingPrefabName = "prefabTower";
        this.price = 100f;
        Debug.Log("Block onGenerate done");
    }
    
    public bool build(string prefabName = "") {
        if (!isEmpty) {
            return false;
        }
        if (prefabName != "") {
            this.buildingPrefabName = prefabName;
        }
        GameObject newBuilding = GameObject.FindGameObjectWithTag(this.buildingPrefabName);
        newBuilding = GameObject.Instantiate(newBuilding, this.transform.position, new Quaternion());
        this.building = newBuilding.AddComponent<Building>();
        return true;
    }

    public void sellBuilding() {
        if (companyBelong != null) {
            companyBelong.fund += building.price;
        }
        GameObject.DestroyImmediate(building.GetComponentInParent<GameObject>());
        this.building = null;
    }

    private void OnMouseEnter(){
        if (!EventSystem.current.IsPointerOverGameObject()) {
            blockRenderer.material.color = Color.red;
        }
    }

    private void OnMouseExit(){
        blockRenderer.material.color = blockColor;
    }

    private void OnMouseDown(){
        if (EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        BlockUI blockUI = Canvas.FindObjectOfType<BlockUI>();
        blockUI.targetBlock = this;

        //display UI according to the Block's status
        if (this.isOwned){
            if (this.isEmpty) {
                blockUI.SendMessage("BoughtPanelEntry");
            }
            else {
                blockUI.SendMessage("BuildingPanelEntry");
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
