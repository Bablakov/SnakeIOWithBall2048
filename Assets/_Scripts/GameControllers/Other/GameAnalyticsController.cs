using DreamTeamMobile;

public static class GameAnalyticsController {
    public static void SendEvent(string eventName) {
        GoogleAnalytics.Instance.TrackEvent(eventName);
    }
}