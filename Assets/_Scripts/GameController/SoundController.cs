using UnityEngine;
using Zenject;

public class SoundController : MonoBehaviour, IInitializable {
    private SoundPickUpSection _soundPickUpSection;
    private SoundBackground _soundBackground;
    private SoundDeathPlayer _deathPlayer;
    private SoundDeathEnemy _deathEnemy;
    private SoundConfig _soundConfig;

    public void Initialize() {
        GetComponents();
        InitializeComponents();
    }

    [Inject]
    private void Construct(SoundConfig soundConfig) {
        _soundConfig = soundConfig;
    }

    private void GetComponents() {
        _soundPickUpSection = GetComponentInChildren<SoundPickUpSection>();
        _soundBackground = GetComponentInChildren<SoundBackground>();
        _deathPlayer = GetComponentInChildren<SoundDeathPlayer>();
        _deathEnemy = GetComponentInChildren<SoundDeathEnemy>();
    }

    private void InitializeComponents() {
        _soundPickUpSection.Initialize(_soundConfig.PickUpSection);
        _soundBackground.Initialize(_soundConfig.Background);
        _deathPlayer.Initialize(_soundConfig.DeathPlayer);
        _deathEnemy.Initialize(_soundConfig.DeathEnemy);
    }
}