using Game.Board;
using UnityEngine;

namespace Game
{
    public static class CellUtils 
    {

        public static Vector2Int ToCell(this Vector3 position)
        {

            Vector2Int floorPosition = new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));
            return (Vector2Int)BoardController.Instance.gemTilemap.LocalToCell(position);
        }


    }
}
