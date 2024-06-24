using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class ControllerObject<TypeObject> : MonoBehaviour where TypeObject : MonoBehaviour {
    public abstract IReadOnlyList<TypeObject> Objects { get; }

    protected abstract bool CanSpawn { get; }
    protected abstract int CountSpawn { get; }

    protected MemoryPool<TypeObject> MemoryPool;
    protected GameplayConfig GameplayConfig;
    protected SignalBus SignalBus;

    public virtual void Initialize() {
        SpawnObject();
        Subscribe();
    }

    [Inject]
    private void Construct(SignalBus signaleBus, GameplayConfig gameplayConfig) {
        GameplayConfig = gameplayConfig;
        SignalBus = signaleBus;
    }

    protected virtual void Subscribe() {
        SignalBus.Subscribe<ReleasedObjectSignal<TypeObject>>(OnReleasedObject);
    }

    protected virtual void Unsubscribe() {
        SignalBus.Subscribe<ReleasedObjectSignal<TypeObject>>(OnReleasedObject);
    }

    protected virtual void OnReleasedObject(ReleasedObjectSignal<TypeObject> signal) {
        RemoveFromCollection(signal.ReleasedObject);
        MemoryPool.Despawn(signal.ReleasedObject);
        SpawnObject();
    }

    protected void SpawnObject() {
        if (CanSpawn) {
            SpawnMissingObject();
        }
    }

    protected abstract void RemoveFromCollection(TypeObject removedObject);

    protected abstract void AddInCollection(TypeObject addedObject);

    protected virtual void SpawnOnRandomPlaceAndAddInCollection() {
        var gameObject = MemoryPool.Spawn();
        SetPosition(gameObject);
        SetParent(gameObject);
        AddInCollection(gameObject);
        InitializeComponent(gameObject);
    }

    protected virtual void InitializeComponent(TypeObject gameObject) { }

    protected void SetPosition(TypeObject gameObject) {
        gameObject.transform.position = GetRandomPosition();
    }

    private void SpawnMissingObject() {
        var countNeedSpawn = CountSpawn;
        for (int i = 0; i < countNeedSpawn; i++) {
            SpawnOnRandomPlaceAndAddInCollection();
        }
    }

    private void SetParent(TypeObject gameObject) {
        gameObject.transform.SetParent(transform);
    }

    private Vector3 GetRandomPosition() {
        var randomX = Random.Range(GameplayConfig.MinimalPositionSpawnObject, GameplayConfig.MaximalPositionSpawnObject);
        var randomZ = Random.Range(GameplayConfig.MinimalPositionSpawnObject, GameplayConfig.MaximalPositionSpawnObject);
        return new Vector3(randomX, 0, randomZ);
    }
}