using System;
using Zenject;

public class ConflictController : IDisposable {
    public event Action<string, string> CompletedMurder;

    private SignalBus _signalBus;
    private SectionPool _sectionPool;

    [Inject]
    public ConflictController(SignalBus signalBus, SectionPool sectionPool) {
        _signalBus = signalBus;
        _sectionPool = sectionPool;
        Subscribe();
    }

    private void Subscribe() {
        _signalBus.Subscribe<ConflictedSignal>(OnConflicted);
    }

    private void Unsubscribe() {
        _signalBus.Unsubscribe<ConflictedSignal>(OnConflicted);
    }

    private void OnConflicted(ConflictedSignal signal) {
        CollisionHandler recorder = signal.RecorderConflict;
        CollisionHandler conflict = signal.ConflictedUnit;

        DetermineWinner(recorder, conflict);
    }

    private void DetermineWinner(CollisionHandler recorder, CollisionHandler conflict) {
        if (recorder.Level > conflict.Level) {
            ProcessCollision(recorder, conflict);
        } 
        
        else if (recorder.Level < conflict.Level) {
            ProcessCollision(conflict, recorder);
        }
    }

    private void ProcessCollision(CollisionHandler winner, CollisionHandler loser) {
        var section = SpawnSection(winner, loser);
        GiveSection(winner, section);
        Disable(loser);
        CompletedMurder?.Invoke(winner.Nickname, loser.Nickname);
    }

    private void Disable(CollisionHandler unit) {
        unit.SetOff();
    }

    private Section SpawnSection(CollisionHandler winner, CollisionHandler loser) {
        var section = _sectionPool.Spawn();
        section.transform.position = winner.transform.position;
        section.SetLevel(loser.Level);

        return section;
    }

    private void GiveSection(CollisionHandler winner, Section section) {
        winner.AddSection(section);
    }

    public void Dispose() {
        Unsubscribe();
    }
}