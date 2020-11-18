using UnityEngine;

namespace SpaceGame.UI
{
    public static class UIManager
    {
        public static ScoreUI ScoreUI
        { get; set; }

        public static void Init()
        {
            ScoreUI = GameObject.FindObjectOfType<ScoreUI>();
        }
    }
}