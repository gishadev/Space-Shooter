using SpaceGame.Player;
using UnityEngine;

namespace SpaceGame.PowerUp
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] private string effectName = default;

        PlayerInfluencer _influencer;
        public PlayerInfluencer Influencer => _influencer;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Player").TryGetComponent(out _influencer))
                Debug.LogError("Player Influencer wasn't found!");
        }

        public virtual void OnInteract()
        {
            Effects.Audio.AudioManager.Instance.PlaySFX("PowerUp_Use");
        }

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