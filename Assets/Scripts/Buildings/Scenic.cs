public class Scenic: Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        price = 6000f;
        buildingType = "Scenic";
        nickName = blockBelong.companyBelong.nickName + "的景区" + buildingCount;
    }
    void Start() {
        this.onGenerate();
    }
    
}

