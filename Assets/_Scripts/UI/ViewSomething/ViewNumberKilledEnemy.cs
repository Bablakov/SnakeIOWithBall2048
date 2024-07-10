using UnityEngine;
using TMPro;
using Zenject;

public class ViewNumberKilledEnemy : MonoBehaviour { 
    private TextMeshProUGUI _textMeshPro;
    private string _startText;
    private CounterKilles _counterKilles;

    public void Initialize() {
        GetComponent();
        GetAndSetStartValue();
        Subscribe();
    }

    [Inject]
    private void Construct(CounterKilles counterKilles) {
        _counterKilles = counterKilles;
    }

    private void GetComponent() {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void GetAndSetStartValue() {
        _startText = _textMeshPro.text + " ";
        _textMeshPro.text = _startText + 0;
    }

    private void Subscribe() {
        _counterKilles.ChangedNumberEnemiesKilled += OnChangedNubmerEnemiesKilled;
    }

    private void Unsubscribe() {
        _counterKilles.ChangedNumberEnemiesKilled -= OnChangedNubmerEnemiesKilled;
    }

    private void OnChangedNubmerEnemiesKilled(int numberKilled) {
        _textMeshPro.text = _startText + numberKilled;
    }

    private void OnDestroy() {
        Unsubscribe();
    }
}