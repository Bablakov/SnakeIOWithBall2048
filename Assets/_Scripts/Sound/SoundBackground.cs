using UnityEngine;
using Zenject;

public class SoundBackground : Sound {
    private AudioClip _background;

    public override void Initialize() {
        base.Initialize();
        StartPlay();
    }

    [Inject]
    private void Construct(SoundConfig config) {
        _background = config.Background;
    }

    private void StartPlay() {
        AudioSource.clip = _background;
        AudioSource.loop = true;
        AudioSource.Play();
    }
}