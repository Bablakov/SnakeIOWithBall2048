using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {
    [SerializeField] private ControllerSections controllerSections;
    [SerializeField] private ControllerUnits controllerUnits;
    [SerializeField] private InputGame _inputGame;
    [SerializeField] private Section section;
    [SerializeField] private Camera _camera;
    [SerializeField] private Player player;
    [SerializeField] private Unit unit;
    [SerializeField] private ViewLineKilledField killedField;

    private NavigatorEnemy _navigatorEnemy;

    public override void InstallBindings() {
        BindInputSystem();
        BindMemoryPool();
        BindSignal();
    }

    private void BindInputSystem() {
        _navigatorEnemy = new NavigatorEnemy(player, controllerUnits, controllerSections);

        Container.Bind<ControllerUnits>().FromInstance(controllerUnits);
        Container.Bind<NavigatorEnemy>().FromInstance(_navigatorEnemy);
        Container.Bind<InputGame>().FromInstance(_inputGame);
        Container.Bind<Camera>().FromInstance(_camera);
        Container.Bind<Player>().FromInstance(player);
    }

    private void BindMemoryPool() {
        Container.BindMemoryPool<Section, SectionPool>().FromComponentInNewPrefab(section);
        Container.BindMemoryPool<Unit, UnitPool>().FromComponentInNewPrefab(unit);
        Container.BindMemoryPool<ViewLineKilledField, LineKillFieldPool>().FromComponentInNewPrefab(killedField);
    }

    private void BindSignal() {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<AddedSectionSignal>().OptionalSubscriber();
        Container.DeclareSignal<ReleasedObjectSignal<Section>>().OptionalSubscriber();
        Container.DeclareSignal<ReleasedObjectSignal<Unit>>().OptionalSubscriber();
        Container.DeclareSignal<ConflictedUnitsSignal>().OptionalSubscriber();
        Container.DeclareSignal<PutOnSectionSignal>().OptionalSubscriber();
        Container.DeclareSignal<DiedPlayerSignal>().OptionalSubscriber();
        Container.DeclareSignal<DiedEnemySignal>().OptionalSubscriber();
        Container.DeclareSignal<CompletedMurderSignal>().OptionalSubscriber();
    }
}   