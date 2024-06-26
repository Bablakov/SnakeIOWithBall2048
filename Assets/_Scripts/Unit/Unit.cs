using UnityEngine;
using Zenject;

public abstract class Unit : MonoBehaviour {
    public Section Head { get; protected set; }
    public string Nickname { get; private set; }
    public int Level => Head.Level;

    protected CollisionHandler CollisionHandler;
    protected FollowingElements FollowingElements;
    protected ControllerStorageSection ControllerStorageSection;
    protected StorageSection StorageSection;
    protected ParametrsSnake ParametrsSnake;
    protected NickUnit NickUnit;

    private SignalBus _signalBus;
    private SnakeConfig _snakeConfig;

    public virtual void Initialize(string nickname) {
        SetNewNickName(nickname);
        CreateComponents();
        InitializeComponents();
    }

    [Inject]
    private void Construct(SnakeConfig snakeConfig, SignalBus signalBus) {
        GetComponents();
        _snakeConfig = snakeConfig;
        _signalBus = signalBus;
    }

    protected virtual void Update() {
        FollowingElements.Update();
    }

    protected virtual void OnDiedMe() {
        StorageSection.FreeCollection();
    }

    protected virtual void GetComponents() {
        CollisionHandler = GetComponentInChildren<CollisionHandler>();
        NickUnit = GetComponentInChildren<NickUnit>();
        Head = GetComponentInChildren<Section>();
    }

    protected virtual void InitializeComponents() {
        CollisionHandler.Initialize(Nickname);
        ControllerStorageSection = new(_signalBus, Head, StorageSection, CollisionHandler);
    }

    protected void SetNewNickName(string nickName) {
        NickUnit.Initialize(nickName);
        Nickname = nickName;
    }

    private void CreateComponents() {
        StorageSection = new(Head);
        ParametrsSnake = new ParametrsSnake(_snakeConfig, Head);
        FollowingElements = new FollowingElements(ParametrsSnake, StorageSection);
    }
}