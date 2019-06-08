public class Stadium: Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        price = 3000f;
        buildingType = "Stadium";
        nickName = blockBelong.companyBelong.nickName + "的体育场" + buildingCount;
    }
    void Start() {
        onGenerate();
    }
}
