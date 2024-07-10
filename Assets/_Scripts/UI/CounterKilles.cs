using System;
using Zenject;

public class CounterKilles : IDisposable {
    public event Action<int> ChangedNumberEnemiesKilled;
    public int _numberEnemiesKilled { get; private set; }

    private Player _player;

    [Inject]
    public CounterKilles(Player player) { 
        _player = player;
        _numberEnemiesKilled = 0;
        Subscribe();
    }

    private void Subscribe() {
        _player.KilledEnemy += OnKilledEnemy;
    }

    private void Unsubscribe() {
        _player.KilledEnemy += OnKilledEnemy;
    }

    private void OnKilledEnemy() {
        _numberEnemiesKilled++;
        ChangedNumberEnemiesKilled?.Invoke(_numberEnemiesKilled);
    }

    public void Dispose() {
        Unsubscribe();
    }
}