public class ButtonPauseGame : StandartButton {
    public override void Initialize() {
        base.Initialize();

        AddMethodInEventClick(PauseGame);
        AddEventOnButton();
    }

    private void PauseGame() {
        
    }
}