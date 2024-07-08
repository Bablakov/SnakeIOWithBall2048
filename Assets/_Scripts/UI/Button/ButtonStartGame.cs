using UnityEngine.SceneManagement;

public class ButtonStartGame : StandartButton {
    private const string NAME_GAME_SCENE = "Game";

    public override void Initialize() {
        base.Initialize();

        AddMethodInEventClick(LoadGameScene);
        AddEventOnButton();
    }

    private void LoadGameScene() {
        SceneManager.LoadScene(NAME_GAME_SCENE);
    }
}