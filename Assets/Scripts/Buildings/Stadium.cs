public class Stadium: Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        basicPrice = 40000f;
        buildingType = "Stadium";
        nickName = "体育场" + buildingCount;
        if (blockBelong.companyBelong != null) {
            nickName = blockBelong.companyBelong.nickName + "的" + nickName;
        }
    }
    void Start() {
        onGenerate();
    }
}
