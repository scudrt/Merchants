public class Restaurant: Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        price = 4000f;
        buildingType = "Restaurant";
        nickName = blockBelong.companyBelong.nickName + "的餐厅" + buildingCount;
    }
    void Start() {
        onGenerate();
    }
}
