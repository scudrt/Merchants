public class Cinema : Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        price = 8000f;
        buildingType = "Cinema";
        nickName = blockBelong.companyBelong.nickName + "的电影院" + buildingCount;
    }
    void Start() {
        onGenerate();
    }
}
