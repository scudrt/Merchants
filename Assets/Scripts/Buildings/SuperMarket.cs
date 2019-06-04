public class SuperMarket: Building {
    public override void onGenerate() {
        this.price = 3000f;
        this.buildingType = "SuperMarket";
    }
    void Start() {
        this.onGenerate();
    }
}
