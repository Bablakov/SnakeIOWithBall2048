using UnityEngine;
using Zenject;

public class SphereSection : Section {
    [SerializeField] private int level;

    public override Vector3 PositionFront => transform.position + _collider.radius * transform.forward * transform.localScale.z;
    public override Vector3 PositionBack => transform.position - _collider.radius * transform.forward * transform.localScale.z;
    
    private SphereCollider _collider;
    private MeshRenderer _meshRenderer;

    [Inject]
    protected override void Construct(SectionConfig sectionConfig) {
        base.Construct(sectionConfig);
        Level = level;
        CalculateScale();
        SetValueAppropriateLevel();
    }

    protected override void GetComponents() {
        base.GetComponents();
        _collider = GetComponentInChildren<SphereCollider>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    protected override void CalculateValues() {
        Width = _collider.radius * transform.localScale.z;
    }

    public override void Upgrade() {
        UpdateLevel();
        level = Level;
        CalculateScale();
        CalculateValues();
        SetValueAppropriateLevel();
    }

    private void SetValueAppropriateLevel() {
        _meshRenderer.material.color = SectionConfig.Sections[level].ColorSection;
        Text.text = SectionConfig.Sections[level].Text;
    }

    private void CalculateScale() {
        transform.localScale = new Vector3(1 + (0.1f * Level), 1 + (0.1f * Level), 1 + (0.1f * Level));
    }
}