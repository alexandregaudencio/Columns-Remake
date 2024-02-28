using Game.Board.Gems;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Game.Board
{
    public class BoardController : MonoBehaviour
    {
        [field: SerializeField] public Vector2Int Size { get; private set; } = new Vector2Int(7, 13);
        [field: SerializeField] public Tilemap gemTilemap { get; private set; }

        public int[,] gemCells;
        public static BoardController Instance { get; private set; }
        public Bounds Bounds => new Bounds(
            (transform.position + new Vector3(Size.x / 2, Size.y / 2)),
            new Vector3(Size.x, Size.y));

        public event Action<List<Vector2Int>> Cellsfilled;

        public Vector3 CellToLocal(Vector2Int cell)
        {
            return gemTilemap.LocalToCell((Vector3Int)cell);
        }
        public bool IsValidCell(Vector2Int position)
        {
            return (position.x >= 0 && position.y >= 0 && position.x < Size.x && position.y < Size.y);
        }



        public Vector3 GetStartBlockPosition()
        {
            Vector2Int initialCellBlock = new Vector2Int(Size.x / 2, (int)Size.y);
            return CellToLocal(initialCellBlock);
        }



        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            gemCells = new int[Size.x, Size.y];
            ResetGemsInCells();

        }

        private void ResetGemsInCells()
        {
            for (int i = 0; i < gemCells.GetLength(0); i++)
            {
                for (int j = 0; j < gemCells.GetLength(1); j++)
                {
                    gemCells[i, j] = -1;
                }
            }

        }

        public void SetGemsAuto()
        {
            SetGemTile(BlockController.Instance.GemBlock.PositionGemPair);
        }


        public void SetGemTile(Dictionary<Vector2Int, Gem> gems)
        {
            foreach (KeyValuePair<Vector2Int, Gem> gemProperties in gems)
            {
                gemCells[gemProperties.Key.x, gemProperties.Key.y] = gemProperties.Value.Index;
                gemTilemap.SetTile((Vector3Int)gemProperties.Key, gemProperties.Value.TileBase);
            }
            List<Vector2Int> cells = new List<Vector2Int>(gems.Keys);
            Cellsfilled?.Invoke(cells);
        }

        public int GetGemIndex(Vector2Int position)
        {
            return gemCells[position.x, position.y];
        }


        public bool HasGem(Vector2Int position)
        {
            if (IsValidCell(position))
                return gemCells[position.x, position.y] != -1;
            return false;
        }



        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Bounds.center, Bounds.size);


        }





    }
}
