using UnityEngine;

public class SphereSection : Section {
    [SerializeField] private int level;

    public override Vector3 PositionFront => transform.position + _collider.radius * transform.forward * transform.localScale.z;
    public override Vector3 PositionBack => transform.position - _collider.radius * transform.forward * transform.localScale.z;
    
    private SphereCollider _collider;
    private MeshRenderer _meshRenderer;
    private SectionConfig _sectionConfig;

    protected override void Awake() {
        base.Awake();
        _sectionConfig = Resources.Load<SectionConfig>("SectionConfig");
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

    public override void Upgrade(/*SectionInfo sectionInfo*/) {
        UpdateLevel();
        level = Level;
        CalculateScale();
        SetValueAppropriateLevel();
    }

    private void SetValueAppropriateLevel() {
        _meshRenderer.material.color = _sectionConfig.Sections[level].ColorSection;
        Text.text = _sectionConfig.Sections[level].Text;
    }

    private void CalculateScale() {
        transform.localScale = new Vector3(1 + (0.1f * Level), 1 + (0.1f * Level), 1 + (0.1f * Level));
    }
}