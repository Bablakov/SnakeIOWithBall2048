using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public abstract class Section : MonoBehaviour {
    public abstract Vector3 PositionFront { get; }
    public abstract Vector3 PositionBack { get; }
    public float Width { get; protected set; }
    public int Level { get; protected set; }
    public Vector3 Position => transform.position;

    protected Section PreviousSection;
    protected Section NextSection;

    private TextMeshProUGUI Text;

    protected virtual void Awake() {
        GetComponents();
        CalculateValues();
    }
/*
    public virtual void SetPreviousSection(Section section) {
        if (PreviousSection != null) {
            PreviousSection.SetNextSection(null);
        }
        PreviousSection = section;
    }

    public virtual void SetNextSection(Section section) { 
        if (NextSection != null) {
            NextSection.SetPreviousSection(null);
        }
        NextSection = section;
        NextSection.SetPreviousSection(this);
    }*/

    protected virtual void GetComponents() {
        Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected abstract void CalculateValues();
}