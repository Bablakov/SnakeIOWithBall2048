using DG.Tweening;
using System;
using UnityEngine;

public class TestDoTweenOnCubeOnScene : MonoBehaviour {
    [SerializeField, Range(0.01f, 2f)] private float coefficientScalling;
    [SerializeField, Range(0.01f, 2f)] private float duration;
    [SerializeField, Range(0.01f, 2f)] private float delay;
    [SerializeField] private Ease typeEase;

    private Vector3 startScale;
    private Vector3 endScale;
    private Sequence sequence;
    private TweenCallback _callback;
    private bool _isPlaying;

    private void Awake() {
        startScale = transform.localScale;
        endScale = startScale * coefficientScalling;
    }

    public void SetSequence(TweenCallback callback) {
        if (callback != null) {
            _callback = callback;
        }
    }

    public void PlayAnimation() {
        if (_isPlaying) {
            Debug.Log("KillSequence");
            sequence.Kill();
            _isPlaying = true;
            CreateNewAnimation();
        }
        else {
            _isPlaying = true;
            CreateNewAnimation();
        }

        Debug.Log($"{_isPlaying} - isPlaying");
        _isPlaying = false;
    }

    private void CreateNewAnimation() {
        sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(endScale, duration).SetEase(typeEase)).SetDelay(delay);
        
        sequence.AppendCallback(_callback);

        sequence.Append(transform.DOScale(startScale, duration).SetEase(typeEase).SetDelay(delay));
       
        
        
        /* sequence.Play();*/
    }
}
