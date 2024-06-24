using TMPro;
using UnityEngine.SceneManagement;

public class ButtonRestartGame : HidingButton {
    public override void Initialize() {
        base.Initialize();

        AddMethodInEventClick(RestartGame);
        AddEventOnButton();
    }
    private void RestartGame() {
    }
}