using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller {
    [SerializeField] private SoundController soundController;
    [SerializeField] private UIController uiController;

    public override void InstallBindings() {
        BindMonoBehaviourObject();
        BindDefaultClass();
    }

    private void BindMonoBehaviourObject() {
        Container.BindInterfacesAndSelfTo<SoundController>().FromInstance(soundController);
        Container.BindInterfacesAndSelfTo<UIController>().FromInstance(uiController);
    }

    private void BindDefaultClass() {
        Container.BindInterfacesAndSelfTo<ProcessorRewardedStartGame>().AsSingle();
    }
}