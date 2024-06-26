using UnityEngine;
using TMPro;
using Zenject;

public abstract class Section : MonoBehaviour {
    public bool IsUnit;
    public abstract Vector3 PositionFront { get; }
    public abstract Vector3 PositionBack { get; }
    public Vector3 Position => transform.position;
    public float Width { get; protected set; }
    public int Level { get; protected set; }

    protected TextMeshProUGUI Text;
    protected SectionConfig SectionConfig;

    private StorageSection _controllerSection;

    public virtual void Upgrade() {
        UpdateLevel();
        UpdateSection();
    }

    public void SetLevel(int level) {
        Level = level;
        UpdateSection();
    }

    public void SetNewControllerSection(StorageSection controllerSection) {
        _controllerSection?.Delete(this);
        _controllerSection = controllerSection;
    }

    [Inject]
    protected virtual void Construct(SectionConfig sectionConfig) {
        SectionConfig = sectionConfig;
        GetComponents();
        UpdateSection();
    }

    protected virtual void GetComponents() {
        Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void UpdateLevel() {
        Level++;
    }

    private void UpdateSection() {
        CalculateValues();
        SetValueAppropriateLevel();
    }

    protected abstract void CalculateValues();

    protected abstract void SetValueAppropriateLevel();
}