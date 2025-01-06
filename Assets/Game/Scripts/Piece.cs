using DG.Tweening;
using ObjectPooling;
using UnityEngine;

namespace Game.Board
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Piece : PoolObject
    {

        private SpriteRenderer spriteRenderer;
        [SerializeField] private Gem gem;
        [SerializeField] private AnimationCurve matchBlinkCurve;
        [SerializeField] private float removeTime = 0.5f;
        //[field: SerializeField] public bool IsSpecial { get; private set; }

        public Vector2Int BoardPlacementPosition;
        public Board Board => BoardController.Instance.Board;


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Setup(Gem gem, Vector2Int position)
        {
            SetGem(gem);
            SetPosition(position);
        }

        private void OnEnable()
        {
            BoardController.Instance.Board.CellsCleaned += OnCellsCleaned;
            spriteRenderer.DOFade(1, 0);

        }

        public override void OnDisable()
        {
            base.OnDisable();
            BoardController.Instance.Board.CellsCleaned -= OnCellsCleaned;
        }
        private void OnCellsCleaned(Vector2Int[] matchList)
        {
            if (HasMatch(matchList))
            {
                RemovePiece();
            }

            if (HasMatchInThisCollumn(matchList))
            {
                DownPieceCount();
                MoveDirection(new Vector2Int(BoardPlacementPosition.x, DownPieceCount()), removeTime).SetDelay(removeTime);

            }

        }

        private bool HasMatchInThisCollumn(Vector2Int[] matchList)
        {
            foreach (var matchPosition in matchList)
            {
                //mesma columa mas linha diferente
                if (matchPosition.x == BoardPlacementPosition.x && matchPosition.y != BoardPlacementPosition.y)
                {
                    return true;
                }
            }
            return false;

        }

        private int DownPieceCount()
        {
            int voidCellsCount = 0;
            for (int i = 0; i < BoardPlacementPosition.y; i++)
            {
                Vector2Int targetCell = new Vector2Int(BoardPlacementPosition.x, i);
                if (!Board.HasGem(targetCell))
                {
                    Debug.Log(targetCell + " não tem gemas ");
                    voidCellsCount++;
                }
            }
            return voidCellsCount;
        }

        public void SetGem(Gem gem)
        {
            this.gem = gem;
            spriteRenderer.sprite = gem.Sprite;

        }



        public void SetPosition(Vector2 position)
        {
            BoardPlacementPosition = position.ToInt();
            transform.localPosition = position;
        }

        public void Move(Vector2 position)
        {
            transform.localPosition = position;
        }
        public Tween MoveDirection(Vector2Int direction, float duration)
        {
            return transform.DOLocalMove((Vector3Int)direction, duration, false).SetEase(Ease.InQuad);
        }


        private bool HasMatch(Vector2Int[] match)
        {
            foreach (Vector2Int cellPosition in match)
            {
                if (cellPosition == BoardPlacementPosition)
                {
                    return true;
                }
            }
            return false;
        }

        private void RemovePiece()
        {
            spriteRenderer.DOFade(0, removeTime).SetEase(matchBlinkCurve).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }


    }


}
