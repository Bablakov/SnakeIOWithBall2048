using System;
using UnityEngine;
using Zenject;

public class InputMobile : InputGame, ITickable, IDisposable {
    public event Action SpeededUp;
    public event Action SpeededDown;

    private Vector3 _directionMovement;
    private GameInputAction _gameInput;
    private Joystick _floatingJoystick;

    [Inject]
    public InputMobile(Joystick floatingJoystick) {
        _floatingJoystick = floatingJoystick;
        CreateComponent();
        Subscribe();
    }

    public void Tick() {
        GetDataInput();
    }

    private void CreateComponent() {
        _gameInput = new GameInputAction();
        _gameInput.Player.Enable();
    }

    private void Subscribe() {
        _gameInput.Player.BoostSpeedMobile.started += SpeedUp;
        _gameInput.Player.BoostSpeedMobile.canceled += SpeedDown;
    }

    private void Unsubscribe() {
        _gameInput.Player.BoostSpeedMobile.started -= SpeedUp;
        _gameInput.Player.BoostSpeedMobile.canceled -= SpeedDown;
    }

    public Vector3 GetDirectionMovememt() {
        return _directionMovement;
    }

    private void SpeedUp(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        SpeededUp?.Invoke();
    }

    private void SpeedDown(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        SpeededDown?.Invoke();
    }

    private void GetDataInput() {
        if (_floatingJoystick.Direction != Vector2.zero) {
            var newVector = _floatingJoystick.Direction.normalized;
            _directionMovement = new Vector3 (newVector.x, 0, newVector.y);
        }
    }

    public void Dispose() {
        Unsubscribe();
    }
}