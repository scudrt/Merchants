public class Scenic: Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        basicPrice = 400000f;
        buildingType = "Scenic";
        nickName = "景区" + buildingCount;
        if (blockBelong.companyBelong != null) {
            nickName = blockBelong.companyBelong.nickName + "的" + nickName;
        }
    }
    void Start() {
        this.onGenerate();
    }
    
}

