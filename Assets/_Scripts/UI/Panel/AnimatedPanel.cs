using DG.Tweening;
using UnityEngine;

public abstract class AnimatedPanel : MonoBehaviour {
    [SerializeField] private Transform[] objectAnimations;
    [SerializeField, Range(0.1f, 2f)] private float scalePanel;
    [SerializeField, Range(0.01f, 10f)] private float duration;
    private const float END_VALUE_ANIMATION_SCALING = 1f;

    private Sequence _sequence;

    protected void EnableAnimation() {
        AnimationAppearance();
    }

    protected void DisableAnimation() {
        InitialAnimationCheck();
    }

    public void AnimationAppearance() {
        InitialAnimationCheck();
        SetUpAnimationObjects();
        CreateNewAnimation();
    }

    private void InitialAnimationCheck() {
        if (CheckExistingAnimation()) {
            DeleteExistingAnimation();
        }
    }

    private void SetUpAnimationObjects() {
        foreach (var objectAnimation in objectAnimations) {
            TurnOffAndReduce(objectAnimation);
        }
    }

    private void CreateNewAnimation() {
        CreateSequence();
        foreach (var objectAnimation in objectAnimations) {
            SetAnimationSettings(objectAnimation);
        }
        _sequence.Append(transform.DOScale(scalePanel, duration));
    }
    
    private bool CheckExistingAnimation() {
        return _sequence != null;
    }

    private void DeleteExistingAnimation() {
        _sequence.Kill();
    }

    private static void TurnOffAndReduce(Transform objectAnimation) {
        objectAnimation.localScale = Vector3.zero;
    }

    private void CreateSequence() {
        _sequence = DOTween.Sequence();
    }

    private void SetAnimationSettings(Transform objectAnimation) {
        _sequence.Append(objectAnimation.DOScale(END_VALUE_ANIMATION_SCALING, duration));
    }
}