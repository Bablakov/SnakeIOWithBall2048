using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Configs/GameConfigInstaller")]
public class GameConfigInstaller : ScriptableObjectInstaller<GameConfigInstaller> {
    [SerializeField] private GameConfig gameConfig;
    public override void InstallBindings() {
        BindConfigs();
    }

    private void BindConfigs() {
        Container.Bind<SnakeConfig>().FromInstance(gameConfig.Snake);
        Container.Bind<SectionConfig>().FromInstance(gameConfig.Section);
        Container.Bind<GameplayConfig>().FromInstance(gameConfig.Gameplay);
    }
}