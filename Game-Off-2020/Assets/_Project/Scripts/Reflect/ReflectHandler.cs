using UnityEngine;

namespace SpaceGame.Movement
{
    public static class ReflectHandler
    {
        private static float boundsOffset = 1f;

        private static float MaxY => Camera.main.orthographicSize + boundsOffset;
        private static float MaxX => Camera.main.orthographicSize * Screen.width / Screen.height + boundsOffset;

        public static bool IsInView(Vector2 position)
            => position.x > -MaxX && position.x < MaxX && position.y > -MaxY && position.y < MaxY;

        public static Vector3 GetReflectedPosOf(Vector3 pos)
        {
            var reflectedPos = pos * -Vector2.one;
            var sign = new Vector2(Mathf.Sign(reflectedPos.x), Mathf.Sign(reflectedPos.y));
            return reflectedPos + 0.1f * -sign;
        }
    }
}