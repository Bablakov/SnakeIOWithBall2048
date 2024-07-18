using UnityEngine;
using YG;

public class BootstrapGame : MonoBehaviour{
    [SerializeField] private Player player;

    private void Awake() {
        if (YandexGame.EnvironmentData.language == "en") {
            player.Initialize("You");
        }
        else if (YandexGame.EnvironmentData.language == "ru") {
            player.Initialize("Вы");
        }
        else {
            player.Initialize("You");
        }
    }
}