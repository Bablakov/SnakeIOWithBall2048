using System;
using YG;

public class ProcessorRewardedStartGame : IDisposable {
    private const string NAME_GAME_SCENE = "Game";

    public ProcessorRewardedStartGame() {
        Subscribe();
    }

    private void Subscribe() {
        YandexGame.RewardVideoEvent += LoadRewardedGameScene;
    }

    private void Unsubscribe() {
        YandexGame.RewardVideoEvent -= LoadRewardedGameScene;
    }

    private void LoadRewardedGameScene(int id) {
        if (id == StorageIDRewardedAds.LOAD_SCENE) {
            GameAnalyticsController.SendEvent("GameStartedWithRewarded");
            YandexGame.savesData.startLevelPlayer = 6;
            GameLoadSceneContorller.LoadScene(NAME_GAME_SCENE);
        }
    }

    public void Dispose() {
        Unsubscribe();
    }
}