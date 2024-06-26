using UnityEngine;

public class SoundBackground : Sound {
    public override void Initialize(AudioClip clip) {
        base.Initialize(clip);
        StartPlay();
    }

    private void StartPlay() {
        AudioSource.loop = true;
        AudioSource.Play();
    }
}