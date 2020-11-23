using System.Collections;
using UnityEngine;

namespace SpaceGame.EnemyLogic
{
    public class Enemy : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private int maxHealth = default;

        [Header("Dropping Power-Ups")]
        [SerializeField] private PowerUpDropper powerUpDropper = default;

        public PowerUpDropper PowerUpDropper => powerUpDropper;

        public int Health
        {
            get { return _health; }
            set { _health = Mathf.Clamp(value, 0, maxHealth); }
        }
        int _health;

        Collider2D _collider;
        Material _mat;

        private void OnEnable()
        {
            if (_collider == null) _collider = GetComponent<Collider2D>();
            if (_mat == null) _mat = GetComponent<SpriteRenderer>().material;
        }

        private void OnValidate()
        {
            powerUpDropper.UpdateDropItems();
        }

        public void RestoreHealth()
        {
            Health = maxHealth;
        }

        public virtual void OnStartSpawning()
        {
            if (_collider == null) _collider = GetComponent<Collider2D>();

            _collider.enabled = false;
        }

        public virtual void OnSpawn()
        {
            _collider.enabled = true;
        }

        public void TakeDamage(int dmg)
        {
            Health -= dmg;
            StartCoroutine(DamageShaderCoroutine());

            if (Health <= 0) 
                Die();
        }

        IEnumerator DamageShaderCoroutine()
        {
            var progress = 1f;
            while (progress > 0f)
            {
                progress -= Time.deltaTime * 25f;
                _mat.SetFloat("_DmgControl", progress);
                yield return null;
            }
            _mat.SetFloat("_DmgControl", 0f);
        }

        public virtual void Die()
        {
            Effects.VFX.VFXManager.Instance.Emit("Enemy_Destroy", transform.position);
            StopAllCoroutines();
            _mat.SetFloat("_DmgControl", 0f);
            gameObject.SetActive(false);

            if (PowerUpDropper.IsDrop())
                PowerUpDropper.Drop(transform.position);
        }
    }
}