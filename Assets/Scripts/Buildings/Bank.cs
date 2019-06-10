public class Bank : Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        price = 20000f;
        buildingType = "Bank";
        nickName = "银行" + buildingCount;
        if (blockBelong.companyBelong != null) {
            nickName = blockBelong.companyBelong.nickName + "的" + nickName;
        }
    }
    // Start is called before the first frame update
    void Start() {
        onGenerate();
    }
}
