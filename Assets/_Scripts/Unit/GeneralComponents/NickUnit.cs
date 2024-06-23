using TMPro;
using UnityEngine;
using Zenject;

public class NickUnit : MonoBehaviour {
    private TextMeshProUGUI _viewText;
    private bool _initialized = false;
    private Camera _camera;

    public void Initialize(string nickname) {
        if (!_initialized) {
            GetComponent();
            _initialized = true;
        }
        SetValueComponent(nickname);
    }

    [Inject]
    private void Construct(Camera camera) {
        _camera = camera;
    }

    private void Update() {
        transform.LookAt(_camera.transform.position);
    }

    private void GetComponent() {
        _viewText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void SetValueComponent(string nickname) {
        _viewText.text = nickname;
    }
}