using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {
    [SerializeField] private ControllerMergeSection controllerMergeSection;
    [SerializeField] private ControllerSections controllerSections;
    [SerializeField] private ControllerUnits controllerUnits;
    [SerializeField] private SoundControllerGame soundController;
    [SerializeField] private UIControllerGame uiController;
    [SerializeField] private Camera _camera;
    [SerializeField] private Player player;
    
    public override void InstallBindings() {
        BindMonoBehaviourObject();
        BindWithLinkOnObjectScene();
        BindDefaultClass();
    }

    private void BindMonoBehaviourObject() {
        Container.BindInterfacesAndSelfTo<ControllerSections>().FromInstance(controllerSections);
        Container.BindInterfacesAndSelfTo<ControllerUnits>().FromInstance(controllerUnits);
        Container.BindInterfacesAndSelfTo<SoundControllerGame>().FromInstance(soundController);
        Container.BindInterfacesAndSelfTo<UIControllerGame>().FromInstance(uiController);
    }

    private void BindWithLinkOnObjectScene() {
        Container.Bind<Camera>().FromInstance(_camera);
        Container.Bind<Player>().FromInstance(player);
    }

    private void BindDefaultClass() {
        Container.BindInterfacesAndSelfTo<ConflictController>().AsSingle();
        Container.BindInterfacesAndSelfTo<CounterKilles>().AsSingle();
        Container.BindInterfacesAndSelfTo<InputDesktop>().AsSingle();
        Container.BindInterfacesAndSelfTo<ControllerMergeSection>().AsSingle();
        Container.Bind<NavigatorEnemy>().AsSingle();
    }
}   