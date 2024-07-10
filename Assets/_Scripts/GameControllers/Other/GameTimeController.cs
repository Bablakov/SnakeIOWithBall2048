using UnityEngine;

public static class GameTimeController {
    private const float STOPPED_TIME = 0f;
    private const float RUNNING_TIME = 1f;

    public static void StopTime() {
        Time.timeScale = STOPPED_TIME;
    }

    public static void StartTime() {
        Time.timeScale = RUNNING_TIME;
    }
}