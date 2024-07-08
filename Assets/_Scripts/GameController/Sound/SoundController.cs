using Zenject;
using UnityEngine;

public abstract class SoundController : MonoBehaviour, IInitializable {
    protected SoundConfig SoundConfig;

    private SoundBackground _soundBackground;

    public void Initialize() {
        GetComponents();
        InitializeComponents();
    }

    [Inject]
    private void Construct(SoundConfig soundConfig) {
        SoundConfig = soundConfig;
    }

    protected virtual void GetComponents() {
        _soundBackground = GetComponentInChildren<SoundBackground>();
    }

    protected virtual void InitializeComponents() {
        _soundBackground.Initialize(SoundConfig.Background);
    }
}