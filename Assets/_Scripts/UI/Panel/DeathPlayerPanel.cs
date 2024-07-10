using UnityEngine;
using UnityEngine.UI;
using YG;

public class DeathPlayerPanel : MonoBehaviour {
    [SerializeField, Range(0.1f, 3f)] private float timeViewPanel;
    [SerializeField] private ButtonWithExternalAction _buttonCancelPanel;
    [SerializeField] private ButtonWithExternalAction _buttonRevivalPlayer;
    [SerializeField] private Slider _slider;
    
    private ProcessorPlayerDeath _processorPlayerDeath;
    private float _currentTime;

    public void Initialize(ProcessorPlayerDeath processorPlayerDeath) {
        _processorPlayerDeath = processorPlayerDeath;
        InitializeComponents();
    }

    private void Update() {
        if (_currentTime < 0) {
            AbandonRevival();
        }
        else {
            _currentTime -= Time.deltaTime;
            _slider.value = _currentTime / timeViewPanel;
        }
    }

    public void Enable() {
        _currentTime = timeViewPanel;
        gameObject.SetActive(true);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }

    private void InitializeComponents() {
        _buttonRevivalPlayer.Initialize(StartRewardedAdForRevivalPlayer);
        _buttonCancelPanel.Initialize(AbandonRevival);
    }

    private void StartRewardedAdForRevivalPlayer() {
        YandexGame.RewVideoShow(StorageIDRewardedAds.REVIVAL_PLAYER);
    }

    private void AbandonRevival() {
        Disable();
        _processorPlayerDeath.ViewResultPanel();
    }
}