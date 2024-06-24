using Zenject;

public class LineKillFieldPool : MemoryPool<ViewLineKilledField> {
    protected override void OnSpawned(ViewLineKilledField item) {
        item.gameObject.SetActive(true);
    }

    protected override void OnDespawned(ViewLineKilledField item) {
        item.gameObject.SetActive(false);
    }
}