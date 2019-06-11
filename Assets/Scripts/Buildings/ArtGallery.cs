public class ArtGallery: Building {
    public new static float PRICE = 120000f;
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        basicPrice = PRICE;
        buildingType = "ArtGallery";
        nickName = "美术馆" + buildingCount;
        if (blockBelong.companyBelong != null) {
            nickName = blockBelong.companyBelong.nickName + "的" + nickName;
        }
    }
    void Start() {
        onGenerate();
    }
}
