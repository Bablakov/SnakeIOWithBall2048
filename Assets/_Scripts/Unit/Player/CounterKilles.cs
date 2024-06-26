using System;
using Zenject;

public class CounterKilles {
    public event Action<int> ChangedNumberEnemiesKilled;

    private Player _player;
    private int _numberEnemiesKilled;

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

}