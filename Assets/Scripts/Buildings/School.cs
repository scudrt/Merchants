public class School: Building {
    public override void upgrade() {
        ;
    }
    public override void makeMoney() {

    }
    public override void onGenerate() {
        this.price = 7000f;
        this.buildingType = "School";
    }
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

}
