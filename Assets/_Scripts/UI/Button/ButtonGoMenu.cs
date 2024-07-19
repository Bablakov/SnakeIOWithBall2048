using DG.Tweening;

public class ButtonGoMenu : ButtonStandart {
    private const string NAME_MENU_SCENE = "Main";

    public override void Initialize() {
        base.Initialize();
        AddMethodInEventClick(GoMenu);
        AddEventOnButton();
    }

    public void GoMenu() {
        DOTween.KillAll();
        GameAnalyticsController.SendEvent("WentMenu");
        GameLoadSceneContorller.LoadScene(NAME_MENU_SCENE);
    }
}