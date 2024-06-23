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
                if (_mineUnit.ConflictUnit == unit && _mineUnit.IsConflict) {
                    _mineUnit.ConflictUnit = null;
                    _mineUnit.IsConflict = false;
                }
                else {
                    unit.IsConflict = true;
                    unit.ConflictUnit = _mineUnit;
                    _signalBus.Fire(new ConflictedUnitsSignal(_mineUnit, unit));
                }
            } 
            else {
                AddedSection?.Invoke(section);
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (TryGetComponentSection(other, out Section section)) {
            if (TryGetComponentUnit(other, out Unit unit)) {
                if (_mineUnit.ConflictUnit == unit && _mineUnit.IsConflict) {
                    _mineUnit.ConflictUnit = null;
                    _mineUnit.IsConflict = false;
                } else {
                    unit.IsConflict = true;
                    unit.ConflictUnit = _mineUnit;
                    _signalBus.Fire(new ConflictedUnitsSignal(_mineUnit, unit));
                }
            } else {
                AddedSection?.Invoke(section);
            }
        }
    }

    private bool TryGetComponentUnit(Collider other, out Unit unit) {
        unit = other.GetComponentInParent<Unit>();
        return unit != null;
    }

    private static bool TryGetComponentSection(Collider other, out Section section) {
        section = other.GetComponentInParent<Section>();
        return section != null;
    }
}