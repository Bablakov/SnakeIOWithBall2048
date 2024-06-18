using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {
    [SerializeField] private Section section;
    [SerializeField] private Player player;
    [SerializeField] private InputGame _inputGame;

    public override void InstallBindings() {
        Container.Bind<InputGame>().FromInstance(_inputGame);

        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<AddedSectionSignal>().OptionalSubscriber();
        Container.BindMemoryPool<Section, SectionPool>().FromComponentInNewPrefab(section);
    }
}   