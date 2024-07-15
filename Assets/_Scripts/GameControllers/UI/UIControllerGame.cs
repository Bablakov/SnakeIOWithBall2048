using Zenject;

public class UIControllerGame : UIController {
    private ProcessorPlayerRevival _processorPlayerRevival;
    private ViewNumberKilledEnemy _viewNumberKilledEnemy;
    private ProcessorPlayerDeath _processorPlayerDeath;
    private DeathPlayerPanel _deathPlayerPanel;
    private ResultPanel _resultPanel;
    private KillField _killField;

    public override void Initialize() {
        base.Initialize();
        SetValues();
        DisableComponents();
    }

    [Inject]
    private void Construct(ProcessorPlayerDeath processorPlayerDeath, ProcessorPlayerRevival processorPlayerRevival) {
        _processorPlayerRevival = processorPlayerRevival;
        _processorPlayerDeath = processorPlayerDeath;
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