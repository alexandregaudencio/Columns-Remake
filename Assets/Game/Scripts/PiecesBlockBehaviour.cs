using UnityEngine;

namespace Game.Board
{
    /// <summary>
    /// CONTROLA UM BLOCO DE GEMAS QUE VAI CAINDO NO JOGO.
    /// </summary>
    [RequireComponent(typeof(PiecesBlockController))]
    public class PiecesBlockBehaviour : MonoBehaviour
    {
        public PiecesBlockController piecesBlockController { get; private set; }
        [SerializeField] private float forceDownSpeed = 10;
        [SerializeField][Min(0.1f)] float descentSpeed = 0.1f;


        public static PiecesBlockBehaviour Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
            //privsório
            piecesBlockController = GetComponent<PiecesBlockController>();

        }
        private void Start()
        {
            ResetPosition();
        }






        private void FixedUpdate()
        {
            Vector2 positionDown = Vector2.down * descentSpeed * Time.fixedDeltaTime;
            if (IsTargetCellEmpty(positionDown))
            {
                TryMove(positionDown);

            }

        }

        public void Update()
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                TryMove(Vector2.left);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                TryMove(Vector2.right);

            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                TryMove(Vector2.down * forceDownSpeed * Time.deltaTime);
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                piecesBlockController.SwitchSequence();
            }



        }



        public bool IsValidMovement(Vector2 direction)
        {
            if (piecesBlockController.PointUnderLeft.x + direction.x < BoardController.Instance.Bounds.min.x) return false;
            if (piecesBlockController.PointUnderRight.x + direction.x > BoardController.Instance.Bounds.max.x) return false;
            if (piecesBlockController.PointUnderRight.y + direction.y < BoardController.Instance.Bounds.min.y) return false;
            return true;

        }

        public bool IsTargetCellEmpty(Vector2 direction)
        {
            //List<Vector2Int> gemPosition = GemBlock.PositionGemPair.Keys.ToList();
            //foreach (Vector2Int position in gemPosition)
            //{
            //    Vector2 targetPosition = (position + direction).ToInt();
            //    if (BoardController.Instance.HasGem(targetPosition.ToInt())) return false;
            //}

            if (BoardController.Instance.Board.HasGem((Vector2Int)(transform.localPosition + (Vector3)direction).ToInt())) return false;


            return true;


        }

        public void ResetPosition()
        {
            transform.localPosition = BoardController.Instance.GetStartBlockPosition();
        }


        public void TryMove(Vector2 direction)
        {
            if (!IsValidMovement(direction)) return;
            if (!IsTargetCellEmpty(direction)) return;
            transform.position += (Vector3)direction;
        }


        private void OnDrawGizmos()
        {
            if (piecesBlockController == null) return;
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(piecesBlockController.PointUnderLeft, piecesBlockController.PointUnderRight);
        }



        //private void OnGUI()
        //{

        //    if (Selection.activeGameObject != gameObject) return;
        //    Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        //    GUI.Label(new Rect(screenPoint, Vector2.one * 300), GemBlock.GetPositionGemPair(0).Key.ToString());

        //}


    }


}
