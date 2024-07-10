using UnityEngine;
using TMPro;

public class ViewLineKilledField : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI fieldKiller;
    [SerializeField] private TextMeshProUGUI fieldKilled;

    private float _currentTime;
    private float _time = 1.5f;
    private KillField _killField;

    public void Initialize(string nameKiller, string nameDied, KillField killField) {
        fieldKiller.text = nameKiller;
        fieldKilled.text = nameDied;
        _killField = killField;
        _currentTime = 0f;
    }

    private void Update() {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _time) {
            _killField.DisableLine(this);
        }
    }
}