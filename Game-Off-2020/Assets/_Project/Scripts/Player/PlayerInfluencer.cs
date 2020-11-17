using SpaceGame.Core;
using System.Collections;
using UnityEngine;

namespace SpaceGame.Player
{
    public class PlayerInfluencer : MonoBehaviour
    {
        [Header("Power Ups Influence")]
        [SerializeField] private GameObject forceField = default;

        Collider2D _collider;
        PlayerSpaceshipData _spaceshipData;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _spaceshipData = GetComponent<PlayerController>().SpaceshipData;
        }

        private void Start()
        {
            _spaceshipData.LevelIndex = 0;
        }

        #region Force Field
        public void ActivateForceField(float affectTime) => StartCoroutine(ForceField(affectTime));

        IEnumerator ForceField(float affectTime)
        {
            _collider.enabled = false;
            forceField.SetActive(true);
            yield return new WaitForSeconds(affectTime);
            _collider.enabled = true;
            forceField.SetActive(false);
        }
        #endregion

        #region LevelUp
        public void LevelUp()
        {
            _spaceshipData.LevelIndex++;
        }

        #endregion

        void Die()
        {
            GameManager.Instance.ReloadLevel();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy")) Die();
        }
    }
}