using UnityEngine;
using TMPro;
using Zenject;

public class ViewNumberKilledEnemy : MonoBehaviour { 
    private TextMeshProUGUI _textMeshPro;
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
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void GetAndSetStartValue() {
        _textMeshPro.text = "0";
    }

    private void Subscribe() {
        _counterKilles.ChangedNumberEnemiesKilled += OnChangedNubmerEnemiesKilled;
    }

    private void Unsubscribe() {
        _counterKilles.ChangedNumberEnemiesKilled -= OnChangedNubmerEnemiesKilled;
    }

    private void OnChangedNubmerEnemiesKilled(int numberKilled) {
        _textMeshPro.text = numberKilled.ToString();
    }

    private void OnDestroy() {
        Unsubscribe();
    }
}