using UnityEngine;
using UnityEngine.AI;

public class Enemy : Unit {
    private Transform _player;
    private NavMeshAgent _navMeshAgent;
    private ControlledElementEnemy _controlledElmentEnemy;

    public void Initialize(Transform player) {
        _player = player;
        base.Initialize();
    }

    protected override void InitializeComponents() {
        base.InitializeComponents();
        _controlledElmentEnemy.Initialize(ParametrsSnake, _player, _navMeshAgent);
    }

    protected override void GetComponents() {
        base.GetComponents();
        _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        _controlledElmentEnemy = GetComponent<ControlledElementEnemy>();
    }
}