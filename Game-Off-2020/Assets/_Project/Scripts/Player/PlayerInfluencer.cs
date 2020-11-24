using System.Collections;
using UnityEngine;

namespace SpaceGame.Player
{
    public class PlayerInfluencer : MonoBehaviour
    {
        [Header("Power Ups Influence")]
        [SerializeField] private GameObject forceField = default;

        public bool IsImmortal { get; private set; } = false;

        Transform _transform;
        PlayerSpaceshipData _spaceshipData;

        private void Awake()
        {
            _transform = transform;
            _spaceshipData = GetComponent<PlayerController>().SpaceshipData;
        }

        private void Start()
        {
            _spaceshipData.LevelIndex = 0;
            Effects.Audio.AudioManager.Instance.PlaySFX("Player_Spawn");
        }

        #region Force Field
        public void ActivateForceField(float affectTime) => StartCoroutine(ForceField(affectTime));

        IEnumerator ForceField(float affectTime)
        {
            IsImmortal = true;
            forceField.SetActive(true);
            yield return new WaitForSeconds(affectTime);
            IsImmortal = false;
            forceField.SetActive(false);
        }
        #endregion

        #region Level Up
        public void LevelUp()
        {
            _spaceshipData.LevelIndex++;
        }

        #endregion

        public void Die()
        {
            Effects.VFX.VFXManager.Instance.Emit("Player_Destroy", _transform.position);
            Effects.Audio.AudioManager.Instance.PlaySFX("Player_Destroy");

            _transform.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") && !IsImmortal) Die();
        }
    }
}