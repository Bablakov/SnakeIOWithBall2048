using UnityEngine;
using TMPro;
using Zenject;

public class ViewNumberKilledEnemy : MonoBehaviour { 
    private TextMeshProUGUI _textMeshPro;
    private string _startText;
    private Player _player;

    public void Initialize() {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _startText = _textMeshPro.text;
        _textMeshPro.text = _startText + " " + 0;
        Subscribe();
    }

    [Inject]
    private void Construct(Player player) {
        _player = player;
    }

    private void Subscribe() {
        _player.ChangedNumberEnemiesKilled += OnChangedNubmerEnemiesKilled;
    }

    private void Unsubscribe() {
        _player.ChangedNumberEnemiesKilled -= OnChangedNubmerEnemiesKilled;
    }

    private void OnChangedNubmerEnemiesKilled(int numberKilled) {
        _textMeshPro.text = _startText + " " + numberKilled;
    }
}