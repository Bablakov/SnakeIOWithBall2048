public class ButtonResumeGame : HidingButton {
    public override void Initialize() {
        base.Initialize();

        AddMethodInEventClick(ResumeGame);
        AddEventOnButton();

        Hide();
    }

    private void ResumeGame() {
        Hide();
    }
}