using UnityEngine;
using TMPro;
using Zenject;
using System;

public abstract class Section : MonoBehaviour {
    [SerializeField] protected TextMeshProUGUI text;
    [SerializeField] private SectionShader sectionShader;

    public abstract Vector3 PositionFront { get; }
    public abstract Vector3 PositionBack { get; }
    public Vector3 Position => transform.position;
    public bool Invulnerability { get; private set; }
    public float Width { get; protected set; }
    public int Level { get; protected set; }

    protected SectionConfig SectionConfig;

    private AnimationFlickeringSection _animationFlickeringSection;
    private AnimationIncreaseSection _animationIncreaseSection;
    private StorageSection _storageSection;

    public virtual void Upgrade() {
        UpdateLevel();
        UpdateSection();
    }

    public void SetLevel(int level) {
        _animationIncreaseSection.StopAnimation(Width * 2 * Vector3.one);
        Level = level;
        UpdateSection();
    }

    public void SetNewControllerSection(StorageSection storageSection) {
        CalculateValues();
        _storageSection?.Delete(this);
        if (storageSection == null)
            _animationIncreaseSection.StopAnimation(Width * 2 * Vector3.one);
        _storageSection = storageSection;
    } 

    public void PlayAnimation() {
        CalculateValues();
        if (_storageSection != null)
            _animationIncreaseSection.PlayAnimation(Width * 2 * Vector3.one);
    }

    public void SetSequence(DG.Tweening.TweenCallback callback) {
        CalculateValues();
        _animationIncreaseSection.SetSequence(callback);
    }

    public bool CheckOwnerSection(StorageSection storageSection) {
        return _storageSection == storageSection;
    }

    public void OnEnable() {
        SetValueAppropriateLevel();
        if (Invulnerability) {
            SetVulnerability();
        }
    }

    public void SetInvulnerability() {
        Invulnerability = true;
        _animationFlickeringSection.StartAnimation();
    }

    public void SetVulnerability() {
        Invulnerability = false;
        _animationFlickeringSection.StopAnimation();
    }

    [Inject]
    protected virtual void Construct(SectionConfig sectionConfig) {
        SectionConfig = sectionConfig;
        GetComponents();
        UpdateSection();
        InitializeComponents();
    }

    protected virtual void GetComponents() {
        _animationIncreaseSection = GetComponent<AnimationIncreaseSection>();
        _animationFlickeringSection = GetComponentInChildren<AnimationFlickeringSection>();
    }

    private void InitializeComponents() {
        _animationFlickeringSection.Initialize(sectionShader);
    }

    private void UpdateLevel() {
        if (Invulnerability) {
            _animationFlickeringSection.StartAnimation();
        }
        Level++;
    }

    private void UpdateSection() {
        CalculateValues();
        SetValueAppropriateLevel();
    }

    protected abstract void CalculateValues();

    protected abstract void SetValueAppropriateLevel();
}