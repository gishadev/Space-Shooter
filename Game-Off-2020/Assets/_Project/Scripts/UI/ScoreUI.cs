using TMPro;
using UnityEngine;

namespace SpaceGame.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText = default;

        public void UpdateScoreUI(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}