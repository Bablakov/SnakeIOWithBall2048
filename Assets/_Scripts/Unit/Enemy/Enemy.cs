using UnityEngine;

public class Enemy : Unit {
    [SerializeField] private float timeUpdateTarget;

    private float _currentTime;
    private Transform _target;
    private ControlledElementEnemy _controlledElmentEnemy;

    protected override void InitializeComponents() {
        base.InitializeComponents();
        _controlledElmentEnemy.Initialize(ParametrsSnake, ControllerSection);
    }

    protected override void GetComponents() {
        base.GetComponents();
        _controlledElmentEnemy = GetComponentInChildren<ControlledElementEnemy>();
    }
}