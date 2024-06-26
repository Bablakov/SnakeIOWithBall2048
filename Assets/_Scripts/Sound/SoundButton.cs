using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundButton : Sound /*IDisposable*/ {
    /*private AudioClip _soundClickButton;

    public override void Initialize() {
        base.Initialize();
        Subscribe();
    }

    private void Subscribe() {
    }

    private void Unsubscribe() {
    }

    private void OnClickedButton() {
        AudioSource.clip = _soundClickButton;
        AudioSource.pitch = Random.Range(0.90f, 1.10f);
        AudioSource.Play();
    }

    public void Dispose() {
        Unsubscribe();
    }*/
}