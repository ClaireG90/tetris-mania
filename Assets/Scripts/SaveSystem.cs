using UnityEngine;

namespace TetrisMania
{
    /// <summary>
    /// Simple PlayerPrefs based save utility.
    /// </summary>
    public static class SaveSystem
    {
        private const string BestScoreKey = "bestScore";
        private const string SessionsKey = "sessionsCount";
        private const string NoAdsKey = "noAdsPurchased";

        public static int BestScore
        {
            get => PlayerPrefs.GetInt(BestScoreKey, 0);
            set => PlayerPrefs.SetInt(BestScoreKey, value);
        }

        public static int SessionsCount
        {
            get => PlayerPrefs.GetInt(SessionsKey, 0);
            set => PlayerPrefs.SetInt(SessionsKey, value);
        }

        public static bool NoAdsPurchased
        {
            get => PlayerPrefs.GetInt(NoAdsKey, 0) == 1;
            set => PlayerPrefs.SetInt(NoAdsKey, value ? 1 : 0);
        }

        public static void Save()
        {
            PlayerPrefs.Save();
        }
    }
}
