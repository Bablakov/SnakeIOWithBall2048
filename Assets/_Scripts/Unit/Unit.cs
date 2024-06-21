using UnityEngine;
using Zenject;

public class Unit : MonoBehaviour {
    [SerializeField] protected Section head;
    protected CollisionHandler CollisionHandler;
    protected FollowingElements FollowingElements;
    protected ControllerSection ControllerSection;
    protected ParametrsSnake ParametrsSnake;

    public Transform transformParent => head.transform;
    public int Level => head.Level;
    public Vector3 Position => head.Position;

    public void SetOff() {
        Debug.Log($"Unit SetOff- {gameObject}");
        ControllerSection.FreeCollection();
        gameObject.SetActive(false);
    }

    public void AddSeciton(Section section) {
        ControllerSection.AddElement(section);
    }

    public virtual void Initialize() {
        InitializeComponents();
    }

    [Inject]
    public void Construct(SnakeConfig snake, SignalBus signalBus) {
        GetComponents();
        ParametrsSnake = new ParametrsSnake(snake, head);
        ControllerSection = new (CollisionHandler, signalBus, head);
        CollisionHandler.Initialize(signalBus, this);
    }

    protected virtual void Update() {
        FollowingElements.Update();
    }

    protected virtual void GetComponents() {
        CollisionHandler = GetComponentInChildren<CollisionHandler>();
    }

    protected virtual void InitializeComponents() {
        FollowingElements = new FollowingElements(ParametrsSnake, ControllerSection);
    }
}