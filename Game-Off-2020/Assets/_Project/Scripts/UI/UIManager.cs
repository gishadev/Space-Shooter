using TMPro;
using UnityEngine;

namespace SpaceGame.UI
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton
        public static UIManager Instance { get; private set; }
        #endregion

        [SerializeField] private TMP_Text restartText = default;

        public ScoreUI ScoreUI
        { get; private set; }

        private void Awake()
        {
            Instance = this;
            ScoreUI = GameObject.FindObjectOfType<ScoreUI>();
        }

        public void ActivateRestartText()
        {
            restartText.enabled = true;
        }
    }
}