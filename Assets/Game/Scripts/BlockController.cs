using Game.Board.Gems;
using Game.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Board
{
    /// <summary>
    /// CONTROLA UM BLOCO DE GEMAS QUE VAI CAINDO NO JOGO.
    /// </summary>
    [RequireComponent(typeof(GemBlock))]
    public class BlockController : MonoBehaviour
    {
        public GemBlock GemBlock { get; private set; }
        [SerializeField] private float forceDownSpeed = 10;
        [SerializeField][Min(0.1f)] float descentSpeed = 0.1f;

        [SerializeField] private PlayerSessionProperties sessionProperties;
        public static BlockController Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            GemBlock = GetComponent<GemBlock>();
        }
        private void Start()
        {
            ResetPosition();
        }

        private void OnEnable()
        {
            sessionProperties.SequenceIndexUpdate += OnSequenceIndexUpdate;
            
        }

        private void OnDisable()
        {
            sessionProperties.SequenceIndexUpdate -= OnSequenceIndexUpdate;
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
                GemBlock.SwitchSequence();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
               BoardController.Instance.SetGemsAuto();
                sessionProperties.SetNextSequenceIndex();
            }


        }



        private void OnSequenceIndexUpdate(int _)
        {
            GemBlock.SetupBlock(sessionProperties.CurrentSequence);
            ResetPosition();

        }

        public bool IsValidMovement(Vector2 direction)
        {
            if (GemBlock.PointUnderLeft.x + direction.x < BoardController.Instance.Bounds.min.x) return false;
            if (GemBlock.PointUnderRight.x + direction.x > BoardController.Instance.Bounds.max.x) return false;
            if (GemBlock.PointUnderRight.y + direction.y < BoardController.Instance.Bounds.min.y) return false;
            return true;

        }

        public bool IsTargetCellEmpty(Vector2 direction)
        {
            List<Vector2Int> gemPosition = GemBlock.PositionGemPair.Keys.ToList();
            foreach (Vector2Int position in gemPosition)
            {
                if (BoardController.Instance.HasGem(position + direction.ToInt())) return false;
            }

            //pra evitar que haja movimentos quando tem pedras acima.
            //Vector2 positionAboveGem2 = gemPosition[0] + Vector2Int.up;
            //if (BoardController.Instance.HasGem(positionAboveGem2.ToInt()))
            //{
            //    return false;
            //}

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
            if (GemBlock == null) return;
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(GemBlock.PointUnderLeft, GemBlock.PointUnderRight);
        }



    }

}
