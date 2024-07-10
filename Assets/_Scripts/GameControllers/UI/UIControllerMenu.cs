using YG;

public class UIControllerMenu : UIController {
    private ButtonStartGame _buttonStartGame;
    private ButtonWithExternalAction _buttonRewardStartGame;

    protected override void GetComponents() {
        base.GetComponents();
        _buttonStartGame = GetComponentInChildren<ButtonStartGame>();
        _buttonRewardStartGame = GetComponentInChildren<ButtonWithExternalAction>();
    }

    protected override void InitializeComponents() {
        base.InitializeComponents();
        _buttonStartGame.Initialize();
        _buttonRewardStartGame.Initialize(LaunchRewardAd);
    }

    private void LaunchRewardAd() {
        YandexGame.RewVideoShow(StorageIDRewardedAds.LOAD_SCENE);
    }
}