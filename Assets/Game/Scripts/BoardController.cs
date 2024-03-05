using Game.Board.Gems;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Game.Board
{
    public class BoardController : MonoBehaviour
    {
        private GemMatchManager gemMatchManager;
        [field: SerializeField] public Vector2Int Size { get; private set; } = new Vector2Int(7, 13);
        [field: SerializeField] public Tilemap gemTilemap { get; private set; }

        public int[,] gemCells;
        public static BoardController Instance { get; private set; }

        public Bounds Bounds => new Bounds(
            (transform.position + new Vector3(Size.x / 2, Size.y / 2)),
            new Vector3(Size.x, Size.y));

        public event Action<List<Vector2Int>> Cellsfilled;
        public event Action<List<Vector2Int>> CellsCleaned;

        private void Awake()
        {
            Instance = this;
            gemMatchManager = GetComponent<GemMatchManager>();
        }

        private void Start()
        {
            gemCells = new int[Size.x, Size.y];
            ResetCells();

        }

        private void OnEnable()
        {
            gemMatchManager.Match += OnMatch;
        }

        private void OnDisable()
        {
            gemMatchManager.Match -= OnMatch;

        }

        private void OnMatch(List<List<Vector2Int>> positions)
        {

            List<Vector2Int> singleGemPosition = positions.SelectMany(x => x).Distinct().ToList();
            Debug.Log(singleGemPosition.Count);
            RemoveGems(singleGemPosition);

        }

        public Vector3 CellToLocal(Vector2Int cell)
        {
            return gemTilemap.LocalToCell((Vector3Int)cell);
        }
        public bool IsValidCell(Vector2Int position)
        {
            return (position.x >= 0 && position.y >= 0 && position.x < Size.x && position.y < Size.y);
        }
        public void SetGemInCell(Vector2Int position, Gem gem)
        {
            if (!IsValidCell(position))
                throw new ArgumentOutOfRangeException(string.Concat(position, " is not valid cell position."));
            gemCells[position.x, position.y] = gem.Index;
            gemTilemap.SetTile((Vector3Int)position, gem.TileBase);

        }

        public void SetGemsInCells(Dictionary<Vector2Int, Gem> positionGemPairs)
        {
            foreach (KeyValuePair<Vector2Int, Gem> positionGemPair in positionGemPairs)
            {
                SetGemInCell(positionGemPair.Key, positionGemPair.Value);
            }
            List<Vector2Int> cells = new List<Vector2Int>(positionGemPairs.Keys);
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

        public void RemoveGem(Vector2Int position)
        {
            if (!IsValidCell(position)) throw new ArgumentOutOfRangeException(string.Concat(position, " is not valid cell position."));
            gemCells[position.x, position.y] = -1;
            gemTilemap.SetTile((Vector3Int)position, null);

        }

        public void RemoveGems(List<Vector2Int> positions)
        {
            foreach(var position in positions)
            {
                RemoveGem(position);
            }
            CellsCleaned?.Invoke(positions);
        }


        public Vector3 GetStartBlockPosition()
        {
            Vector2Int initialCellBlock = new Vector2Int(Size.x / 2, (int)Size.y);
            return CellToLocal(initialCellBlock);
        }





        private void ResetCells()
        {
            for (int i = 0; i < gemCells.GetLength(0); i++)
            {
                for (int j = 0; j < gemCells.GetLength(1); j++)
                {
                    gemCells[i, j] = -1;
                }
            }

        }

        public void SetGemBlockAuto()
        {
            SetGemsInCells(BlockController.Instance.GemBlock.PositionGemPair);
        }





        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Bounds.center, Bounds.size);


        }





    }
}
