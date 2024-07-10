using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ViewFreeSpeedUpOnPlayer : MonoBehaviour {
    private ControllerSpeedUpSnake _controllerSpeedUpSnake;
    private Slider _slider;
    private Camera _camera;

    public void Initialize(ControllerSpeedUpSnake controllerSpeedUpSnake) {
        _controllerSpeedUpSnake = controllerSpeedUpSnake;
        _slider = GetComponent<Slider>();
    }

    public void Enable() {
        gameObject.SetActive(true);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }

    [Inject]
    private void Construct(Camera camera) {
        _camera = camera;
    }

    private void Update() {
        _slider.value = _controllerSpeedUpSnake.PercentageFreeSpeedUp;
        transform.LookAt(_camera.transform.position);
    }
}