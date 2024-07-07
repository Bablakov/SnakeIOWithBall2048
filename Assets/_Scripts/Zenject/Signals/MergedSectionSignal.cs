public class MergedSectionSignal {
    public readonly StorageSection StorageSection;
    public readonly Section UpgradeSection;
    public readonly Section DeleteSection;

    public float Time = 1f;

    public MergedSectionSignal(StorageSection storageSection, Section upgradeSection, Section deleteSection) {
        StorageSection = storageSection;
        UpgradeSection = upgradeSection;
        DeleteSection = deleteSection;
    }
}