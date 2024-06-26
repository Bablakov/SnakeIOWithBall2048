using UnityEngine;

public class BootstrapGame : MonoBehaviour{
    [SerializeField] private Player player;
    [SerializeField] private ControllerSections controllerSections;
    [SerializeField] private ControllerUnits controllerUnits;
    [SerializeField] private SoundController soundController;
    [SerializeField] private UIController uiController;

    private void Awake() {
        controllerSections.Initialize();
        controllerUnits.Initialize();
        soundController.Initialize();
        uiController.Initialize();
        player.Initialize("Вы");
    }
}