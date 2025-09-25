using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Game.Board
{

    public class BoardController : MonoBehaviour
    {
        [field: SerializeField] public Board Board { get; private set; }
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
            Board.CellsCleaned += OnCellsCleaned;

        }

        private void OnDisable()
        {
            gemMatchManager.Match -= OnMatch;
            Board.CellsCleaned -= OnCellsCleaned;
        }

        private void OnCellsCleaned(Vector2Int[] matchList)
        {
            foreach (Vector2Int matchPosition in matchList)
            {
                int y = matchPosition.y;

                while (Board.HasGem(new Vector2Int(matchPosition.x, y + 1)))
                {
                    Vector2Int currentGemPosition = new Vector2Int(matchPosition.x, y);
                    Vector2Int upGemPosition = new Vector2Int(matchPosition.x, y + 1);
                    Board.SetGemInCell(currentGemPosition, Board.GetGemIndex(upGemPosition));
                    Board.SetGemInCell(upGemPosition, -1);
                    y++;
                }
            }

        }




        private void OnMatch(List<Vector2Int[]> positions)
        {

            Vector2Int[] singleGemPosition = positions.SelectMany(x => x).Distinct().ToArray();
            Debug.Log("match count: " + singleGemPosition.Length);
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


        //posiciona novas gemas no bloco de peças que caem
        public void SetGemBlockAuto()
        {
            Board.SetGemsInCells(PiecesBlockBehaviour.Instance.piecesBlockController.PositionGemPair);
        }





        public void OnDrawGizmosSelected()
        {
            Gizmos.color = UnityEngine.Color.red;
            Gizmos.DrawWireCube(Bounds.center, Bounds.size);


        }





    }
}
