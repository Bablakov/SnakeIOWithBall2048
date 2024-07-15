using UnityEngine;
using DG.Tweening;

public class TestDoTweenOnCubeOnScene : MonoBehaviour {
    [SerializeField, Range(0.1f, 2f)] private float duration = 1f;
    [SerializeField, Range(0, 100)] private int loop;
    [SerializeField] private SectionShader _sectionShader;

    private Sequence _sequence;

    public void StartAnimation() {
        if (_sequence != null) {
            _sequence.Kill();
        }

        _sequence = DOTween.Sequence();

        //_sequence.Append(DOTween.To(() => _material.color, color => _material.color = color, new Color(_color.r, _color.g, _color.b, 0), duration).SetLoops(_loop, LoopType.Yoyo));
        _sequence.Append(DOTween.To(() => _sectionShader.Alpha, alpha => _sectionShader.Alpha = alpha, 0, duration / 2));
        _sequence.Append(DOTween.To(() => _sectionShader.Alpha, alpha => _sectionShader.Alpha = alpha, 1, duration / 2));
        _sequence.SetLoops(loop, LoopType.Yoyo);
        _sequence.Play();
    }

    public void StopAnimation() {
        _sectionShader.Alpha = 1f;
        _sequence.Kill();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartAnimation();
        }
    }
}