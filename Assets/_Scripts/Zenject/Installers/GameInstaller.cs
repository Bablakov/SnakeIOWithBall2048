using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {
    [SerializeField] private ControllerSections controllerSections;
    [SerializeField] private ControllerUnits controllerUnits;
    [SerializeField] private InputGame _inputGame;
    [SerializeField] private Section section;
    [SerializeField] private Player player;
    [SerializeField] private Unit unit;

    private NavigatorEnemy _navigatorEnemy;

    public override void InstallBindings() {
        BindInputSystem();
        BindMemoryPool();
        BindSignal();
    }

    private void BindInputSystem() {
        Container.Bind<InputGame>().FromInstance(_inputGame);

        _navigatorEnemy = new NavigatorEnemy(player, controllerUnits, controllerSections);

        Container.Bind<NavigatorEnemy>().FromInstance(_navigatorEnemy);
    }

    private void BindMemoryPool() {
        Container.BindMemoryPool<Section, SectionPool>().FromComponentInNewPrefab(section);
        Container.BindMemoryPool<Unit, UnitPool>().FromComponentInNewPrefab(unit);
    }

    private void BindSignal() {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<AddedSectionSignal>().OptionalSubscriber();
        Container.DeclareSignal<ReleasedObjectSignal<Section>>().OptionalSubscriber();
        Container.DeclareSignal<ReleasedObjectSignal<Unit>>().OptionalSubscriber();
        Container.DeclareSignal<ConflictedUnitsSignal>().OptionalSubscriber();
    }
}   