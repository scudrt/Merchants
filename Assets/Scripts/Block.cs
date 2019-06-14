using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Block : MonoBehaviour {
    public int id;
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
    //block's price includes building price
    private float _blockPrice;
    public float price { get {
            return _blockPrice + (building == null ? 0 : building.price);
        }private set { }}

    //company which block belongs
    private Company _companyBelong;
    public Company companyBelong { get {
            return _companyBelong;
        } set {
            if (_companyBelong == value) {
                return;
            }
            _companyBelong = value;
            if (!isEmpty) { // clear profit record for new company
                building.clearRecord();
            }
            if (value != null) {
                this.color = this.blockColor = value.companyColor;
            } else {
                this.color = this.blockColor = Color.clear;
            }
        } }

    public bool isOwned{ //whether the block belongs to a company
        get{
            return this.companyBelong != null;
        }
        private set {}
    }
    public bool isEmpty { //whether there is a building on it
        get {
            return this.building == null;
        }
        private set{}
    }

    // Use this for initialization
    void Start()
    {
        onGenerate();
    }
	
	// Update is called once per frame


    private void onGenerate() {
        this._isChosen = false;

        this.blockRenderer = GetComponent<Renderer>();
        this.blockColor = blockRenderer.material.color;

        this.building = null;
        this.companyBelong = null;

        _blockPrice = 100000f;
        Debug.Log("Block onGenerate done");
    }

    public bool build(string buildingTypeName) {
        if (!isEmpty) { //current block isn't empty
            return false;
        }

        //pay for the building
        if (this.isOwned){
            if (companyBelong.costMoney((float)Type.GetType(buildingTypeName).GetField("PRICE").GetValue(0)) == false) {
                //don't have enough money
                return false;
            }
        }

        //load the prefab of building
        GameObject newBuilding = (GameObject)Resources.Load("Prefabs/" + buildingTypeName);
        newBuilding = GameObject.Instantiate(newBuilding, this.transform.position, newBuilding.transform.rotation);

        //set prefab's scale
        Vector3 blockScale = this.transform.localScale;
        Vector3 buildingScale = newBuilding.transform.localScale;
        buildingScale.x *= blockScale.x;
        buildingScale.y *= blockScale.y;
        buildingScale.z *= blockScale.z;
        newBuilding.transform.localScale = buildingScale;

        //bind script with building prefab
        this.building = (Building)newBuilding.AddComponent(Type.GetType(buildingTypeName));
        this.building.blockBelong = this;

        //remember to send message to network

        return true;
    }

    public void sellBuilding() {
        this.building.onDestory();
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
                blockUI.SendMessage("EmptyBlockPanelEntry");
            }
        }
    }
}
