using UnityEngine;
using System;

public class CollisionHandler : MonoBehaviour {
    public event Action<Section> AddedSection;

    private void OnTriggerEnter(Collider other) {
        if (TryGetComponentSection(other, out Section section)) {
            AddedSection?.Invoke(section);
        }
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