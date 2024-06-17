using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {
    [SerializeField] private Player player;

    private InputGame _inputGame;

    public override void InstallBindings() {

        _inputGame = new InputDesktop();
        Container.Bind<InputGame>().FromInstance(_inputGame);

        Container.BindInstance(player);

    }
}