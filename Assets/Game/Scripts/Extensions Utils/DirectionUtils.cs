using UnityEngine;

namespace Game
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        UpperLeft,
        LowerLeft,
        LowerRight,
        UpperRight,
    }

    public static class Vector2IntUtils
    {

        public static Vector2Int UpperLeft(this Vector2Int vector2Int)
        {
            return new Vector2Int(-1, 1);
        }

        public static Vector2Int LowerLeft(this Vector2Int vector2Int)
        {
            return new Vector2Int(-1, -1);
        }

        public static Vector2Int UpperRight(this Vector2Int vector2Int)
        {
            return new Vector2Int(1, 1);
        }

        public static Vector2Int LowerRight(this Vector2Int vector2Int)
        {
            return new Vector2Int(1, -1);
        }

    }
}
