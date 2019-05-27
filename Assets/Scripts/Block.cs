using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour {
    public Building building;
    
    private Renderer blockRenderer;
    private Color blockColor, activeColor = Color.red, chosenColor = Color.blue;
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
    void Start () {
        this.onGenerate();
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void onGenerate() {
        this.blockRenderer = GetComponent<Renderer>();
        this.color = blockRenderer.material.color;

        this.building = null;
        this.companyBelong = null;

        this.price = 50000f;
        Debug.Log("Block onGenerate done");
    }
    
    public bool build(string prefabName = "Tower") {
        if (!isEmpty) { //current block isn't empty
            return false;
        }
        GameObject newBuilding = (GameObject)Resources.Load("Prefabs/" + prefabName);
        newBuilding = GameObject.Instantiate(newBuilding, this.transform.position, new Quaternion());
        Vector3 blockScale = this.transform.localScale;
        Vector3 buildingScale = newBuilding.transform.localScale;
        buildingScale.x *= blockScale.x;
        buildingScale.y *= blockScale.y;
        buildingScale.z *= blockScale.z;
        newBuilding.transform.localScale = buildingScale;
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

    private void OnMouseOver(){
        if (!EventSystem.current.IsPointerOverGameObject()) {
            blockRenderer.material.color = Color.red;
        } else {
            blockRenderer.material.color = this.blockColor;
        }
    }

    private void OnMouseExit(){
        blockRenderer.material.color = blockColor;
    }

    private void OnMouseDown(){
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        BlockUI blockUI = Canvas.FindObjectOfType<BlockUI>();
        blockUI.targetBlock = this;

        GameObject.FindObjectOfType<BlockButton>().SendMessageUpwards("UIExit");
        //display UI according to the Block's status
        if (this.isOwned){
            if (this.isEmpty) {
                blockUI.SendMessage("OwnedBlockPanelEntry");
            }
            else {
                blockUI.SendMessage("BuildingInfoPanelEntry");
            }
        }
        else{
            if (this.isEmpty){
                blockUI.SendMessage("EmptyBlockPanelEntry");
            }
            else{
                blockUI.SendMessage("NaturalBlockEntry");
            }
        }
    }
}
