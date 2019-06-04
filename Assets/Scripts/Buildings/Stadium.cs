public class Stadium: Building {
    public override void onGenerate() {
        this.price = 3000f;
        this.buildingType = "Stadium";
    }
    void Start() {
        onGenerate();
    }
}
