using UnityEngine;
using UnityEngine.UI;

public class WinPlayerPanel : AnimatedPanel {
    [SerializeField, Range(0.1f, 10f)] private float timeViewPanel;
    [SerializeField] private ButtonGoMenu buttonGoMenu;
    [SerializeField] private Slider _slider;
    
    private float _currentTime;

    public void Initialize() {
        buttonGoMenu.Initialize();
    }

    private void Update() {
        if (_currentTime < 0) {
            buttonGoMenu.GoMenu();
        }
        else {
            _currentTime -= Time.deltaTime;
            _slider.value = _currentTime / timeViewPanel;
        }
    }

    public void Enable() {
        _currentTime = timeViewPanel;
        gameObject.SetActive(true);
        EnableAnimation();
    }

    public void Disable() {
        gameObject.SetActive(false);
        DisableAnimation();
    }
}