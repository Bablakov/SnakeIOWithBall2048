using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {
    [SerializeField] private ControllerMergeSection controllerMergeSection;
    [SerializeField] private ControllerSections controllerSections;
    [SerializeField] private ControllerUnits controllerUnits;
    [SerializeField] private SoundControllerGame soundController;
    [SerializeField] private UIControllerGame uiController;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Camera _camera;
    [SerializeField] private Player player;
    
    public override void InstallBindings() {
        BindInputSystem();
        BindMonoBehaviourObject();
        BindWithLinkOnObjectScene();
        BindDefaultClass();
    }

    private void BindInputSystem() {
        if (SystemInfo.deviceType == DeviceType.Handheld) {
            Container.BindInterfacesAndSelfTo<Joystick>().FromInstance(joystick);  
            Container.BindInterfacesAndSelfTo<InputMobile>().AsSingle();
        }
        else {
            joystick.gameObject.SetActive(false);
            Container.BindInterfacesAndSelfTo<InputDesktop>().AsSingle();
        }
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
        Container.BindInterfacesAndSelfTo<ExecutionMethodController>().AsSingle();
        Container.BindInterfacesAndSelfTo<ProcessorPlayerRevival>().AsSingle();
        Container.BindInterfacesAndSelfTo<ControllerMergeSection>().AsSingle();
        Container.BindInterfacesAndSelfTo<ProcessorPlayerDeath>().AsSingle();
        Container.BindInterfacesAndSelfTo<ConflictController>().AsSingle();
        Container.BindInterfacesAndSelfTo<CounterKilles>().AsSingle();
        Container.Bind<NavigatorEnemy>().AsSingle();
    }
}   