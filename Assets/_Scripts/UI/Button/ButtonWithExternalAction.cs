using TMPro;
using UnityEngine.Events;

public class ButtonWithExternalAction : HidingButton {
    public void Initialize(UnityAction actionExitPanel) {
        base.Initialize();

        AddMethodInEventClick(actionExitPanel);
        AddEventOnButton();
    }
}