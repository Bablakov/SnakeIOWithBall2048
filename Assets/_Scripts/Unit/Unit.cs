using UnityEngine;
using Zenject;

public abstract class Unit : MonoBehaviour {
    public Section Head { get; protected set; }
    public string Nickname { get; private set; }
    public int Level => Head.Level;

    protected ControllerSpeedUpSnake ControllerSpeedUp;
    protected ControllerStorageSection ControllerStorageSection;
    protected FollowingElements FollowingElements;
    protected AnimationSpeedUp AnimationSpeedUp;
    protected CollisionHandler CollisionHandler;
    protected StorageSection StorageSection;
    protected ParametrsSnake ParametrsSnake;
    protected NickUnit NickUnit;

    private SignalBus _signalBus;
    private SnakeConfig _snakeConfig;
    private float _timeInvulnerability;

    public virtual void Initialize(string nickname) {
        GetComponents();
        SetNewNickName(nickname);
        CreateComponents();
        InitializeComponents();
    }

    [Inject]
    private void Construct(SnakeConfig snakeConfig, SignalBus signalBus) {
        _timeInvulnerability = snakeConfig.TimeInvulnerability;
        _snakeConfig = snakeConfig;
        _signalBus = signalBus;
    }

    protected virtual void Update() {
        FollowingElements.Update();
        ControllerSpeedUp.Update();
        StorageSection.Update();
    }

    protected virtual void OnDiedMe() {
        StorageSection.FreeCollection();
    }

    protected virtual void GetComponents() {
        AnimationSpeedUp = GetComponentInChildren<AnimationSpeedUp>();
        CollisionHandler = GetComponent<CollisionHandler>();
        NickUnit = GetComponentInChildren<NickUnit>();
        Head = GetComponentInChildren<Section>();
    }

    protected virtual void InitializeComponents() {
        CollisionHandler.Initialize(Nickname);
        ControllerStorageSection = new(_signalBus, Head, StorageSection, CollisionHandler);
        AnimationSpeedUp.TurnOff();
    }

    protected void SetNewNickName(string nickName) {
        NickUnit.Initialize(nickName);
        Nickname = nickName;
    }

    private void CreateComponents() {
        StorageSection = new(Head);
        ParametrsSnake = new ParametrsSnake(_snakeConfig, Head);
        FollowingElements = new FollowingElements(ParametrsSnake, StorageSection);
        ControllerSpeedUp = new ControllerSpeedUpSnake(ParametrsSnake, AnimationSpeedUp);
    }

    private void OnEnable() {
        TurnOnInvulnerable();
    }

    private void TurnOnInvulnerable() {
        if (StorageSection != null) {
            StorageSection.MakeSectionsInvulnerable();
            _signalBus.Fire(new CalledDelayedMethodSignal(_timeInvulnerability, StorageSection.MakeSectionsVulnerable));
        }
    }

    protected virtual void OnDestroy() {
        ControllerStorageSection.Dispose();
    }
}