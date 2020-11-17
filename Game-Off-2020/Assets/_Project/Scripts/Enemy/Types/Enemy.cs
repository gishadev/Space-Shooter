using UnityEngine;

namespace SpaceGame.EnemyLogic
{
    public abstract class Enemy : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private int maxHealth = default;

        public int Health
        {
            get { return _health; }
            set { _health = Mathf.Clamp(value, 0, maxHealth); }
        }
        int _health;

        Collider2D _collider;

        private void Awake()
        {
            if (_collider == null) _collider = GetComponent<Collider2D>();
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

        public abstract void TakeDamage(int dmg);
        public abstract void Die();
    }
}