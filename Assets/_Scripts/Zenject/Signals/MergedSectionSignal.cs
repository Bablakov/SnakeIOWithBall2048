public class MergedSectionSignal {
    public readonly Section UpgradeSection;
    public readonly Section DeleteSection;

    public MergedSectionSignal(Section upgradeSection, Section deleteSection) {
        UpgradeSection = upgradeSection;
        DeleteSection = deleteSection;
    }
}