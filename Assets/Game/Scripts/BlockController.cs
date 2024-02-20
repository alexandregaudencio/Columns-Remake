using Game.Player;
using UnityEngine;

namespace Game.Board
{
    public class BlockController: MonoBehaviour
    {
        [SerializeField] private GemBlock gemBlock;
        [SerializeField] private float forceDownSpeed = 10;
        [SerializeField] [Min(0.1f)] float descentSpeed = 0.1f;

        [SerializeField] private PlayerSessionProperties sessionProperties;

        private void FixedUpdate()
        {
            Move(Vector2.down * descentSpeed * Time.fixedDeltaTime);

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
                gemBlock.SwitchSequence();
            }

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


        private void OnSequenceIndexUpdate(int _)
        {
            gemBlock.SetupBlock(sessionProperties.CurrentSequence);
            ResetPosition();

        }

        
        public bool IsValidMoviment(Vector2 direction)
        {

            if (gemBlock.PointUnderLeft.x + direction.x < BoardController.Instance.Bounds.min.x) return false;
            if (gemBlock.PointUnderRight.x + direction.x > BoardController.Instance.Bounds.max.x) return false;
            if(gemBlock.PointUnderRight.y + direction.y < BoardController.Instance.Bounds.min.y) return false;
            return true;

        }


        public void ResetPosition()
        {
            transform.position = new Vector3(BoardController.Instance.Size.x / 2, BoardController.Instance.Size.y);
        }



        public void Move(Vector2 direction)
        {
            if (!IsValidMoviment(direction)) return;
            transform.position += (Vector3)direction;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;


            Gizmos.DrawLine(gemBlock.PointUnderLeft, gemBlock.PointUnderRight);

        }

        private void OnGUI()
        {
            bool a = gemBlock.Bounds.Intersects(BoardController.Instance.Bounds);
            GUI.Label(new Rect(0, 0, 200, 100), "board intersect block: " + a);
            GUI.Label(new Rect(0, 20, 200, 100), "is valid movement: " + IsValidMoviment(Vector3.zero));


        }

    }

}
