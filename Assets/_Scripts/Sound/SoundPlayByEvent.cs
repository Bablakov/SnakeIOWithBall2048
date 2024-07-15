using UnityEngine;

public abstract class SoundPlayByEvent : Sound {

    public override void Initialize(AudioClip clip) {
        base.Initialize(clip);
        Subscribe();
    }

    protected void PlaySound() {
        AudioSource.pitch = Random.Range(0.90f, 1.10f);
        AudioSource.Play();
    }

    private void OnDestroy() {
        Unsubscribe();
    }

    protected abstract void Subscribe();
    protected abstract void Unsubscribe();
}