using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {
    [SerializeField] private InputGame _inputGame;
    [SerializeField] private Section section;
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;

    public override void InstallBindings() {
        BindInputSystem();
        BindMemoryPool();
        BindSignal();
    }

    private void BindInputSystem() {
        Container.Bind<InputGame>().FromInstance(_inputGame);
    }

    private void BindMemoryPool() {
        Container.BindMemoryPool<Section, SectionPool>().FromComponentInNewPrefab(section);
        Container.BindMemoryPool<Enemy, EnemyPool>().FromComponentInNewPrefab(enemy);
    }

    private void BindSignal() {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<AddedSectionSignal>().OptionalSubscriber();
        Container.DeclareSignal<ReleasedSectionSignal>().OptionalSubscriber();
        Container.DeclareSignal<KilledEnemySignal>().OptionalSubscriber();
        Container.DeclareSignal<CollisionedUnitsSignal>().OptionalSubscriber();
    }
}   