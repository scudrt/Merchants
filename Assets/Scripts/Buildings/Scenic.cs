public class Scenic: Building {
    public override void upgrade() {
        ;
    }
    public override void makeMoney() {

    }
    public override void onGenerate() {
        this.price = 6000f;
        this.buildingType = "Scenic";
    }
    void Start() {
        this.onGenerate();
    }

    // Update is called once per frame
    void Update() {

    }

}

