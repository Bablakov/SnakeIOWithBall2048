using UnityEngine;
using Zenject;

public class BootstrapGame : MonoBehaviour{
    [SerializeField] private Player player;
    [SerializeField] private ControllerSections _controllerSections;
    [SerializeField] private ControllerUnits _controllerUnits;

    private ConflictControllerUnit _collisionControllerUnit;

    private void Awake() {
        player.Initialize();
        _controllerSections.Initialize();
        _controllerUnits.Initialize();
    }

    [Inject]
    private void Construct(SignalBus signalBus, SectionPool sectionPool) {
        _collisionControllerUnit = new ConflictControllerUnit(signalBus, sectionPool);
    }
}