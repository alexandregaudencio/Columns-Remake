using Game.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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



            //TODO: cleanup this code
            if(GemBlock.TimeStoppedInCell > 1)
            {
                BoardController.Instance.SetGemsAuto();
                sessionProperties.SetNextSequenceIndex();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
               BoardController.Instance.SetGemsAuto();
               sessionProperties.SetNextSequenceIndex();
            }
            //


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
            //List<Vector2Int> gemPosition = GemBlock.PositionGemPair.Keys.ToList();
            //foreach (Vector2Int position in gemPosition)
            //{
            //    Vector2 targetPosition = (position + direction).ToInt();
            //    if (BoardController.Instance.HasGem(targetPosition.ToInt())) return false;
            //}

            if (BoardController.Instance.HasGem((Vector2Int)(transform.localPosition + (Vector3)direction).ToInt())) return false;


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



        //private void OnGUI()
        //{

        //    if (Selection.activeGameObject != gameObject) return;
        //    Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        //    GUI.Label(new Rect(screenPoint, Vector2.one * 300), GemBlock.GetPositionGemPair(0).Key.ToString());
            
        //}


    }


}
