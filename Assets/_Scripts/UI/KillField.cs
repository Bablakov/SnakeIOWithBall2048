using UnityEngine;
using Zenject;

public class KillField : MonoBehaviour {
    private SignalBus _signalBus;
    private LineKillFieldPool _memoryPool;

    public void Initialize() {
        Subscribe();
    }

    public void DisableLine(ViewLineKilledField lineKillField) {
        _memoryPool.Despawn(lineKillField);
    }

    [Inject]
    private void Contstruct(SignalBus signalBus, LineKillFieldPool memoryPool) {
        _signalBus = signalBus;
        _memoryPool = memoryPool;
    }

    private void Subscribe() {
        _signalBus.Subscribe<CompletedMurderSignal>(OnCompletedMurder);
    }

    private void Unsubscribe() {
        _signalBus.Unsubscribe<CompletedMurderSignal>(OnCompletedMurder);
    }

    private void OnCompletedMurder(CompletedMurderSignal signal) {
        var line = _memoryPool.Spawn();
        line.transform.SetParent(transform);
        line.transform.localScale = Vector3.one;
        line.Initialize(signal.NameKiller, signal.NameKilled, this);
    }
}