using System;
using UnityEngine;

public class ControllerSpeedUpSnake {
    public float PercentageFreeSpeedUp => _currentTimeFreeSpeedUp / _parametrsSnake.MaxTimeSpeedUp;
    public event Action SpeededUp;
    public event Action SlowedDown;

    private AnimationSpeedUp _animationSpeedUp;
    private float _currentTimeFreeSpeedUp = 0f;
    private ParametrsSnake _parametrsSnake;
    private bool _isSpeedUp = false;

    public ControllerSpeedUpSnake(ParametrsSnake parametrsSnake, AnimationSpeedUp animationSpeedUp) {
        _animationSpeedUp = animationSpeedUp;
        _parametrsSnake = parametrsSnake;
    }

    public void Update() {
        if (_isSpeedUp) {
            if (_currentTimeFreeSpeedUp > 0) {
                _currentTimeFreeSpeedUp -= Time.deltaTime;
            } 
            else {
                SpeedLow();
            }
        } 
        else if (_currentTimeFreeSpeedUp < _parametrsSnake.MaxTimeSpeedUp) {
                _currentTimeFreeSpeedUp += Time.deltaTime;
        }
    }

    public void SpeedUp() {
        SpeedUpValue();
        _animationSpeedUp.TurnOn();
        _isSpeedUp = true;
        SpeededUp?.Invoke();
    }

    public void SpeedLow() {
        SpeedLowValue();
        _animationSpeedUp.TurnOff();
        _isSpeedUp = false;
        SlowedDown?.Invoke();
    }

    private void SpeedUpValue() {
        _parametrsSnake.SpeedUp();
    }

    private void SpeedLowValue() {
        _parametrsSnake.SpeedLow();
    }
}