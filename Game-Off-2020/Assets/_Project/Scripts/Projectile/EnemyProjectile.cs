﻿using SpaceGame.Player;
using UnityEngine;

namespace SpaceGame.Projectile
{
    public class EnemyProjectile : Projectile
    {
        public override void OnHit(RaycastHit2D hitInfo)
        {
            PlayerInfluencer player;
            if (hitInfo.collider.TryGetComponent(out player))
                player.Die();

            Effects.VFX.VFXManager.Instance.Emit("Enemy_Projectile_Destroy", transform.position);
            DestroyProjectile();
        }
    }
}