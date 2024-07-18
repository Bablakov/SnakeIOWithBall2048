using Zenject;
using UnityEngine;

public class UIControllerGame : UIController {
    [SerializeField] private AnimationObject _desktopAnimation;
    [SerializeField] private AnimationObject _mobileAnimation;
    [SerializeField] private Transform _buttonSpeedUp;

    private ProcessorPlayerRevival _processorPlayerRevival;
    private ViewNumberKilledEnemy _viewNumberKilledEnemy;
    private ProcessorPlayerDeath _processorPlayerDeath;
    private DeathPlayerPanel _deathPlayerPanel;
    private ResultPanel _resultPanel;
    private KillField _killField;

    private bool _isMobile;

    public override void Initialize() {
        base.Initialize();
        SetValues();
        DisableComponents();
        if (_isMobile) {
            _desktopAnimation.gameObject.SetActive(false);
            _mobileAnimation.AnimationAppearance();
        }
        else {
            _mobileAnimation.gameObject.SetActive(false);
            _buttonSpeedUp.gameObject.SetActive(false);
            _desktopAnimation.AnimationAppearance();
        }
    }

    [Inject]
    private void Construct(ProcessorPlayerDeath processorPlayerDeath, ProcessorPlayerRevival processorPlayerRevival, InputGame inputGame) {
        _processorPlayerRevival = processorPlayerRevival;
        _processorPlayerDeath = processorPlayerDeath;
        _isMobile = inputGame is InputMobile;
    }

    protected override void GetComponents() {
        _viewNumberKilledEnemy = GetComponentInChildren<ViewNumberKilledEnemy>();
        _deathPlayerPanel = GetComponentInChildren<DeathPlayerPanel>();
        _resultPanel = GetComponentInChildren<ResultPanel>();
        _killField = GetComponentInChildren<KillField>();
    }

    protected override void InitializeComponents() {
        _deathPlayerPanel.Initialize(_processorPlayerDeath);
        _viewNumberKilledEnemy.Initialize();
        _resultPanel.Initialize();
        _killField.Initialize();
    }

    private void SetValues() {
        _processorPlayerDeath.SetValue(_resultPanel, _deathPlayerPanel);
        _processorPlayerRevival.SetValue(_deathPlayerPanel);
    }

    private void DisableComponents() {
        _resultPanel.Disable();
        _deathPlayerPanel.Disable();
    }
}