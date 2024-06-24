using TMPro;
using UnityEngine;

public class ButtonExitGame : HidingButton {
    public override void Initialize() {
        base.Initialize();
        
        AddMethodInEventClick(ExitGame);
        AddEventOnButton();
    }

    private void ExitGame() {
    }
}