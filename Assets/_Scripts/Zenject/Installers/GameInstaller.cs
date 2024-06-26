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
    
    public override void InstallBindings() {
        BindObject();
        BindMemoryPool();
        BindSignal();
    }

    private void BindObject() {
        Container.Bind<ControllerSections>().FromInstance(controllerSections);
        Container.Bind<ControllerUnits>().FromInstance(controllerUnits);
        Container.Bind<Camera>().FromInstance(_camera);
        Container.Bind<Player>().FromInstance(player);

        Container.Bind<CounterKilles>().AsSingle();
        Container.Bind<NavigatorEnemy>().AsSingle();
        Container.Bind<ConflictController>().AsSingle();
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