using UnityEngine;
using Zenject;

public class SectionPool : MemoryPool<Section> {
    protected override void OnSpawned(Section item) {
        item.gameObject.SetActive(true);
    }

    protected override void OnDespawned(Section item) {
        item.gameObject.SetActive(false);
    }
}