using TMPro;
using UnityEngine.SceneManagement;

public class ButtonGoMenu : HidingButton {
    public override void Initialize() {
        base.Initialize();
        AddMethodInEventClick(GoMenu);
        AddEventOnButton();
    }

    private void GoMenu() {
    }
}