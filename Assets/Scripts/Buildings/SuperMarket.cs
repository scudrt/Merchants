public class SuperMarket: Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        price = 3000f;
        buildingType = "SuperMarket";
        nickName = blockBelong.companyBelong.nickName + "的超市" + buildingCount;
    }
    void Start() {
        this.onGenerate();
    }
}
