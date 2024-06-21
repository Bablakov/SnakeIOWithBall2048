using UnityEngine;
using Zenject;

public class BootstrapGame : MonoBehaviour{
    [SerializeField] private Player player;
    [SerializeField] private ControllerFreeSectionOnScene _controllerFreeSection;
    [SerializeField] private ControllerFreeEnemyOnScene _controllerFreeEnemy;

    private CollisionControllerUnit _collisionControllerUnit;

    private void Awake() {
        player.Initialize();
        _controllerFreeSection.Initialize();
        _controllerFreeEnemy.Initialize();
    }

    [Inject]
    private void Construct(SignalBus signalBus, SectionPool sectionPool) {
        _collisionControllerUnit = new CollisionControllerUnit(signalBus, sectionPool);
    }
}