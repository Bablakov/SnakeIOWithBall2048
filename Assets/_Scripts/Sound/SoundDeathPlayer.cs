using UnityEngine;
using Zenject;

public class SoundDeathPlayer : Sound {
    private SignalBus _signalBus;
    private AudioClip _clip;

    public override void Initialize() {
        base.Initialize();
        AudioSource.clip = _clip;
        Subscribe();
    }

    [Inject]
    private void Construct(SoundConfig config, SignalBus signalBus) {
        _clip = config.DeathPlayer;
        _signalBus = signalBus;
    }

    private void Subscribe() {
        _signalBus.Subscribe<DiedPlayerSignal>(PlaySound);
    }

    private void Unsubscribe() {
        _signalBus.Unsubscribe<DiedPlayerSignal>(PlaySound);
    }

    private void PlaySound() {
        AudioSource.pitch = Random.Range(0.90f, 1.10f);
        AudioSource.Play();
    }
}