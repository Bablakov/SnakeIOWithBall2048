using UnityEngine.SceneManagement;

public class ButtonStartGame : StandartButton {
    public override void Initialize() {
        base.Initialize();

        AddMethodInEventClick(LoadGameScene);
        AddEventOnButton();
    }

    private void LoadGameScene() {
    }
}