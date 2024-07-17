using DG.Tweening;
using UnityEngine;
using Zenject;

public class AnimationFlickeringSection : MonoBehaviour {
    [SerializeField, Range(0.1f, 2f)] private float duration = 1f;
    
    private SectionShader _sectionShader;
    private SnakeConfig _shakeConfig;
    private Sequence _sequence;
    private int _loop;

    public void Initialize(SectionShader sectionShader) {
        _sectionShader = sectionShader;
        _sectionShader.Initialize();
    }

    public void StartAnimation() {
        if (_sequence != null) {
            _sequence.Kill();
        }

        _loop = (int)(_shakeConfig.TimeInvulnerability / duration);
         
        _sequence = DOTween.Sequence();

        _sequence.Append(DOTween.To(() => _sectionShader.Alpha, alpha => _sectionShader.Alpha = alpha, 0, duration / 2));
        _sequence.Append(DOTween.To(() => _sectionShader.Alpha, alpha => _sectionShader.Alpha = alpha, 1, duration / 2));
        _sequence.SetLoops(10, LoopType.Yoyo);
        _sequence.Play();
    }

    public void StopAnimation() {
        _sequence.Kill();
        _sectionShader.SetFullVisual();
    }

    [Inject]
    private void Construct(SnakeConfig snakeConfig) {
        _shakeConfig = snakeConfig;
    }
}