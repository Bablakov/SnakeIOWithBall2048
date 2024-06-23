using UnityEngine;
using Zenject;

public abstract class Unit : MonoBehaviour {
    [SerializeField] public Section Head;
    protected CollisionHandler CollisionHandler;
    protected FollowingElements FollowingElements;
    protected ControllerSection ControllerSection;
    protected ParametrsSnake ParametrsSnake;
    protected NickUnit NickUnit;

    public Transform transformParent => Head.transform;
    public string Nickname { get; private set; }
    public int Level => Head.Level;
    public Vector3 Position => Head.Position;
    public Unit ConflictUnit;
    public bool IsConflict;


    public void SetOff() {
        ControllerSection.FreeCollection();
    }

    public virtual void AddSeciton(Section section) {
        ControllerSection.AddElement(section);
    }

    public void Initialize(string nickname) {
        SetNewNickName(nickname);
        InitializeComponents();
    }

    [Inject]
    public void Construct(SnakeConfig snake, SignalBus signalBus) {
        GetComponents();
        ParametrsSnake = new ParametrsSnake(snake, Head);
        ControllerSection = new (CollisionHandler, signalBus, Head, this);
        CollisionHandler.Initialize(signalBus, this);
    }

    protected virtual void Update() {
        FollowingElements.Update();
    }

    protected void SetNewNickName(string nickName) {
        NickUnit.Initialize(nickName);
    }

    protected virtual void GetComponents() {
        CollisionHandler = GetComponentInChildren<CollisionHandler>();
        NickUnit = GetComponentInChildren<NickUnit>();
    }

    protected virtual void InitializeComponents() {
        FollowingElements = new FollowingElements(ParametrsSnake, ControllerSection);
    }
}