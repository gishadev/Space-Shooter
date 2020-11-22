using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace SpaceGame.Core
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance { get; private set; }
        #endregion

        PlayerInput _controls;


        private void Awake()
        {
            Instance = this;
            _controls = new PlayerInput();
            Initialize();
        }

        private void OnEnable()
        {
            _controls.UI.Enable();

            _controls.UI.RKey.performed += _ => ReloadLevel();
        }

        private void OnDisable()
        {
            _controls.UI.Disable();

            _controls.UI.RKey.performed -= _ => ReloadLevel();
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