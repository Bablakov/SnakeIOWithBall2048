using UnityEngine;
using TMPro;

public abstract class Section : MonoBehaviour {
    public abstract Vector3 PositionFront { get; }
    public abstract Vector3 PositionBack { get; }
    
    public float Width { get; protected set; }
    public int Level { get; protected set; }
    public Vector3 Position => transform.position;

    protected Section PreviousSection;
    protected Section NextSection;
    protected SectionInfo SectionInfo;
    protected TextMeshProUGUI Text;

    protected virtual void Awake() {
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