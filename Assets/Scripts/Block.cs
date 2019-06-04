using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour {
    public Building building;
    public int Pos_x { set; get; }
    public int Pos_y { set; get; }
    private bool _isChosen;
    public bool isChosen { set {
            _isChosen = value;
            if (value == true) {
                this.color = this.chosenColor;
            } else {
                this.color = this.blockColor;
            }
        } get {
            return _isChosen;
        }}
    private Renderer blockRenderer;
    private Color blockColor,
        activeColor = Color.red, 
        chosenColor = Color.blue;
    public Color color { get {
            return this.blockRenderer.material.color;
        }
        set {
            this.blockRenderer.material.color = value;
        }
    }
    //Block's attribute
    public float price { get; set; }
    private Company _companyBelong;
    public Company companyBelong { get {
            return _companyBelong;
        } set {
            _companyBelong = value;
            if (value != null) {
                this.blockColor = value.companyColor;
            }
        } }
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
        this._isChosen = false;

        this.blockRenderer = GetComponent<Renderer>();
        this.blockColor = blockRenderer.material.color;

        this.building = null;
        this.companyBelong = null;

        this.price = 5000f;
        Debug.Log("Block onGenerate done");
    }
    
    public bool build(string buildingTypeName) {
        if (!isEmpty) { //current block isn't empty
            return false;
        }
        //attach script to the new building
        switch (buildingTypeName) {
            case "ArtGallery":
                this.building = gameObject.AddComponent<ArtGallery>();
                break;
            case "Bank":
                this.building = gameObject.AddComponent<Bank>();
                break;
            case "Cinema":
                this.building = gameObject.AddComponent<Cinema>();
                break;
            case "Hospital":
                this.building = gameObject.AddComponent<Hospital>();
                break;
            case "Restaurant":
                this.building = gameObject.AddComponent<Restaurant>();
                break;
            case "Scenic":
                this.building = gameObject.AddComponent<Scenic>();
                break;
            case "School":
                this.building = gameObject.AddComponent<School>();
                break;
            case "Stadium":
                this.building = gameObject.AddComponent<Stadium>();
                break;
            case "SuperMarket":
                this.building = gameObject.AddComponent<SuperMarket>();
                break;
            default:
                this.building = gameObject.AddComponent<Scenic>();
                //can throw an exception here
                break;
        }
        //pay for the building
        if (this.companyBelong.costMoney(this.building.price) == false) {
            //don't have enough money, return false
            GameObject.Destroy(this.building);
            this.building = null;
            return false;
        } else {
            this.building.blockBelong = this;
        }
        //load the prefab of building
        GameObject newBuilding = (GameObject)Resources.Load("Prefabs/" + buildingTypeName);
        newBuilding = GameObject.Instantiate(newBuilding, this.transform.position, newBuilding.transform.rotation);
        //set building's scale
        Vector3 blockScale = this.transform.localScale;
        Vector3 buildingScale = newBuilding.transform.localScale;
        buildingScale.x *= blockScale.x;
        buildingScale.y *= blockScale.y;
        buildingScale.z *= blockScale.z;
        newBuilding.transform.localScale = buildingScale;
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
        if (this.isChosen) {
            this.color = this.chosenColor;
        }else if (!EventSystem.current.IsPointerOverGameObject()) {
            this.color = Color.red;
        } else {
            this.color = this.blockColor;
        }
    }

    private void OnMouseExit(){
        if (this.isChosen) {
            this.color = this.chosenColor;
        } else {
            this.color = blockColor;
        }
    }

    private void OnMouseDown(){
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        BlockUI blockUI = Canvas.FindObjectOfType<BlockUI>();
        blockUI.setBlock(this);

        GameObject.FindObjectOfType<BlockUI>().BroadcastMessage("UIExit");
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
