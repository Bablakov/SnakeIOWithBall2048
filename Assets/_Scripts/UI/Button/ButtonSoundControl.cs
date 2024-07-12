using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundControl : ButtonStandart {
    [SerializeField] private Image imageForChange;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Sprite soundOn;

    private bool _isSoundOn = true;

    public override void Initialize() {
        base.Initialize();

        SetInitialValue();

        AddMethodInEventClick(TurnSound);
        AddEventOnButton();
    }

    private void SetInitialValue() {
        _isSoundOn = true;
        imageForChange.sprite = soundOn;
    }

    private void TurnSound() {
        SetSoundInGameOpposite();
        SetSoundValueOpposite();
    }

    private void SetSoundInGameOpposite() {
        if (_isSoundOn) {
            TurnOff();

        } else {
            TurnOn();
        }
    }

    private void SetSoundValueOpposite() {
        _isSoundOn = !_isSoundOn;
    }

    private void TurnOff() {
        GameSoundController.TurnOffSound();
        imageForChange.sprite = soundOff;
    }

    private void TurnOn() {
        GameSoundController.TurnOnSound();
        imageForChange.sprite = soundOn;
    }
}