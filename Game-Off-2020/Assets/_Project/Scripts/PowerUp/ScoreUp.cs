using SpaceGame.Core;
using UnityEngine;

namespace SpaceGame.PowerUp
{
    public class ScoreUp : PowerUp
    {
        [SerializeField] private int count = default;

        public override void OnInteract()
        {
            base.OnInteract();
            ScoreManager.AddScore(count);
        }
    }
}