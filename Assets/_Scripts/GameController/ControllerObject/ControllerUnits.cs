﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ControllerUnits : ControllerObject<Unit> {
    public override List<Section> Objects => _units.Select(unit => unit.Head).ToList();

    protected override bool CanSpawn => _units.Count < GameplayConfig.NumberSpawnedEnemy;
    protected override int CountSpawn => GameplayConfig.NumberSpawnedEnemy - _units.Count;

    private List<Unit> _units;
    private string[] _nicknames;
    
    public override void Initialize() {
        _units = new List<Unit>();
        _nicknames = GameplayConfig.Nicknames.Split(',');
        base.Initialize();
    }

    [Inject]
    private void Construct(UnitPool memoryPool) {
        MemoryPool = memoryPool;
    }

    protected override void InitializeComponent(Unit gameObject) {
        base.InitializeComponent(gameObject);
        gameObject.Initialize(_nicknames[Random.Range(0, _nicknames.Length-1)]);
    }

    protected override void RemoveFromCollection(Unit removedObject) {
        _units.Remove(removedObject);
    }

    protected override void AddInCollection(Unit addedObject) {
        _units.Add(addedObject);
    }
}