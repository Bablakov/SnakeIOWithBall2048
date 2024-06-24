using UnityEngine;

public static class GameSoundController {
    public static void TurnOnSound() {
        AudioListener.volume = 1f;
    }

    public static void TurnOffSound() {
        AudioListener.volume = 0f;
    }
}