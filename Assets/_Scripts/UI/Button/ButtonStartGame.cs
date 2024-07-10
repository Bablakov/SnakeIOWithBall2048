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
        YandexGame.savesData.startLevelPlayer = 0;
        GameLoadScenteContorller.LoadScene(NAME_GAME_SCENE);
    }
}