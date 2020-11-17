using UnityEngine;

namespace SpaceGame.Core
{
    public static class ScoreManager
    {
        private static int _score = 0;

        public static int Score
        {
            get { return _score; }
            private set { _score = Mathf.Max(value, 0); }
        }

        public static void OnInitialize()
        {
            Score = 0;
        }

        public static void AddScore(int n)
        {
            Score += n;
            Debug.LogFormat("Now Score: <b><color=yellow>{0}</color></b>", Score);
        }
    }
}