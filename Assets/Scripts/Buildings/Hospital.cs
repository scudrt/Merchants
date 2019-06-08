public class Hospital: Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        price = 6000f;
        buildingType = "Hospital";
        nickName = blockBelong.companyBelong.nickName + "的医院" + buildingCount;
    }
    void Start() {
        this.onGenerate();
    }
}
