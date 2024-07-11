using System;
using Zenject;

public class ProcessorPlayerDeath : IDisposable {
    private DeathPlayerPanel _deathPlayerPanel;
    private CounterKilles _counterKilles;
    private SectionConfig _sectionConfig;
    private ResultPanel _resultPanel;
    private bool _canRevive;
    private Player _player;

    [Inject]
    public ProcessorPlayerDeath(Player player, CounterKilles counterKilles, SectionConfig sectionConfig) {
        _counterKilles = counterKilles;
        _sectionConfig = sectionConfig;
        _canRevive = true;
        _player = player;
        Subscribe();
    }

    public void SetValue(ResultPanel resultPanel, DeathPlayerPanel deathPlayerPanel) {
        _deathPlayerPanel = deathPlayerPanel;
        _resultPanel = resultPanel;
    }

    public void ViewResultPanel() {
        DataProcessor.Save(_player.Level, _counterKilles._numberEnemiesKilled);

        string bestPoint = _sectionConfig.Sections[DataProcessor.LoadBestLevel()].Text;
        string bestKilled = DataProcessor.LoadBestNumberKilled().ToString();

        string currentPoint = _sectionConfig.Sections[_player.Level].Text;
        string currentKilled = _counterKilles._numberEnemiesKilled.ToString();

        _resultPanel.Enable(bestPoint, bestKilled, currentPoint, currentKilled);
    }

    private void Subscribe() {
        _player.DiedPlayer += OnDiedPlayer;
    }

    private void Unsubscribe() {
        _player.DiedPlayer -= OnDiedPlayer;
    }

    private void OnDiedPlayer() {
        if (_canRevive) {
            _deathPlayerPanel.Enable();
            _canRevive = false;
            GameAnalyticsController.SendEvent("FirstDiedPlayer");
        } else {
            ViewResultPanel();
            GameAnalyticsController.SendEvent("SecondDiedPlayer");
        }
    }

    public void Dispose() {
        Unsubscribe();
    }
}