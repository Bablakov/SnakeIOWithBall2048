using UnityEngine;
using TMPro;
using Zenject;

public class ViewNumberKilledEnemy : MonoBehaviour { 
    private TextMeshProUGUI _textMeshPro;
    private string _startText;
    private CounterKilles _counterKilles;

    public void Initialize() {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _startText = _textMeshPro.text;
        _textMeshPro.text = _startText + " " + 0;
        Subscribe();
    }

    [Inject]
    private void Construct(CounterKilles counterKilles) {
        _counterKilles = counterKilles;
    }

    private void Subscribe() {
        _counterKilles.ChangedNumberEnemiesKilled += OnChangedNubmerEnemiesKilled;
    }

    private void Unsubscribe() {
        _counterKilles.ChangedNumberEnemiesKilled -= OnChangedNubmerEnemiesKilled;
    }

    private void OnChangedNubmerEnemiesKilled(int numberKilled) {
        _textMeshPro.text = _startText + " " + numberKilled;
    }
}