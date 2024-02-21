using Game.Board.Gems;
using Game.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Board
{
    [RequireComponent(typeof(GemBlock))]
    public class BlockController: MonoBehaviour
    {
        public GemBlock GemBlock { get; private set; }
        [SerializeField] private float forceDownSpeed = 10;
        [SerializeField] [Min(0.1f)] float descentSpeed = 0.1f;

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
            Debug.Log(HasFreePositionOnBoard(positionDown));

            if (HasFreePositionOnBoard(positionDown))
            {
                Move(positionDown);

            }

        }

        public void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(Vector2.left);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(Vector2.right);

            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Move(Vector2.down * forceDownSpeed * Time.deltaTime);
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                GemBlock.SwitchSequence();
            }







            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                foreach(KeyValuePair<Vector2Int, Gem> positionGemPair in GemBlock.PositionGemPair)
                {
                    BoardController.Instance.SetGemTile(positionGemPair.Key, positionGemPair.Value);
                }
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
            if(GemBlock.PointUnderRight.y + direction.y < BoardController.Instance.Bounds.min.y) return false;
            return true;

        }

        public bool HasFreePositionOnBoard(Vector2 positionDown)
        {
            Vector2 targetPositionDown = (Vector2)GemBlock.PointUnderRight + positionDown;


            if (targetPositionDown.y < BoardController.Instance.Bounds.min.y) return false;


            //Vector2Int gem2downPosition = GemBlock.GetPositionGemPair(2).Key + new Vector2Int((int)positionDown.x, Mathf.FloorToInt(positionDown.y));
            //if(BoardController.Instance.HasGem(gem2downPosition)) return false;

            return true;


        }


        public void ResetPosition()
        {
            transform.localPosition = BoardController.Instance.GetStartBlockPosition();
        }



        public void Move(Vector2 direction)
        {
            if (!IsValidMovement(direction)) return;
            transform.position += (Vector3)direction;
        }


        private void OnDrawGizmos()
        {
            if (GemBlock == null) return;
            Gizmos.color = Color.cyan ;
            Gizmos.DrawLine(GemBlock.PointUnderLeft, GemBlock.PointUnderRight);
        }



    }

}
