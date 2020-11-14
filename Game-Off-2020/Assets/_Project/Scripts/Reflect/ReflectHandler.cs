using SpaceGame.World;
using UnityEngine;

namespace SpaceGame.ReflectMovement
{
    public static class ReflectHandler
    {
        private static float boundsOffset = 1f;
        private static float MaxY => WorldBounds.MaxY + boundsOffset;
        private static float MaxX => WorldBounds.MaxX + boundsOffset;

        public static bool IsInView(Vector2 position)
            => position.x > -MaxX && position.x < MaxX && position.y > -MaxY && position.y < MaxY;

        public static Vector3 GetReflectedPosOf(Vector3 pos)
        {
            Vector2 posSign = new Vector2(Mathf.Sign(pos.x), Mathf.Sign(pos.y));
            Vector2 reflectSign = new Vector2(Mathf.Sign(MaxX - Mathf.Abs(pos.x)), Mathf.Sign(MaxY - Mathf.Abs(pos.y)));

            var reflectedPos = pos * reflectSign;

            return reflectedPos + 0.25f * posSign;
        }
    }
}
