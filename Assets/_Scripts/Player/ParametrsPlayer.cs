using System;
using UnityEngine;
using Zenject;

[Serializable]
public class ParametrsPlayer {
    [field: SerializeField, Range(0.1f, 100f)] public float Speed { get; private set; }
    [field: SerializeField, Range(0.1f, 2f)] public float DistanceBeetwenSection { get; private set; }

    public Vector3 DirectionMovement => _inputGame.GetDirectionMovememt();

    private const int SPEED_MULTIPLIER = 2;
    private InputGame _inputGame;

    [Inject]
    public void Construct(InputGame inputGame) {
        _inputGame = inputGame;
        _inputGame.SpeededUp += OnSpededUp;
        _inputGame.SpeededDown += OnSpeededDown;
    }

    private void OnSpededUp() {
        Speed *= SPEED_MULTIPLIER;
    }

    private void OnSpeededDown() {
        Speed /= SPEED_MULTIPLIER;
    }
}