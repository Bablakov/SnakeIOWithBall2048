using Zenject;
using UnityEngine;
using System.Collections.Generic;

public class ControllerFreeEnemyOnScene : MonoBehaviour {
    [SerializeField] private float minX = -49f;
    [SerializeField] private float maxX = 49f;
    [SerializeField] private float minY = -49f;
    [SerializeField] private float maxY = 49f;
    [SerializeField] private int countFreeEnemy = 30;
    [SerializeField] private Transform player;

    private bool CanSpawn => _freeEnemy.Count < countFreeEnemy;
    private int CountSpawn => countFreeEnemy - _freeEnemy.Count;

    private List<Enemy> _freeEnemy;
    private EnemyPool _enemyPool;
    private SignalBus _signalBus;

    public void Initialize() {
        _freeEnemy = new List<Enemy>();
        SpawnEnemies();
        Subscribe();
    }

    [Inject]
    private void Construct(EnemyPool enemyPool, SignalBus signaleBus) {
        _enemyPool = enemyPool;
        _signalBus = signaleBus;
    }

    private void Subscribe() {
        _signalBus.Subscribe<KilledEnemySignal>(OnKilledEnemy);
    }

    private void Unsubscribe() {
        _signalBus.Unsubscribe<KilledEnemySignal>(OnKilledEnemy);
    }

    private void OnKilledEnemy(KilledEnemySignal signal) {
        _enemyPool.Despawn(signal.EnemyKilled);
        RemoveFromCollection(signal.EnemyKilled);
        SpawnEnemies();
    }

    private void RemoveFromCollection(Enemy enemy) {
        _freeEnemy.Remove(enemy);
    }

    private void SpawnEnemies() {
        if (CanSpawn) {
            SpawnMissingEnemies();
        }
    }

    private void SpawnMissingEnemies() {
        var countNeedSpawn = CountSpawn;
        for (int i = 0; i < countNeedSpawn; i++) {
            SpawnOnRandomPlaceAndAddInCollection();
        }
    }

    private void SpawnOnRandomPlaceAndAddInCollection() {
        var gameObject = _enemyPool.Spawn();
        gameObject.transform.position = GetRandomPosition();
        gameObject.transform.SetParent(transform);
        gameObject.Initialize(player);
        _freeEnemy.Add(gameObject);
    }

    private Vector3 GetRandomPosition() {
        var randomX = Random.Range(minX, maxX);
        var randomZ = Random.Range(minY, maxY);
        return new Vector3(randomX, 0, randomZ);
    }
}