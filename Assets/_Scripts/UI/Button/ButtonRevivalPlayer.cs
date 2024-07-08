using Zenject;

public class ButtonRevivalPlayer : StandartButton {
    private Player _player;
    private LosingPanel _losingPanel;

    public override void Initialize() {
        base.Initialize();
        AddMethodInEventClick(GoMenu);
        AddEventOnButton();
    }

    public void SetValue(LosingPanel losingPanel) {
        _losingPanel = losingPanel;
    }

    [Inject]
    private void Construct(Player player) {
        _player = player;
    }

    private void GoMenu() {
        _player.gameObject.SetActive(true);
        _losingPanel.Disable();
    }
}