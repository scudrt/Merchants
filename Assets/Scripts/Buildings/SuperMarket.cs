public class SuperMarket: Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        basicPrice = 300000f;
        buildingType = "SuperMarket";
        nickName = "超市" + buildingCount;
        if (blockBelong.companyBelong != null) {
            nickName = blockBelong.companyBelong.nickName + "的" + nickName;
        }
    }
    void Start() {
        this.onGenerate();
    }
}
