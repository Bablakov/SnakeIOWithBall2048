public class UIControllerMenu : UIController {
    private ButtonStartGame _buttonStartGame;
    private ButtonRewardStartGame _buttonRewardStartGame;

    protected override void GetComponents() {
        base.GetComponents();
        _buttonStartGame = GetComponentInChildren<ButtonStartGame>();
        _buttonRewardStartGame = GetComponentInChildren<ButtonRewardStartGame>();
    }

    protected override void InitializeComponents() {
        base.InitializeComponents();
        _buttonStartGame.Initialize();
        _buttonRewardStartGame.Initialize();
    }
}