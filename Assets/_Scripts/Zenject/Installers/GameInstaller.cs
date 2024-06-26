using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {
    [SerializeField] private ControllerSections controllerSections;
    [SerializeField] private ControllerUnits controllerUnits;
    [SerializeField] private ViewLineKilledField killedField;
    [SerializeField] private Section section;
    [SerializeField] private Camera _camera;
    [SerializeField] private Player player;
    [SerializeField] private Unit unit;
    
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
        Container.Bind<Camera>().FromInstance(_camera);
        Container.Bind<Player>().FromInstance(player);

        Container.Bind<ConflictController>().AsSingle();
        Container.Bind<CounterKilles>().AsSingle();
        Container.Bind<InputGame>().To<InputDesktop>().AsSingle();
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
        Container.DeclareSignal<ConflictedSignal>().OptionalSubscriber();
    }
}   