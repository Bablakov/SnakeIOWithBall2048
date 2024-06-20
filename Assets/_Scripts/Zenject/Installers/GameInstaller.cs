using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {
    [SerializeField] private Section section;
    [SerializeField] private Player player;
    [SerializeField] private InputGame _inputGame;

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
    }

    private void BindSignal() {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<AddedSectionSignal>().OptionalSubscriber();
        Container.DeclareSignal<ReleasedSectionSignal>().OptionalSubscriber();
    }
}   