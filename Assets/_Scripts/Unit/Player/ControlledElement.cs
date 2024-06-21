using UnityEngine;
using Zenject;

public class ControlledElement : MonoBehaviour {
    private const int SPEED_MULTIPLIER = 2;

    public Vector3 DirectionMovement => _inputGame.GetDirectionMovememt();

    public ParametrsSnake _parametrsSnake;
    private InputGame _inputGame;
    private Rotation _rotation;
    private Movement _movement;

    public void Initialize(ParametrsSnake parametrsSnake) {
        _parametrsSnake = parametrsSnake;
        Subscribe();
        CreateComponents();
    }

    [Inject]
    public void Construct(InputGame inputGame) {
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
        _parametrsSnake.SetNewSpeed(_parametrsSnake.Speed * SPEED_MULTIPLIER);
    }

    private void OnSpeededDown() {
        _parametrsSnake.SetNewSpeed(_parametrsSnake.Speed / SPEED_MULTIPLIER);
    }
}