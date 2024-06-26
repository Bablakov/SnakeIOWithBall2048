using Zenject;

public class Enemy : Unit {
    private SignalBus _signalBus;
    private ControlledElementEnemy _controlledElmentEnemy;
    private bool _initialized = false;

    public override void Initialize(string nickname) {
        if (!_initialized) {
            base.Initialize(nickname);
            Subscribe();
            _initialized = true;
        }
        else {
            SetNewNickName(nickname);
        }
    }

    [Inject]
    private void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    protected override void OnDiedMe() {
        base.OnDiedMe();
        _signalBus.Fire(new ReleasedObjectSignal<Unit>(this));
    }

    protected override void InitializeComponents() {
        base.InitializeComponents();
        _controlledElmentEnemy.Initialize(ParametrsSnake, CollisionHandler);
    }

    protected override void GetComponents() {
        base.GetComponents();
        _controlledElmentEnemy = GetComponentInChildren<ControlledElementEnemy>();
    }

    private void Subscribe() {
        CollisionHandler.DiedMe += OnDiedMe;
    }

    private void Unsubscribe() {
        CollisionHandler.DiedMe -= OnDiedMe;
    }
}