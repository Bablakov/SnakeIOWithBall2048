using UnityEngine.SceneManagement;

public static class GameLoadScenteContorller {
    public static void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadScene(int sceneId) {
        SceneManager.LoadScene(sceneId);
    }
}