public class School: Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        price = 7000f;
        buildingType = "School";
        nickName = blockBelong.companyBelong.nickName + "的学校" + buildingCount;
    }
    void Start() {
        onGenerate();
    }
}
