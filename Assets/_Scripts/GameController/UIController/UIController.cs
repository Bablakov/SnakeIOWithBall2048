using UnityEngine;
using Zenject;

public abstract class UIController : MonoBehaviour, IInitializable {
    private ButtonSoundControl _buttonSoundControl;

    public virtual void Initialize() {
        GetComponents();
        InitializeComponents();
    }

    protected virtual void GetComponents() {
        _buttonSoundControl = GetComponentInChildren<ButtonSoundControl>();
    }

    protected virtual void InitializeComponents() {
        _buttonSoundControl.Initialize();
    }
}