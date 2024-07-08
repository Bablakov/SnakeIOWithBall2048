using DG.Tweening;
using UnityEngine.SceneManagement;

public class ButtonGoMenu : HidingButton {
    private const string NAME_Menu_SCENE = "Main";

    public override void Initialize() {
        base.Initialize();
        AddMethodInEventClick(GoMenu);
        AddEventOnButton();
    }

    private void GoMenu() {
        DOTween.KillAll();
        SceneManager.LoadScene(NAME_Menu_SCENE);
    }
}