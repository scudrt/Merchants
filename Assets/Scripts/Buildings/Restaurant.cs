public class Restaurant: Building {
    public override void onGenerate() {
        this.price = 4000f;
        this.buildingType = "Restaurant";
    }
    void Start() {
        onGenerate();
    }
}
