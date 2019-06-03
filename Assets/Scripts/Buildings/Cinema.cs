public class Cinema : Building {
    public override void upgrade() {
        ;
    }
    public override void makeMoney() {

    }
    public override void onGenerate() {
        this.price = 8000f;
        this.buildingType = "Cinema";
    }
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

}
