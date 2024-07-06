using System;
using UnityEngine;

public class ParametrsSnake {
    public float DistanceBeetwenSection { get; private set; }
    public float MaxTimeSpeedUp { get; private set; }
    public Section Head { get; private set; }
    public float Speed { get; private set; }

    public event Action ChangedSpeed;
    
    private const int SPEED_MULTIPLIER = 2;
    private float _startSpeed;

    public ParametrsSnake(SnakeConfig snakeConfig, Section head) {
        DistanceBeetwenSection = snakeConfig.DistanceBeetwen;
        MaxTimeSpeedUp = snakeConfig.TimeSpeedUp;
        _startSpeed = snakeConfig.Speed;
        Speed = snakeConfig.Speed;
        Head = head;
    }
    public void SpeedUp() {
        Speed = _startSpeed * SPEED_MULTIPLIER;
        InvokeEventChangeSpeed();
    }

    public void SpeedLow() {
        Speed = _startSpeed;
        InvokeEventChangeSpeed();
    }

    private void InvokeEventChangeSpeed() {
        ChangedSpeed?.Invoke();
    }
}