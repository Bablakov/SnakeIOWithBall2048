using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class ControlledElement : MonoBehaviour {
    private ParametsSnake _parametrsPlayer;
    private SignalBus _signalBus;
    private Rotation _rotation;
    private Movement _movement;

    [Inject]
    public void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    public void Initialize(ParametsSnake parametrsPlayer) {
        _parametrsPlayer = parametrsPlayer;
        CreateComponents();
    }

    private void Update() {
        Rotate();
        Move();
    }

    private void CreateComponents() {
        var seciton = GetComponentInChildren<Section>();
        _movement = new Movement(transform, seciton);
        _rotation = new Rotation(transform);
    }

    private void Rotate() {
        _rotation.Rotate(_parametrsPlayer.DirectionMovement);
    }

    private void Move() {
        _movement.Move(_parametrsPlayer.Speed);
    }

    private void OnTriggerEnter(Collider other) {
        if (TryGetComponentSection(other, out Section section)) {
            _signalBus.Fire(new AddedSectionSignal(section));
        }
    }

    private static bool TryGetComponentSection(Collider other, out Section section) {
        section = null;
        return TryGetParent(other) && TryGetComponentSectionOnParent(other, out section);
    }

    private static bool TryGetComponentSectionOnParent(Collider other, out Section section) {
        return other.transform.parent.TryGetComponent(out section);
    }

    private static bool TryGetParent(Collider other) {
        return other.transform.parent != null;
    }
}