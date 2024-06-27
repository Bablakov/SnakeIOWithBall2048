using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {
    [SerializeField] private ControllerSections controllerSections;
    [SerializeField] private ControllerUnits controllerUnits;
    [SerializeField] private SoundController soundController;
    [SerializeField] private UIController uiController;
    [SerializeField] private Camera _camera;
    [SerializeField] private Player player;
    
    public override void InstallBindings() {
        BindMonoBegaviourObject();
        BindWithLinkOnObjectScene();
        BindDefaultClass();
    }

    private void BindMonoBegaviourObject() {
        Container.BindInterfacesAndSelfTo<ControllerSections>().FromInstance(controllerSections);
        Container.BindInterfacesAndSelfTo<ControllerUnits>().FromInstance(controllerUnits);
        Container.BindInterfacesAndSelfTo<SoundController>().FromInstance(soundController);
        Container.BindInterfacesAndSelfTo<UIController>().FromInstance(uiController);
    }

    private void BindWithLinkOnObjectScene() {
        Container.Bind<Camera>().FromInstance(_camera);
        Container.Bind<Player>().FromInstance(player);
    }

    private void BindDefaultClass() {
        Container.BindInterfacesAndSelfTo<ConflictController>().AsSingle();
        Container.BindInterfacesAndSelfTo<CounterKilles>().AsSingle();
        Container.BindInterfacesAndSelfTo<InputDesktop>().AsSingle();
        Container.Bind<NavigatorEnemy>().AsSingle();
    }
}   