using UnityEngine;
using UnityEngine.UIElements;

public class SphereSection : Section {
    [SerializeField] private int level;

    public override Vector3 PositionFront => transform.position + _collider.radius * transform.forward * transform.localScale.z;
    public override Vector3 PositionBack => transform.position - _collider.radius * transform.forward * transform.localScale.z;
    
    private SphereCollider _collider;

    protected override void Awake() {
        base.Awake();
        Level = level;
    }

    protected override void GetComponents() {
        base.GetComponents();
        _collider = GetComponentInChildren<SphereCollider>();
    }

    protected override void CalculateValues() {
        Width = _collider.radius * transform.localScale.z;
    }
}