using Game.Board.Gems;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Game.Board
{
    public class BoardController : MonoBehaviour
    {

        [field: SerializeField] public Vector2Int Size { get; private set; } = new Vector2Int(7, 13);
        [SerializeField] private Tilemap gemTilemap;
        public int[,] gemCells;
        public static BoardController Instance { get; private set; }
        public Bounds Bounds => new Bounds(
            (transform.position + new Vector3(Size.x / 2, Size.y / 2)), 
            new Vector3(Size.x, Size.y));

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

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Bounds.center, Bounds.size);


        }



    }
}
