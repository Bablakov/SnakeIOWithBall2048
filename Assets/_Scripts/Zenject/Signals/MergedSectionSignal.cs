public class MergedSectionSignal {
    public readonly StorageSection StorageSection;
    public readonly Section UpgradeSection;
    public readonly Section DeleteSection;

    public float Time = 0.3f;

    public MergedSectionSignal(StorageSection storageSection, Section upgradeSection, Section deleteSection) {
        StorageSection = storageSection;
        UpgradeSection = upgradeSection;
        DeleteSection = deleteSection;
    }
}