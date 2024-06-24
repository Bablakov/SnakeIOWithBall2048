using UnityEngine;
using Zenject;

public class BootstrapGame : MonoBehaviour{
    [SerializeField] private Player player;
    [SerializeField] private ControllerSections controllerSections;
    [SerializeField] private ControllerUnits controllerUnits;
    [SerializeField] private SoundController soundController;
    [SerializeField] private UIController uiController;

    private ConflictControllerUnit _collisionControllerUnit;

    private void Awake() {
        controllerSections.Initialize();
        controllerUnits.Initialize();
        soundController.Initialize();
        uiController.Initialize();
        player.Initialize("Вы");
    }

    [Inject]
    private void Construct(SignalBus signalBus, SectionPool sectionPool) {
        _collisionControllerUnit = new ConflictControllerUnit(signalBus, sectionPool);
    }
}