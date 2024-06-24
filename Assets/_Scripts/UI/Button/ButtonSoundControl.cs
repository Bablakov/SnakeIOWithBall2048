using UnityEngine;

public class ButtonSoundControl : StandartButton {
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;

    private bool _isSoundOn = true;

    public override void Initialize() {
        base.Initialize();

        SetInitialValue();

        AddMethodInEventClick(TurnSound);
        AddEventOnButton();
    }

    private void SetInitialValue() {
        _isSoundOn = true;
        Image.sprite = _soundOn;
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
        Image.sprite = _soundOff;
    }

    private void TurnOn() {
        GameSoundController.TurnOnSound();
        Image.sprite = _soundOn;
    }
}