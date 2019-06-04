public class School: Building {
    public override void onGenerate() {
        this.price = 7000f;
        this.buildingType = "School";
    }
    void Start() {
        onGenerate();
    }
}
