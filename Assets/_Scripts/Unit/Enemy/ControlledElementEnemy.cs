using System.Linq;
using UnityEngine;
using Zenject;

public class ControlledElementEnemy : MonoBehaviour {
    private const int SPEED_MULTIPLIER = 2;

    private ParametrsSnake _parametrsSnake;
    private ControllerSection _controllerSectioin;
    private Section _target;
    private Movement _movement;
    private Rotation _rotation;
    private bool _initialized = false;
    private NavigatorEnemy _navigatorEnemy;
    private float _time = 3f;
    private float _currentTime = 0f;


    [Inject]
    private void Construct(NavigatorEnemy navigatorEnemy) {
        _navigatorEnemy = navigatorEnemy;
    }

    public void Initialize(ParametrsSnake parametrsSnake, ControllerSection controllerSection) {
        if (!_initialized) {
            _parametrsSnake = parametrsSnake;
            _controllerSectioin = controllerSection;
            _initialized = true;
            _movement = new Movement(transform, parametrsSnake.Head);
            _rotation = new Rotation(transform);
            Subscribe();
        }
    }

    public void SetTarget(Section target) { 
        _target = target;
        _currentTime = 0;
    }

    private void Update() {
        if (_currentTime > _time) {
            SetTarget(_navigatorEnemy.FindTarget(_parametrsSnake.Head));
        } else {
            _currentTime += Time.deltaTime;
        }
        Move();
        Rotate();
    }

    private void Subscribe() {
        _controllerSectioin.AddedSection += OnAddedSection;
    }

    private void Unsubscribe() {
        _controllerSectioin.AddedSection -= OnAddedSection;
    }
     
    private void Rotate() {
        if (_target != null) { 
            Vector3 directionMovement = _target.Position - transform.position;
            _rotation.Rotate(directionMovement);
        }
        else {
            _target = _navigatorEnemy.FindTarget(_parametrsSnake.Head);
        }
    }

    private void Move() {
        _movement.Move(_parametrsSnake.Speed);
    }

    private void OnAddedSection(Section section) {
        if (section == _target) {
            _target = _navigatorEnemy.FindTarget(_parametrsSnake.Head);
        }
    }

    private void OnSpededUp() {
        _parametrsSnake.SetNewSpeed(_parametrsSnake.Speed * SPEED_MULTIPLIER);
    }

    private void OnSpeededDown() {
        _parametrsSnake.SetNewSpeed(_parametrsSnake.Speed / SPEED_MULTIPLIER);
    }
}