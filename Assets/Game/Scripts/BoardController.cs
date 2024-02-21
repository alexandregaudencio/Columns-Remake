using Game.Board.Gems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


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

        public Vector3 CellToLocal(Vector2Int cell)
        {
            return gemTilemap.LocalToCell((Vector3Int)cell);
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

            SetGemTile(new Vector2Int(Random.Range(0, 7), Random.Range(0, 13)), GemProvider.Instance.GetGemRandomly());

        }

        private void ResetGemsInCells()
        {
            for (int i = 0; i < gemCells.GetLength(0); i++)
            {
                for(int j = 0; j < gemCells.GetLength(1); j++)
                {
                    gemCells[i, j] = -1;
                }
            } 

        }

        public void SetGemTile(Vector2Int position, Gem gem)
        {
            gemCells[position.x, position.y] = gem.Index;
            gemTilemap.SetTile((Vector3Int)position, gem.TileBase);
        }

        public bool HasGem(Vector2Int position)
        {
            if(gemCells.GetLength(1) < position.y)
            {
                Debug.Log("está fora");

            } else
            {
                Debug.Log("está DENTRO");

            }
            if (gemCells.GetLength(1) < position.y) return false;
           return gemCells[position.x, position.y] != -1;
        }


        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Bounds.center, Bounds.size);


        }



    }
}
