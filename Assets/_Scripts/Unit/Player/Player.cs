using System;

public class Player : Unit {
    public event Action<int> ChangedNumberEnemiesKilled;

    private ControlledElement _controlledElement;
    private int _numberEnemiesKilled;

    protected override void GetComponents() {
        base.GetComponents();
        _controlledElement = GetComponentInChildren<ControlledElement>();
    }

    protected override void InitializeComponents() {
        base.InitializeComponents();
        _controlledElement.Initialize(ParametrsSnake);
    }

    public override void AddSeciton(Section section) {
        base.AddSeciton(section);
        SignalBus.Fire(new DiedEnemySignal());
        _numberEnemiesKilled++;
        ChangedNumberEnemiesKilled?.Invoke(_numberEnemiesKilled);
    }
}