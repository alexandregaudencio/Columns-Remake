using Game.Player;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Game.Board
{

    public enum BoardState
    {
        None,  //Outo of game
        BLOCK_DOWN, //block scrolling down
        CHECK, //checking if has match
        MATCH, //matching
        CLEAN_UP  //cleaning the board pieces in match
    }

    public class BoardController : MonoBehaviour
    {
        private ReactiveProperty<BoardState> _currentState = new(BoardState.None);

        [field: SerializeField] public Board Board { get; private set; }
        private GemMatchManager gemMatchManager;
        [field: SerializeField] public Tilemap gemTilemap { get; private set; }

        public static BoardController Instance { get; private set; }
        public IReadOnlyReactiveProperty<BoardState> CurrentState => _currentState;

        public Bounds Bounds => new Bounds(
            (transform.localPosition + new Vector3(Board.Size.x / 2, Board.Size.y / 2)),
            new Vector3(Board.Size.x, Board.Size.y));

        public PlayerSessionProperties PlayerSessionProperties;

        private void Awake()
        {
            Instance = this;
            gemMatchManager = GetComponent<GemMatchManager>();
            //board.gemCells = new int[Size.x, Size.y];

        }

        private void Start()
        {
            Board.ResetCells();

            _currentState
           .DistinctUntilChanged() // evita repetir a mesma mudança
           .Subscribe(OnStateChanged)
           .AddTo(this);

            ChangeState(BoardState.BLOCK_DOWN);


        }



        private void OnEnable()
        {
            gemMatchManager.Match += OnMatch;
            gemMatchManager.MatchFailed += OnMatchFailed;
            Board.CellsCleaned += OnCellsCleaned;
            PiecesBlockBehaviour.Instance.piecesBlockController.OnStoppedTimeExceeded += OnScrollingBlockFinished;

        }

        private void OnScrollingBlockFinished()
        {
            ChangeState(BoardState.CHECK);
        }


        private void OnDisable()
        {
            gemMatchManager.Match -= OnMatch;
            gemMatchManager.MatchFailed -= OnMatchFailed;
            Board.CellsCleaned -= OnCellsCleaned;
            PiecesBlockBehaviour.Instance.piecesBlockController.OnStoppedTimeExceeded -= OnScrollingBlockFinished;

        }

        private void OnStateChanged(BoardState newState)
        {
            Debug.Log($"Traca de estado: {newState}");

            switch (newState)
            {
                case BoardState.None:
                    break;
                case BoardState.BLOCK_DOWN:
                    PlayerSessionProperties.SetNextSequenceIndex();
                    break;
                case BoardState.CHECK:
                    SetGemBlockAuto();
                    //ChangeState(BoardState.CHECK);

                    break;
                case BoardState.CLEAN_UP:

                    break;

            }
        }

        ////em outra classe
        //BoardController.CurrentState
        //.Where(state => state == GameState.Playing)
        //.Subscribe(_ => Debug.Log("Entrou em modo Jogando"))
        //.AddTo(this);


        public void ChangeState(BoardState newState)
        {
            _currentState.Value = newState;
        }


        private void OnMatchFailed()
        {
            ChangeState(BoardState.BLOCK_DOWN);
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
            ChangeState(BoardState.MATCH);

            Vector2Int[] singleGemPosition = positions.SelectMany(x => x).Distinct().ToArray();

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
