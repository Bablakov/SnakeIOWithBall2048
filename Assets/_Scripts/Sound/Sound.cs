using UnityEngine;

public abstract class Sound : MonoBehaviour {
    protected AudioSource AudioSource;

    public virtual void Initialize() {
        GetComponent();
    }

    protected virtual void GetComponent() {
        AudioSource = GetComponent<AudioSource>();
    }
}