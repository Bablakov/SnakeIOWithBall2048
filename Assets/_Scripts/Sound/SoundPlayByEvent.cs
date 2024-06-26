using UnityEngine;

public abstract class SoundPlayByEvent : Sound {
    protected void PlaySound() {
        AudioSource.pitch = Random.Range(0.90f, 1.10f);
        AudioSource.Play();
    }
}