using DG.Tweening;
using UnityEngine;

public class AnimationFlickeringSection : MonoBehaviour {
    [SerializeField, Range(0.1f, 2f)] private float duration = 1f;
    
    private Color _color => _sectionConfig.Sections[_section.Level].ColorSection;
    
    private SectionConfig _sectionConfig;
    private Material _material;
    private Sequence _sequence;
    private Section _section;
    private Shader _materialShader;

    public void Initialize(SectionConfig sectionConfig, Section section) {
        _material = GetComponent<Renderer>().material;
        _sectionConfig = sectionConfig;
        _section = section;
    }

    public void StartAnimation() {
        if (_sequence != null) {
            _sequence.Kill();
        }
        _sequence = DOTween.Sequence();

        _sequence.Append(DOTween.To(() => _material.color, color => _material.color = color, new Color(_color.r, _color.g, _color.b, 0), duration).SetLoops(-1, LoopType.Yoyo));
        _sequence.Play();
    }

    public void StopAnimation() {
        _material.color = new Color(_color.r, _color.g, _color.b, 1);
        _sequence.Kill();
    }
}