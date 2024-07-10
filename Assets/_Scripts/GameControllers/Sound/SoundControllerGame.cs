public class SoundControllerGame : SoundController {
    private SoundPickUpSection _soundPickUpSection;
    private SoundDeathPlayer _deathPlayer;
    private SoundDeathEnemy _deathEnemy;

    protected override void GetComponents() {
        base.GetComponents();
        _soundPickUpSection = GetComponentInChildren<SoundPickUpSection>();
        _deathPlayer = GetComponentInChildren<SoundDeathPlayer>();
        _deathEnemy = GetComponentInChildren<SoundDeathEnemy>();
    }

    protected override void InitializeComponents() {
        base.InitializeComponents();
        _soundPickUpSection.Initialize(SoundConfig.PickUpSection);
        _deathPlayer.Initialize(SoundConfig.DeathPlayer);
        _deathEnemy.Initialize(SoundConfig.DeathEnemy);
    }
}