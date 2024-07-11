using UnityEngine.SceneManagement;
using YG;

public class ButtonStartGame : ButtonStandart {
    private const string NAME_GAME_SCENE = "Game";

    public override void Initialize() {
        base.Initialize();

        AddMethodInEventClick(LoadGameScene);
        AddEventOnButton();
    }

    private void LoadGameScene() {
        GameAnalyticsController.SendEvent("GameStarterWithoutRewarded");
        YandexGame.savesData.startLevelPlayer = 0;
        GameLoadSceneContorller.LoadScene(NAME_GAME_SCENE);
    }
}