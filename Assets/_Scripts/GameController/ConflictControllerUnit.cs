using Zenject;

public class ConflictControllerUnit {
    private SignalBus _signalBus;
    private SectionPool _sectionPool;

    public ConflictControllerUnit(SignalBus signalBus, SectionPool sectionPool) {
        _signalBus = signalBus;
        _sectionPool = sectionPool;
        Subscribe();
    }

    private void Subscribe() {
        _signalBus.Subscribe<ConflictedUnitsSignal>(OnCollisionedUnits);
    }

    private void Unsubscribe() {
        _signalBus.Unsubscribe<ConflictedUnitsSignal>(OnCollisionedUnits);
    }

    private void OnCollisionedUnits(ConflictedUnitsSignal signal) {
        Unit recorder = signal.RecorderConflict;
        Unit conflict = signal.ConflictedUnit;

        if (IsActiveUnits(recorder, conflict)) {
            DetermineWinner(recorder, conflict);
        }
    }

    private static bool IsActiveUnits(Unit first, Unit second) {
        return first.gameObject.activeSelf && second.gameObject.activeSelf;
    }

    private void DetermineWinner(Unit recorder, Unit conflict) {
        if (recorder.Level > conflict.Level) {
            ProcessUnits(recorder, conflict);
        } 
        
        else if (recorder.Level < conflict.Level) {
            ProcessUnits(conflict, recorder);
        }
    }

    private void ProcessUnits(Unit winner, Unit loser) {
        var section = SpawnSection(winner, loser);
        GiveSection(winner, section);
        DisableUnit(loser);
    }

    private void DisableUnit(Unit unit) {
        unit.SetOff();
        if (unit is Enemy enemy) {
            _signalBus.Fire(new ReleasedObjectSignal<Unit>(enemy));
        }
        else if (unit is Player player) {
            player.gameObject.SetActive(false);
        }
    }

    private Section SpawnSection(Unit winner, Unit loser) {
        var section = _sectionPool.Spawn();
        section.transform.position = winner.Position;
        section.SetLevel(loser.Level);

        return section;
    }

    private void GiveSection(Unit winner, Section section) {
        winner.AddSeciton(section);
    }
}