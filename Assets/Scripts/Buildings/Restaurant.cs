public class Restaurant: Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        basicPrice = 30000f;
        buildingType = "Restaurant";
        nickName = "餐厅" + buildingCount;
        if (blockBelong.companyBelong != null) {
            nickName = blockBelong.companyBelong.nickName + "的" + nickName;
        }
    }
    void Start() {
        onGenerate();
    }
}
