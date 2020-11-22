using SpaceGame.Player;
using UnityEngine;

namespace SpaceGame.PowerUp
{
    public abstract class PowerUp : MonoBehaviour
    {
        [SerializeField] private string effectName = default;

        PlayerInfluencer _influencer;
        public PlayerInfluencer Influencer => _influencer;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Player").TryGetComponent(out _influencer))
                Debug.LogError("Player Influencer wasn't found!");
        }

        public abstract void OnInteract();

        void Destroy()
        {
            Effects.VFX.VFXManager.Instance.Emit(effectName, transform.position);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                OnInteract();
                Destroy();
            }
        }
    }
}