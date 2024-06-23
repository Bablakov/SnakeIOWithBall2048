using UnityEngine;
using Zenject;

public class SoundPickUpSection : Sound {
    private SignalBus _signalBus;
    private AudioClip _clip;

    public override void Initialize() {
        base.Initialize();
        AudioSource.clip = _clip;
        Subscribe();
    }

    [Inject]
    private void Construct(SoundConfig config, SignalBus signalBus) {
        _clip = config.PickUpSection;
        _signalBus = signalBus;
    }

    private void Subscribe() {
        _signalBus.Subscribe<PutOnSectionSignal>(PlaySound);
    }

    private void Unsubscribe() {
        _signalBus.Unsubscribe<PutOnSectionSignal>(PlaySound);
    }

    private void PlaySound(PutOnSectionSignal signal) {
        AudioSource.pitch = Random.Range(0.90f, 1.10f);
        AudioSource.Play();
        Debug.Log("SoundPickUp");
    }
}