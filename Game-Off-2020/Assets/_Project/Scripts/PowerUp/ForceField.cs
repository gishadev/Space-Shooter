using UnityEngine;

namespace SpaceGame.PowerUp
{
    public class ForceField : PowerUp
    {
        [SerializeField] private float affectTime = default;

        public override void OnInteract() => Influencer.ActivateForceField(affectTime);
    }
}