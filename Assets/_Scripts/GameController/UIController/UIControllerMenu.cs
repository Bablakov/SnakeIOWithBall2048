public class UIControllerMenu : UIController {
    private ButtonStartGame _buttonStartGame;

    protected override void GetComponents() {
        base.GetComponents();
        _buttonStartGame = GetComponentInChildren<ButtonStartGame>();
    }

    protected override void InitializeComponents() {
        base.InitializeComponents();
        _buttonStartGame.Initialize();
    }
}