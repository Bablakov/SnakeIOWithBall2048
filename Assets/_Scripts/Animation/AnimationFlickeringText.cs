using DG.Tweening;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class AnimationFlickeringText : MonoBehaviour {
    [SerializeField, Range(0.1f, 2f)] private float duration = 1f;

    private TextMeshProUGUI _textMeshPro;
    private Color _color;
    private Sequence _sequence;

    public void Initialize() {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _color = _textMeshPro.color;
        StartAnimation();
    }

    public void StartAnimation() {
        if (_sequence != null) {
            _sequence.Kill();
        }
        _sequence = DOTween.Sequence();

        _sequence.Append(DOTween.To(() => _textMeshPro.color, color => _textMeshPro.color = color, new Color(_color.r, _color.g, _color.b, 0), duration)
            .SetLoops(-1, LoopType.Yoyo));
        _sequence.Play();
    }
}