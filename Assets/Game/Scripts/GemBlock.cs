using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game.Board.Gems;
using Game.Player;


namespace Game.Board
{
    public class GemBlock : MonoBehaviour
    {
        [SerializeField] private PlayerSessionProperties sessionProperties;

        //[SerializeField] private GemProvider gemProvider;
        private List<SpriteRenderer> gemRenderes;
        public float descentSpeed = 0.1f;

        private void Awake()
        {
            gemRenderes = GetComponentsInChildren<SpriteRenderer>().ToList();
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
            SetupBlock(sessionProperties.CurrentSequence);
            ResetPosition();

        }

        private void FixedUpdate()
        {
            Move(Vector2.down * descentSpeed * Time.fixedDeltaTime);

        }

        public void SetupBlock(Sequence sequence)
        {

            gemRenderes[0].sprite = sequence.GemUp.Sprite;
            gemRenderes[1].sprite = sequence.GemMiddle.Sprite;
            gemRenderes[2].sprite = sequence.GemDown.Sprite;

        }

        public void ResetPosition()
        {
            transform.position = new Vector3(BoardController.Instance.Size.x / 2, BoardController.Instance.Size.y);
        }



        public void Move(Vector2 direction)
        {
            //Vector3 targetPosition = transform.position + (Vector3)direction;
            transform.position += (Vector3)direction;
        }

        

    }





}
