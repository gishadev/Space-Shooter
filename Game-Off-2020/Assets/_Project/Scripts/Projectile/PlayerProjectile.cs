using SpaceGame.EnemyLogic;
using UnityEngine;

namespace SpaceGame.Projectile
{
    public class PlayerProjectile : Projectile
    {
        public override void OnHit(RaycastHit2D hitInfo)
        {
            Enemy enemy;
            if (hitInfo.collider.TryGetComponent(out enemy))
                enemy.TakeDamage(Damage);

            Effects.VFX.VFXManager.Instance.Emit("Player_Projectile_Destroy", transform.position);
            DestroyProjectile();
        }
    }
}