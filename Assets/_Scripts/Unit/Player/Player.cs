using System;

public class Player : Unit {
    public event Action KilledEnemy;
    public event Action DiedPlayer;
    public event Action PutOnSection;

    private ControlledElement _controlledElement;

    public override void Initialize(string nickname) {
        base.Initialize(nickname);
        Subscribe();
    }

    protected override void GetComponents() {
        base.GetComponents();
        _controlledElement = GetComponent<ControlledElement>();
    }

    protected override void InitializeComponents() {
        base.InitializeComponents();
        _controlledElement.Initialize(ParametrsSnake);
    }

    protected override void OnDiedMe() {
        base.OnDiedMe();
        gameObject.SetActive(false);
        DiedPlayer?.Invoke();
    }

    protected void OnDiedEnemy() {
        KilledEnemy?.Invoke();
    }

    private void OnAddedSection() {
        PutOnSection?.Invoke();
    }

    private void Subscribe() {
        CollisionHandler.DiedEnemy += OnDiedEnemy;
        CollisionHandler.DiedMe += OnDiedMe;
        ControllerStorageSection.AddedSection += OnAddedSection;
    }

    private void Unsubscribe() {
        CollisionHandler.DiedEnemy -= OnDiedEnemy;
        CollisionHandler.DiedMe -= OnDiedMe;
        ControllerStorageSection.AddedSection -= OnAddedSection;
    }

    protected override void OnDestroy() {
        Unsubscribe();
    }
}