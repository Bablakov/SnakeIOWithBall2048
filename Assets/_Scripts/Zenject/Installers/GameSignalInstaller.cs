using Zenject;

internal class GameSignalInstaller : MonoInstaller {
    public override void InstallBindings() {
        BindSignal();
    }

    private void BindSignal() {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<AddedSectionSignal>().OptionalSubscriber();
        Container.DeclareSignal<ReleasedObjectSignal<Section>>().OptionalSubscriber();
        Container.DeclareSignal<ReleasedObjectSignal<Unit>>().OptionalSubscriber();
        Container.DeclareSignal<ConflictedSignal>().OptionalSubscriber();
    }
}