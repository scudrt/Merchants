public class Hospital: Building {
    public override void onGenerate() {
        this.price = 6000f;
        this.buildingType = "Hospital";
    }
    void Start() {
        this.onGenerate();
    }
}
