using UnityEngine;
using System;
using Zenject;

public class CollisionHandler : MonoBehaviour {
    public event Action<Section> AddedSection;

    private SignalBus _signalBus;
    private Unit _mineUnit;

    public void Initialize(SignalBus signalBus, Unit unit) {
        _signalBus = signalBus;
        _mineUnit = unit;
    }

    private void OnTriggerEnter(Collider other) {
        if (TryGetComponentSection(other, out Section section)) {
            if (TryGetComponentUnit(other, out Unit unit)) {
                _signalBus.Fire(new CollisionedUnitsSignal(_mineUnit, unit));
            } 
            else {
                AddedSection?.Invoke(section);
            }
            Debug.Log($"Collision {_mineUnit}");
        }
    }

    private bool TryGetComponentUnit(Collider other, out Unit unit) {
        unit = other.GetComponentInParent<Unit>();
        return unit != null;
    }

    private static bool TryGetComponentSection(Collider other, out Section section) {
        section = null;
        return TryGetParent(other) && TryGetComponentSectionOnParent(other, out section);
    }

    private static bool TryGetComponentSectionOnParent(Collider other, out Section section) {
        return other.transform.parent.TryGetComponent(out section);
    }

    private static bool TryGetParent(Collider other) {
        return other.transform.parent != null;
    }
}