using UnityEngine;

namespace SpaceGame.World
{
    public static class WorldBounds
    {
        public static float MaxY => Camera.main.orthographicSize;
        public static float MaxX => Camera.main.orthographicSize * Screen.width / Screen.height;
    }
}