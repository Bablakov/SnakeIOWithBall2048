using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class ControlledElementEnemy : MonoBehaviour {
    private ControllerSpeedUpEnemy _controllerSpeedUpEnemy;
    private CollisionHandler _collisionHandler;
    private ParametrsSnake _parametrsSnake;
    private NavigatorEnemy _navigatorEnemy;
    private NavMeshAgent _navMeshAgent;
    private bool _initialized = false;
    private Transform _target;
    private Unit _unit;

    public void Initialize(ParametrsSnake parametrsSnake, CollisionHandler collisionHandler, Unit unit) {
        if (!_initialized) {
            GetComponent();
            SetValue(parametrsSnake, collisionHandler, unit);
            FindNewTarget();
            Subscribe();
            _controllerSpeedUpEnemy = new ControllerSpeedUpEnemy(parametrsSnake, _navMeshAgent);
        }
    }

    private void Update() {
        if (_target != null)
            _navMeshAgent.SetDestination(_target.position);
        else 
            FindNewTarget();
    }

    [Inject]
    private void Construct(NavigatorEnemy navigatorEnemy) {
        _navigatorEnemy = navigatorEnemy;
    }

    private void SetValue(ParametrsSnake parametrsSnake, CollisionHandler collisionHandler, Unit unit) {
        _navMeshAgent.speed = parametrsSnake.Speed;
        _collisionHandler = collisionHandler;
        _parametrsSnake = parametrsSnake;
        _initialized = true;
        _unit = unit;
    }

    private void Subscribe() {
        _collisionHandler.TouchedSection += OnTouchedSection;
    }

    private void Unsubscribe() {
        _collisionHandler.TouchedSection -= OnTouchedSection;
    }

    private void GetComponent() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnTouchedSection(Section section) {
        FindNewTarget();
    }

    private void FindNewTarget() {
        SetTarget(_navigatorEnemy.FindTarget(_unit));
    }

    private void SetTarget(Transform target) {
        _target = target;
    }

    private void OnDestroy() {
        Unsubscribe();
    }
}