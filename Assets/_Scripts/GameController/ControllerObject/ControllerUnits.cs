using System.Collections.Generic;
using System.Linq;
using Zenject;

public class ControllerUnits : ControllerObject<Unit> {
    public override List<Section> Objects => _units.Select(unit => unit.Head).ToList();

    protected override bool CanSpawn => _units.Count < GameplayConfig.NumberSpawnedEnemy;
    protected override int CountSpawn => GameplayConfig.NumberSpawnedEnemy - _units.Count;

    private List<Unit> _units;

    public override void Initialize() {
        _units = new List<Unit>();
        base.Initialize();
    }

    [Inject]
    private void Construct(UnitPool memoryPool) {
        MemoryPool = memoryPool;
    }

    protected override void InitializeComponent(Unit gameObject) {
        base.InitializeComponent(gameObject);
        gameObject.Initialize();
    }

    protected override void RemoveFromCollection(Unit removedObject) {
        _units.Remove(removedObject);
    }

    protected override void AddInCollection(Unit addedObject) {
        _units.Add(addedObject);
    }
}