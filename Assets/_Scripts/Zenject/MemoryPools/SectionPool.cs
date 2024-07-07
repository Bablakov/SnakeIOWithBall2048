using Zenject;

public class SectionPool : MemoryPool<Section> {
    protected override void OnSpawned(Section item) {
        item.gameObject.SetActive(true);
        item.IsPool = false;
    }

    protected override void OnDespawned(Section item) {
        item.gameObject.SetActive(false);
        item.IsPool = true;
    }
}