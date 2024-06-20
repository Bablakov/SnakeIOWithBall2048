using UnityEngine;
using Zenject;


public class Player : MonoBehaviour {
    [SerializeField] private Section head;
    private ControlledElement _controlledElement;
    private CollisionHandler _collisionHandler;
    private FollowingElements _followingElements;
    private ControllerSection _controllerSection;
    private ParametrsSnake _parametrsSnake;

    public void Initialize() {
        InitializeComponents();
    }

    [Inject]
    public void Construct(SnakeConfig snake, SignalBus signalBus) {
        GetComponents();
        _parametrsSnake = new ParametrsSnake(snake, head);
        _controllerSection = new(_collisionHandler, signalBus, head);
    }

    private void Update() {
        _followingElements.Update();
    }

    private void GetComponents() {
        _controlledElement = GetComponentInChildren<ControlledElement>();
        _collisionHandler = GetComponentInChildren<CollisionHandler>();
    }

    private void InitializeComponents() {
        _controlledElement.Initialize(_parametrsSnake);
        _followingElements = new FollowingElements(_parametrsSnake, _controllerSection);
    }
}