using UnityEngine;

public abstract class Sound : MonoBehaviour {
    protected AudioSource AudioSource;

    public virtual void Initialize(AudioClip clip) {
        GetComponent();
        SetClip(clip);
    }

    protected virtual void GetComponent() {
        AudioSource = GetComponent<AudioSource>();
    }

    private void SetClip(AudioClip clip) {
        AudioSource.clip = clip;
    }
}