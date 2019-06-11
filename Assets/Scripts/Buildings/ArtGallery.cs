public class ArtGallery: Building {
    private static int buildingCount = 0; //count of this type of building
    public override void onGenerate() {
        ++buildingCount;

        basicPrice = 120000f;
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
