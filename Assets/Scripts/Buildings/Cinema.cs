public class Cinema : Building {
    public override void onGenerate() {
        this.price = 8000f;
        this.buildingType = "Cinema";
    }
    void Start() {
        onGenerate();
    }
}
