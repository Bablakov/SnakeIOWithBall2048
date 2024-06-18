using System;
using UnityEngine;
using Zenject;

public class ParametsSnake: IDisposable {
    public float Speed { get; private set; }
    public float DistanceBeetwenSection { get; private set; }

    public Vector3 DirectionMovement => _inputGame.GetDirectionMovememt();

    private const int SPEED_MULTIPLIER = 2;
    private InputGame _inputGame;

    public ParametsSnake(SnakeConfig snakeConfig, InputGame inputGame) {
        _inputGame = inputGame;

        Speed = snakeConfig.Speed;
        DistanceBeetwenSection = snakeConfig.DistanceBeetwen;
        Subscribe();
    }

    private void Subscribe() {
        _inputGame.SpeededUp += OnSpededUp;
        _inputGame.SpeededDown += OnSpeededDown;
    }

    private void Unsubscribe() {
        _inputGame.SpeededUp -= OnSpededUp;
        _inputGame.SpeededDown -= OnSpeededDown;
    }

    private void OnSpededUp() {
        Speed *= SPEED_MULTIPLIER;
    }

    private void OnSpeededDown() {
        Speed /= SPEED_MULTIPLIER;
    }

    public void Dispose() {
        Unsubscribe();
        Debug.Log("Dispose");
    }
}