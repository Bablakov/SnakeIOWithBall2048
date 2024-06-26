using UnityEngine;
using Zenject;

public class KillField : MonoBehaviour {
    private ConflictController _conflictControllerUnit;
    private LineKillFieldPool _memoryPool;

    public void Initialize() {
        Subscribe();
    }

    public void DisableLine(ViewLineKilledField lineKillField) {
        _memoryPool.Despawn(lineKillField);
    }

    [Inject]
    private void Contstruct(ConflictController conflictControllerUnit, LineKillFieldPool memoryPool) {
        _conflictControllerUnit = conflictControllerUnit;
        _memoryPool = memoryPool;
    }

    private void Subscribe() {
        _conflictControllerUnit.CompletedMurder += OnCompletedMurder;
    }

    private void Unsubscribe() {
        _conflictControllerUnit.CompletedMurder -= OnCompletedMurder;
    }

    private void OnCompletedMurder(string killer, string killed) {
        var line = _memoryPool.Spawn();
        line.transform.SetParent(transform);
        line.transform.localScale = Vector3.one;
        line.Initialize(killer, killed, this);
    }
}