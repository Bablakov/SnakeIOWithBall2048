using YG;
using UnityEngine.SceneManagement;

public class ButtonRewardStartGame : StandartButton {
    private const string NAME_GAME_SCENE = "Game";
    private const int ID_LOAD_SCENE = 0;

    public override void Initialize() {
        base.Initialize();

        AddMethodInEventClick(LaunchRewardAd);
        AddEventOnButton();
        YandexGame.RewardVideoEvent += LoadGameScene;
    }

    private void LaunchRewardAd() {
        YandexGame.RewVideoShow(ID_LOAD_SCENE);
    }

    private void LoadGameScene(int id) {
        if (id == ID_LOAD_SCENE) {
            YandexGame.savesData.startLevelPlayer = 6;
            SceneManager.LoadScene(NAME_GAME_SCENE);
        }
    }
}