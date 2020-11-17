using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceGame.Core
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance { get; private set; }
        #endregion

        private void Awake()
        {
            Instance = this;
            Initialize();
        }

        void Initialize()
        {
            ScoreManager.OnInitialize();
        }

        public void ReloadLevel()
        {
            SceneManager.LoadScene(0);
        }
    }
}