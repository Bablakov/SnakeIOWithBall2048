using UnityEngine;
using TMPro;
using Zenject;

public abstract class Section : MonoBehaviour {
    public abstract Vector3 PositionFront { get; }
    public abstract Vector3 PositionBack { get; }
    public Vector3 Position => transform.position;
    public float Width { get; protected set; }
    public int Level { get; protected set; }

    protected TextMeshProUGUI Text;
    protected SectionConfig SectionConfig;

    private StorageSection _storageSection;

    private AnimationSection _animationSection;

    public virtual void Upgrade() {
        UpdateLevel();
        UpdateSection();
    }

    public void SetLevel(int level) {
        Level = level;
        UpdateSection();
    }

    public void SetNewControllerSection(StorageSection storageSection) {
        CalculateValues();
        _storageSection?.Delete(this);
        if (storageSection == null)
            _animationSection.StopAnimation(Width * 2 * Vector3.one);
        _storageSection = storageSection;
    } 

    public void PlayAnimation() {
        CalculateValues();
        if (_storageSection != null)
            _animationSection.PlayAnimation(Width * 2 * Vector3.one);
    }

    public void SetSequence(DG.Tweening.TweenCallback callback) {
        CalculateValues();
        _animationSection.SetSequence(callback);
    }

    [Inject]
    protected virtual void Construct(SectionConfig sectionConfig) {
        SectionConfig = sectionConfig;
        GetComponents();
        UpdateSection();
    }

    protected virtual void GetComponents() {
        Text = GetComponentInChildren<TextMeshProUGUI>();
        _animationSection = GetComponent<AnimationSection>();
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