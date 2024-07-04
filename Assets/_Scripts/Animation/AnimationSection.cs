using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class AnimationSection : MonoBehaviour {
    [SerializeField, Range(0.01f, 2f)] private float coefficientScalling;
    [SerializeField, Range(0.01f, 2f)] private float duration;
    [SerializeField, Range(0.01f, 2f)] private float delay;
    [SerializeField] private Ease typeEase;

    private Vector3 startScale;
    private Vector3 endScale;
    private Sequence sequence;
    private TweenCallback _callback;

    public void SetSequence(TweenCallback callback) {
        if (callback != null) {
            _callback = callback;
        }
    }

    public void PlayAnimation(Vector3 currentScale) {
        startScale = currentScale;
        endScale = currentScale * coefficientScalling;
        CreateNewAnimation();
    }

    public void StopAnimation(Vector3 localScale) {
        sequence.Kill();
        transform.localScale = localScale;
    }

    private void CreateNewAnimation() {

        sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(endScale, duration).SetEase(typeEase)).SetDelay(delay);
        if (_callback != null) {
            sequence.AppendCallback(_callback);
        }
        sequence.Append(transform.DOScale(startScale, duration).SetEase(typeEase).SetDelay(delay));
    }
}