using UnityEngine;
using Zenject;

public class ControlledElement : MonoBehaviour {
    public Vector3 DirectionMovement => _inputGame.GetDirectionMovememt();

    public ParametrsSnake _parametrsSnake;

    private ControllerSpeedUpSnake _controllerSpeedUpSnake;
    private InputGame _inputGame;
    private Rotation _rotation;
    private Movement _movement;

    public void Initialize(ParametrsSnake parametrsSnake, ControllerSpeedUpSnake controllerSpeedUpSnake) {
        _controllerSpeedUpSnake = controllerSpeedUpSnake;
        _parametrsSnake = parametrsSnake;
        Subscribe();
        CreateComponents();
    }

    [Inject]
    private void Construct(InputGame inputGame) {
        _inputGame = inputGame;
    }

    private void Update() {
        Rotate();
        Move();
    }

    private void CreateComponents() {
        _movement = new Movement(transform, _parametrsSnake.Head);
        _rotation = new Rotation(transform);
    }

    private void Subscribe() {
        _inputGame.SpeededUp += OnSpededUp;
        _inputGame.SpeededDown += OnSpeededDown;
    }

    private void Unsubscribe() {
        _inputGame.SpeededUp -= OnSpededUp;
        _inputGame.SpeededDown -= OnSpeededDown;
    }

    private void Rotate() {
        _rotation.Rotate(DirectionMovement);
    }

    private void Move() {
        _movement.Move(_parametrsSnake.Speed);
    }

    private void OnSpededUp() {
        _controllerSpeedUpSnake.SpeedUp();
    }

    private void OnSpeededDown() {
        _controllerSpeedUpSnake.SpeedLow();
    }

    private void OnDestroy() {
        Unsubscribe();
    }
}