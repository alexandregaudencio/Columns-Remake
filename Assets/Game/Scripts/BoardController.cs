using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Game.Board
{

    public class BoardController : MonoBehaviour
    {
        [field: SerializeField] public Board Board {  get;private set; }
        private GemMatchManager gemMatchManager;
        [field: SerializeField] public Tilemap gemTilemap { get; private set; }

        public static BoardController Instance { get; private set; }

        public Bounds Bounds => new Bounds(
            (transform.localPosition + new Vector3(Board.Size.x / 2, Board.Size.y / 2)),
            new Vector3(Board.Size.x, Board.Size.y));


        private void Awake()
        {
            Instance = this;
            gemMatchManager = GetComponent<GemMatchManager>();

            //board.gemCells = new int[Size.x, Size.y];

        }

        private void Start()
        {
           Board.ResetCells();

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
            Board.RemoveGems(singleGemPosition);

        }

        public Vector3 CellToLocal(Vector2Int cell)
        {
            return gemTilemap.LocalToCell((Vector3Int)cell);
        }



        public Vector3 GetStartBlockPosition()
        {
            return CellToLocal(Board.GemBlockInitialPosition);
        }


        public void SetGemBlockAuto()
        {
           Board.SetGemsInCells(BlockController.Instance.GemBlock.PositionGemPair);
        }





        public void OnDrawGizmosSelected()
        {
            Gizmos.color = UnityEngine.Color.red;
            Gizmos.DrawWireCube(Bounds.center, Bounds.size);


        }





    }
}
