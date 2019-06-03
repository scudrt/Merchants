public class Restaurant: Building {
    public override void upgrade() {
        ;
    }
    public override void makeMoney() {

    }
    public override void onGenerate() {
        this.price = 4000f;
        this.buildingType = "Restaurant";
    }
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

}
