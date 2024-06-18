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

    [Inject]
    protected virtual void Construct(SectionConfig sectionConfig) {
        SectionConfig = sectionConfig;
        GetComponents();
        CalculateValues();
    }

    protected virtual void GetComponents() {
        Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected void UpdateLevel() {
        Level++;
    }

    public abstract void Upgrade(/*SectionInfo sectionInfo*/);

    protected abstract void CalculateValues();
}