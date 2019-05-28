using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour {
    public Building building;
    
    private Renderer blockRenderer;
    private Color blockColor,
        activeColor = Color.red, 
        chosenColor = Color.blue;
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

        this.price = 20000f;
        Debug.Log("Block onGenerate done");
    }
    
    public bool build(string buildingTypeName) {
        if (!isEmpty) { //current block isn't empty
            return false;
        }
        //load the prefab of building
        GameObject newBuilding = (GameObject)Resources.Load("Prefabs/" + buildingTypeName);
        newBuilding = GameObject.Instantiate(newBuilding, this.transform.position, new Quaternion());
        //set building's scale
        Vector3 blockScale = this.transform.localScale;
        Vector3 buildingScale = newBuilding.transform.localScale;
        buildingScale.x *= blockScale.x;
        buildingScale.y *= blockScale.y;
        buildingScale.z *= blockScale.z;
        newBuilding.transform.localScale = buildingScale;
        //attach script to the new building
        switch (buildingTypeName) {
            case "ArtGallery":
                this.building = newBuilding.AddComponent<ArtGallery>();
                break;
            case "Bank":
                this.building = newBuilding.AddComponent<Bank>();
                break;
            case "Cinema":
                this.building = newBuilding.AddComponent<Cinema>();
                break;
            case "Hospital":
                this.building = newBuilding.AddComponent<Hospital>();
                break;
            case "Restaurant":
                this.building = newBuilding.AddComponent<Restaurant>();
                break;
            case "Scenic":
                this.building = newBuilding.AddComponent<Scenic>();
                break;
            case "School":
                this.building = newBuilding.AddComponent<School>();
                break;
            case "Stadium":
                this.building = newBuilding.AddComponent<Stadium>();
                break;
            case "SuperMarket":
                this.building = newBuilding.AddComponent<SuperMarket>();
                break;
            default:
                this.building = newBuilding.AddComponent<Scenic>();
                //can throw an exception here
                break;
        }
        this.building.blockBelong = this;
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
