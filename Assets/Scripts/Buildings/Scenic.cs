public class Scenic: Building {
    public override void onGenerate() {
        this.price = 6000f;
        this.buildingType = "Scenic";
    }
    void Start() {
        this.onGenerate();
    }
    
}

