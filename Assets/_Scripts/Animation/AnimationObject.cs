using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationObject : MonoBehaviour {
    [SerializeField, Range(0.1f, 10f)] private float timeOneIteration;
    [SerializeField, Range(0, 10)] private int numberIteration;
    [SerializeField] private AnimationObject nextAnimationScaleObject;
    [SerializeField] private Transform objectAnimation;
    [SerializeField] private bool animationColor;
    [SerializeField] private Image image;
    [SerializeField] private Color StartColor;
    [SerializeField] private Color EndColor;

    private const float END_VALUE_ANIMATION_SCALING = 1.1f;
    
    private Sequence _sequence;

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
        TurnOffAndReduce(objectAnimation);
    }

    private void CreateNewAnimation() {
        CreateSequence();
        if (animationColor) {
            SetAnimationSettingsColor(image);
        } else {
            SetAnimationSettingsScale(objectAnimation);
        }
    }
    
    private bool CheckExistingAnimation() {
        return _sequence != null;
    }

    private void DeleteExistingAnimation() {
        _sequence.Kill();
    }

    private static void TurnOffAndReduce(Transform objectAnimation) {
        objectAnimation.gameObject.SetActive(false);
    }

    private void CreateSequence() {
        _sequence = DOTween.Sequence();
    }

    private void SetAnimationSettingsScale(Transform objectAnimation) {
        objectAnimation.gameObject.SetActive(true);
        for (int i = 0; i < numberIteration; i++) {
            _sequence.Append(objectAnimation.DOScale(END_VALUE_ANIMATION_SCALING, timeOneIteration / 2));
            _sequence.Append(objectAnimation.DOScale(1f, timeOneIteration / 2));
        }

        _sequence.Append(objectAnimation.DOScale(0, 0).OnComplete(() => {
            if (nextAnimationScaleObject != null) {
                nextAnimationScaleObject.AnimationAppearance();
            }
            TurnOffAndReduce(transform);
        }));
        _sequence.Play();
    }

    private void SetAnimationSettingsColor(Image imageAnimation) {
        imageAnimation.gameObject.SetActive(true);
        for (int i = 0; i < numberIteration; i++) {
            _sequence.Append(image.DOColor(EndColor, timeOneIteration / 2));
            _sequence.Append(image.DOColor(StartColor, timeOneIteration / 2));
        }

        _sequence.Append(objectAnimation.DOScale(0, 0).OnComplete(() => {
            if (nextAnimationScaleObject != null) {
                nextAnimationScaleObject.AnimationAppearance();
            }
            TurnOffAndReduce(transform);
        }));
        _sequence.Play();
    }
}