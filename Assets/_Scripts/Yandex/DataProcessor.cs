using YG;

public static class DataProcessor {
    private const string LIDERBOARD_WITH_MAX_SECTION = "LBLevelSection";
    
    public static void Save(int level, int numberKilled) {
        if (SaveLevel(level) | SaveNumberKilled(numberKilled)) {
            YandexGame.SaveProgress();
        }
    }

    public static int LoadBestLevel() {
        return YandexGame.savesData.bestLevelSection;
    }

    public static int LoadBestNumberKilled() {
        return YandexGame.savesData.bestNumberKilled;
    }

    private static bool SaveLevel(int level) {
        if (level > YandexGame.savesData.bestLevelSection) {
            YandexGame.savesData.bestLevelSection = level;
            var value = CalculateValueForLiderboardByLevel(level);
            YandexGame.NewLeaderboardScores(LIDERBOARD_WITH_MAX_SECTION, value);
            return true;
        }
        return false;
    }

    private static bool SaveNumberKilled(int numberKilled) {
        if (numberKilled > YandexGame.savesData.bestNumberKilled) {
            YandexGame.savesData.bestNumberKilled = numberKilled;
            return true;
        }
        return false;
    }

    private static int CalculateValueForLiderboardByLevel(int level) {
        int value = 1;
        for (int i = 0; i <= level; i++) {
            value *= 2;
        }
        return value;
    }
}