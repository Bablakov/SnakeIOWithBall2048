using UnityEngine;

public class SphereSection : Section {
    public override Vector3 PositionFront => transform.position + _collider.radius * transform.forward * transform.localScale.z;
    public override Vector3 PositionBack => transform.position - _collider.radius * transform.forward * transform.localScale.z;
    
    private SphereCollider _collider;
    private MeshRenderer _meshRenderer;

    protected override void GetComponents() {
        base.GetComponents();
        _collider = GetComponentInChildren<SphereCollider>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    protected override void CalculateValues() {
        Width = _collider.radius * transform.localScale.z;
        transform.localScale = new Vector3(1 + (0.1f * Level), 1 + (0.1f * Level), 1 + (0.1f * Level));
    }

    protected override void SetValueAppropriateLevel() {
        _meshRenderer.material.color = SectionConfig.Sections[Level].ColorSection;
        Text.text = SectionConfig.Sections[Level].Text;
    }
}