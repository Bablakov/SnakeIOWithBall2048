using UnityEngine;
using UnityEngine.AI;

public class ControllerSpeedUpEnemy {
    private ParametrsSnake _parametrsSnake;
    private NavMeshAgent _navMeshAgent;

    public ControllerSpeedUpEnemy(ParametrsSnake parametrsSnake, NavMeshAgent navMeshAgent) {
        _parametrsSnake = parametrsSnake;
        _navMeshAgent = navMeshAgent;
        Subscribe();
    }

    private void Subscribe() {
        _parametrsSnake.ChangedSpeed += OnChangedSpeed;
    }

    private void Unsubscribe() {
        _parametrsSnake.ChangedSpeed -= OnChangedSpeed;
    }

    private void OnChangedSpeed() {
        _navMeshAgent.speed = _parametrsSnake.Speed;
    }
}