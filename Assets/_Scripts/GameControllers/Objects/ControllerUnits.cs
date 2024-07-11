using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using Random = UnityEngine.Random;

public class ControllerUnits : ControllerObject<Unit> {
    public override IReadOnlyList<Unit> Objects => _units;

    protected override bool CanSpawn => _units.Count < GameplayConfig.NumberSpawnedEnemy;
    protected override int CountSpawn => GameplayConfig.NumberSpawnedEnemy - _units.Count;

    private Player _player;
    private List<Unit> _units;
    private string[] _nicknames;
    
    public override void Initialize() {
        _units = new List<Unit>();
        _nicknames = GameplayConfig.Nicknames.Split(',');
        base.Initialize();
    }

    [Inject]
    private void Construct(UnitPool memoryPool, Player player) {
        MemoryPool = memoryPool;
        _player = player;
    }

    protected override void InitializeComponent(Unit gameObject) {
        base.InitializeComponent(gameObject);
        int level = Random.Range(Math.Max(0, _player.Level - 10), _player.Level);

        gameObject.Initialize(_nicknames[Random.Range(0, _nicknames.Length-1)]);
        gameObject.Head.SetLevel(level);
    }

    protected override void RemoveFromCollection(Unit removedObject) {
        _units.Remove(removedObject);
    }

    protected override void AddInCollection(Unit addedObject) {
        _units.Add(addedObject);
    }
}