using YG;
using System;
using Zenject;

public class ProcessorPlayerRevival : IDisposable {
    private const int ID_REVIVAL_PLAYER = 1;

    private Player _player;
    private DeathPlayerPanel _deathPlayerPanel;

    [Inject]
    public ProcessorPlayerRevival(Player player) { 
        _player = player;
        Subscribe();
    }

    public void SetValue(DeathPlayerPanel deathPlayerPanel) {
        _deathPlayerPanel = deathPlayerPanel;
    }

    private void Subscribe() {
        YandexGame.OpenVideoEvent += StopGame;
        YandexGame.CloseVideoEvent += StartGame;
        YandexGame.RewardVideoEvent += RevivalPlayer;
    }

    private void Unsubscribe() {
        YandexGame.OpenVideoEvent -= StopGame;
        YandexGame.CloseVideoEvent -= StartGame;
        YandexGame.RewardVideoEvent -= RevivalPlayer;
    }

    private void RevivalPlayer(int id) {
        if (id == ID_REVIVAL_PLAYER) {
            GameAnalyticsController.SendEvent("PlayerRevived");
            _player.Enable();
            _deathPlayerPanel.Disable();
        }
    }

    private void StopGame() {
        GameTimeController.StopTime();
    }

    private void StartGame() {
        GameTimeController.StartTime();
    }

    public void Dispose() {
        Unsubscribe();
    }
}