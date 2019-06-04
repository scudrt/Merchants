public class Bank : Building {
    public override void onGenerate() {
        this.price = 12000f;
        this.buildingType = "Bank";
    }
    // Start is called before the first frame update
    void Start() {
        onGenerate();
    }
}
