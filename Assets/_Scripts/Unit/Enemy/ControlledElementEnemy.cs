using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class ControlledElementEnemy : MonoBehaviour {
    private ParametrsSnake _parametrsSnake;
    private CollisionHandler _collisionHandler;
    
    private Section _target;
    private bool _initialized = false;
    private float _time = 1f;
    private float _currentTime = 0f;
   
    private NavMeshAgent _navMeshAgent;
    private NavigatorEnemy _navigatorEnemy;


    [Inject]
    private void Construct(NavigatorEnemy navigatorEnemy) {
        _navigatorEnemy = navigatorEnemy;
    }

    public void Initialize(ParametrsSnake parametrsSnake, CollisionHandler collisionHandler) {
        if (!_initialized) {
            _parametrsSnake = parametrsSnake;
            _collisionHandler = collisionHandler;
            _initialized = true;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = parametrsSnake.Speed;
            SetTarget();
            Subscribe();
        }
    }

    public void SetTarget() {
        _target = _navigatorEnemy.FindTarget(_parametrsSnake.Head);
        _currentTime = 0;
    }

    private void Update() {
        if (_currentTime > _time) {
            SetTarget();
        } else {
            _currentTime += Time.deltaTime;
        }
        if (_target != null) {
            _navMeshAgent.SetDestination(_target.transform.position);
        }
        else {
            SetTarget();
        }
    }

    private void Subscribe() {
        _collisionHandler.TouchedSection += OnTouchedSection;
    }

    private void Unsubscribe() {
        _collisionHandler.TouchedSection -= OnTouchedSection;
    }

    private void OnTouchedSection(Section section) {
        if (section == _target) {
            SetTarget();
        }
    }
}