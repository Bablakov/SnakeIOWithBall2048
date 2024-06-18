using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class Player : MonoBehaviour {
    [SerializeField] public List<Section> bodyElements;
    [SerializeField] private Rigidbody rb;

    private ControlledElement _controlledElement;
    private FollowingElements _followingElements;  
    private ParametsSnake _parametrsSnake;

    public void Initialize() {
        GetComponents();
        InitializeComponents();
    }

    [Inject]
    public void Construct(SnakeConfig snake, InputGame inputGame) {
        _parametrsSnake = new ParametsSnake(snake, inputGame);
    }

    private void GetComponents() {
        _controlledElement = GetComponentInChildren<ControlledElement>();
        _followingElements = GetComponentInChildren<FollowingElements>();
    }

    private void InitializeComponents() {
        _controlledElement.Initialize(_parametrsSnake);
        _followingElements.Initialize(_parametrsSnake, this);
    }
}