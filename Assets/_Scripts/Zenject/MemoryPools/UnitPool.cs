using Zenject;

public class UnitPool : MemoryPool<Unit> {
    protected override void OnSpawned(Unit item) {
        item.gameObject.SetActive(true);
    }

    protected override void OnDespawned(Unit item) {
        item.gameObject.SetActive(false);
    }
}