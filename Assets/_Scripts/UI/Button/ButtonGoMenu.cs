using DG.Tweening;
using UnityEngine.SceneManagement;

public class ButtonGoMenu : ButtonStandart {
    private const string NAME_MENU_SCENE = "Main";

    public override void Initialize() {
        base.Initialize();
        AddMethodInEventClick(GoMenu);
        AddEventOnButton();
    }

    private void GoMenu() {
        DOTween.KillAll();
        GameLoadScenteContorller.LoadScene(NAME_MENU_SCENE);
    }
}