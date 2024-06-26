using System;
using Zenject;
using UnityEngine;

public class InputDesktop : InputGame {
    public event Action SpeededUp;
    public event Action SpeededDown;

    private const int HEIGHT_POINT = 0;

    private int ScreenWidth => Screen.width;
    private int ScreenHeight => Screen.height;
    
    private Vector3 DirectionMovement;
    private GameInputAction _gameInput;
    private Vector2 _mousePosition;

    [Inject]
    public InputDesktop() {
        _gameInput = new GameInputAction();
        _gameInput.Player.Enable();
        _gameInput.Player.BoostSpeed.started += SpeedUp;
        _gameInput.Player.BoostSpeed.canceled += SpeedDown;
    }

    public Vector3 GetDirectionMovememt() {
        GetDataInput();
        ProcessData();
        return DirectionMovement;
    }

    private void SpeedUp(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        SpeededUp?.Invoke();
    }

    private void SpeedDown(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        SpeededDown?.Invoke();
    }

    private void GetDataInput() {
        _mousePosition = _gameInput.Player.PositionMouse.ReadValue<Vector2>();
    }

    private void ProcessData() {
        DirectionMovement = new Vector3(CalculatePositionByX(), HEIGHT_POINT, CalculatePositionByY());
    }

    private float CalculatePositionByX() {
        return _mousePosition.x - ScreenWidth / 2;
    }

    private float CalculatePositionByY() {
        return _mousePosition.y - ScreenHeight / 2;
    }
}