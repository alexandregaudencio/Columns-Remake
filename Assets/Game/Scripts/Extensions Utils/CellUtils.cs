using Game.Board;
using UnityEngine;

namespace Game
{
    public static class CellUtils
    {

        public static Vector2Int Floor(this Vector2 vector2)
        {
            return new Vector2Int(Mathf.FloorToInt(vector2.x), Mathf.FloorToInt(vector2.y));
        }

        public static Vector2Int ToInt(this Vector2 vector2)
        {
            return new Vector2Int((int)vector2.x, (int)vector2.y);
        }
        public static Vector3Int ToInt(this Vector3 vector)
        {
            return new Vector3Int((int)vector.x, (int)vector.y, (int)vector.z);
        }
        public static Vector2Int ToCell(this Vector3 position)
        {

            Vector2Int floorPosition = new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));
            return (Vector2Int)BoardController.Instance.gemTilemap.LocalToCell(position);
        }




    }
}
