using UnityEngine;
using Zenject;

public class CollisionControllerUnit {
    private SignalBus _signalBus;
    private SectionPool _sectionPool;

    public CollisionControllerUnit(SignalBus signalBus, SectionPool sectionPool) {
        _signalBus = signalBus;
        _sectionPool = sectionPool;
        Subscribe();
    }

    private void Subscribe() {
        _signalBus.Subscribe<CollisionedUnitsSignal>(OnCollisionedUnits);
    }

    private void Unsubscribe() {
        _signalBus.Unsubscribe<CollisionedUnitsSignal>(OnCollisionedUnits);
    }

    private void OnCollisionedUnits(CollisionedUnitsSignal signal) {
        if (signal.RecorderCollision.Level > signal.CollisionedUnit.Level && signal.CollisionedUnit.gameObject.activeSelf) {
            signal.CollisionedUnit.SetOff();
            var section = _sectionPool.Spawn();
            section.transform.position = signal.CollisionedUnit.Position;
            signal.RecorderCollision.AddSeciton(section);
        }

        if (signal.RecorderCollision.Level < signal.CollisionedUnit.Level && signal.RecorderCollision.gameObject.activeSelf) {
            signal.RecorderCollision.SetOff();
            var section = _sectionPool.Spawn();
            section.transform.position = signal.RecorderCollision.Position;
            signal.CollisionedUnit.AddSeciton(section);
        }
    }
}