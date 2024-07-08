using System;
using Zenject;

public class UIControllerGame : UIController, IDisposable {
    private ViewNumberKilledEnemy _viewNumberKilledEnemy;
    private CounterKilles _counterKilles;
    private SectionConfig _sectionConfig;
    private LeaderBoard _leaderBoard;
    private LosingPanel _losingPanel;
    private KillField _killField;
    private Player _player;

    public override void Initialize() {
        base.Initialize();
        Subscribe();
        _losingPanel.Disable();
    }

    protected override void GetComponents() {
        base.GetComponents();
        _viewNumberKilledEnemy = GetComponentInChildren<ViewNumberKilledEnemy>();
        _leaderBoard = GetComponentInChildren<LeaderBoard>();
        _losingPanel = GetComponentInChildren<LosingPanel>();
        _killField = GetComponentInChildren<KillField>();
    }

    protected override void InitializeComponents() {
        base.InitializeComponents();
        _viewNumberKilledEnemy.Initialize();
        _leaderBoard.Initailize();
        _losingPanel.Initialize();
        _killField.Initialize();
    }

    public void Dispose() {
        Unsubscribe();
    }

    [Inject]
    private void Construct(Player player, CounterKilles counterKilles, SectionConfig sectionConfig) {
        _counterKilles = counterKilles;
        _sectionConfig = sectionConfig;
        _player = player;
    }

    private void Subscribe() {
        _player.DiedPlayer += OnDiedPlayer;
    }

    private void Unsubscribe() {
        _player.DiedPlayer -= OnDiedPlayer;
    }

    private void OnDiedPlayer() {
        DataProcessor.Save(_player.Level, _counterKilles._numberEnemiesKilled);

        string bestPoint = _sectionConfig.Sections[DataProcessor.LoadBestLevel()].Text;
        string bestKilled = DataProcessor.LoadBestNumberKilled().ToString();

        string currentPoint =  _sectionConfig.Sections[_player.Level].Text;
        string currentKilled = _counterKilles._numberEnemiesKilled.ToString();

        _losingPanel.Enable(bestPoint, bestKilled, currentPoint, currentKilled);
    }
}