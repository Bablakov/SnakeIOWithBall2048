using YG;

public class UIControllerMenu : UIController {
    private ButtonStartGame _buttonStartGame;
    private ButtonSoundControl _buttonSoundControl;
    private ButtonWithExternalAction _buttonRewardStartGame;

    protected override void GetComponents() {
        _buttonStartGame = GetComponentInChildren<ButtonStartGame>();
        _buttonSoundControl = GetComponentInChildren<ButtonSoundControl>();
        _buttonRewardStartGame = GetComponentInChildren<ButtonWithExternalAction>();
    }

    protected override void InitializeComponents() {
        _buttonStartGame.Initialize();
        _buttonSoundControl.Initialize();
        _buttonRewardStartGame.Initialize(LaunchRewardAd);
    }

    private void LaunchRewardAd() {
        YandexGame.RewVideoShow(StorageIDRewardedAds.LOAD_SCENE);
    }
}