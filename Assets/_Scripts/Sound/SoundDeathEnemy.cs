using UnityEngine;
using Zenject;

public class SoundDeathEnemy : Sound {
    private SignalBus _signalBus;
    private AudioClip _clip;

    public override void Initialize() {
        base.Initialize();
        AudioSource.clip = _clip;
        Subscribe();
    }

    [Inject]
    private void Construct(SoundConfig config, SignalBus signalBus) {
        _clip = config.DeathEnemy;
        _signalBus = signalBus;
    }

    private void Subscribe() {
        _signalBus.Subscribe<DiedEnemySignal>(PlaySound);
    }

    private void Unsubscribe() {
        _signalBus.Unsubscribe<DiedEnemySignal>(PlaySound);
    }

    private void PlaySound() {
        AudioSource.pitch = Random.Range(0.90f, 1.10f);
        AudioSource.Play();
    }
}