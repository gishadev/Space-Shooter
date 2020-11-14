using UnityEngine;

namespace SpaceGame.EnemyNamespace
{
    public abstract class Enemy : MonoBehaviour
    {
        public abstract void Init();
        public abstract void TakeDamage(int dmg);
        public abstract void Die();
    }
}