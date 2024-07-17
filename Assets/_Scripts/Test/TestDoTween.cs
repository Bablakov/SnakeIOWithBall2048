using UnityEngine;
using DG.Tweening;

public class TestDoTween : MonoBehaviour {
    [SerializeField] private Transform[] objectAnimations;
    [SerializeField, Range(0.01f, 10f)] private float duration;

    private Sequence _sequence;

    public void Start() {
        Animation();
    }

    public void Animation() {
        if (_sequence != null) {
            _sequence.Kill();
        }

        foreach (Transform t in objectAnimations) {
            t.gameObject.SetActive(false);
            t.localScale = Vector3.zero;
        }

        _sequence = DOTween.Sequence();

        foreach (var transform in objectAnimations) {
            transform.gameObject.SetActive(true);
            _sequence.Append(transform.DOScale(1, duration));
        }
        _sequence.Play();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Animation();
        }
    }
}