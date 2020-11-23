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

            DestroyProjectile();
        }

        public override void DestroyProjectile()
        {
            base.DestroyProjectile();
            Effects.VFX.VFXManager.Instance.Emit("Player_Projectile_Destroy", transform.position);
        }
    }
}