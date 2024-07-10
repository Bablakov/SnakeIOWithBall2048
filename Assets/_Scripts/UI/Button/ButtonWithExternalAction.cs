using UnityEngine.Events;

public class ButtonWithExternalAction : ButtonStandart {
    public void Initialize(UnityAction actionExitPanel) {
        base.Initialize();

        AddMethodInEventClick(actionExitPanel);
        AddEventOnButton();
    }
}