using UnityEngine;

namespace SpaceGame.Core
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            ScoreManager.Init();
        }
    }
}