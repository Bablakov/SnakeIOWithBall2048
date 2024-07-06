using Zenject;
using UnityEngine;

public class Enemy : Unit {
    private SignalBus _signalBus;
    private ControlledElementEnemy _controlledElmentEnemy;
    private bool _initialized = false;

    private float _currentTimeUpdateSpeedUp;
    private float _timeUpdateSpeedUp = 10f;

    public override void Initialize(string nickname) {
        if (!_initialized) {
            base.Initialize(nickname);
            Subscribe();
            _initialized = true;
        }
        else {
            SetNewNickName(nickname);
        }
        _currentTimeUpdateSpeedUp = Random.Range(0f, 20f);
        _timeUpdateSpeedUp = Random.Range(5f, 15f);
    }

    [Inject]
    private void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    protected override void Update() {
        base.Update();
        if (_currentTimeUpdateSpeedUp > 0) {
            _currentTimeUpdateSpeedUp -= Time.deltaTime;
        }
        else {
            _currentTimeUpdateSpeedUp = _timeUpdateSpeedUp;
            ControllerSpeedUp.SpeedUp();
        }
    }

    protected override void OnDiedMe() {
        base.OnDiedMe();
        _signalBus.Fire(new ReleasedObjectSignal<Unit>(this));
    }

    protected override void InitializeComponents() {
        base.InitializeComponents();
        _controlledElmentEnemy.Initialize(ParametrsSnake, CollisionHandler, this);
    }

    protected override void GetComponents() {
        base.GetComponents();
        _controlledElmentEnemy = GetComponent<ControlledElementEnemy>();
    }

    private void Subscribe() {
        CollisionHandler.DiedMe += OnDiedMe;
    }

    private void Unsubscribe() {
        CollisionHandler.DiedMe -= OnDiedMe;
    }

    protected override void OnDestroy() {
        Unsubscribe();
    }
}