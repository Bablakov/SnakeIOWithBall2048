using UnityEngine;
using TMPro;
using Zenject;

public abstract class Section : MonoBehaviour {
    public abstract Vector3 PositionFront { get; }
    public abstract Vector3 PositionBack { get; }
    
    public float Width { get; protected set; }
    public int Level { get; protected set; }
    public Vector3 Position => transform.position;

    protected SectionInfo SectionInfo;
    protected TextMeshProUGUI Text;
    protected SectionConfig SectionConfig;

    public virtual void Upgrade() {
        UpdateLevel();
        UpdateSection();
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