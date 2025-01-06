using UnityEngine;
using Game.Board.Gems;
using System.Collections.Generic;
using System;

namespace Game.Board
{
    /// <summary>
    /// UM BLOCO DE 3 GEMAS
    /// </summary>
    public class PiecesBlockController : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> gemRenderes;
        public Bounds Bounds => new Bounds(transform.position + Vector3.up, new Vector3(1, 3));
        public Vector3 PointUnderLeft => new Vector3(Bounds.min.x, Bounds.min.y);
        public Vector3 PointUnderRight => new Vector3(Bounds.max.x, Bounds.min.y);

        private Sequence sequence;

        public Vector2Int LocalPositionInt => new Vector2Int((int)transform.localPosition.x, (int)transform.localPosition.y);
        public float previousYLocalPosition { get; private set; } = 0;

        private float stoppedTime = 0;
        [SerializeField][Min(0.1f)] private float idleTimeLimit = 1f;
        public bool StoppedTimeOver => stoppedTime > idleTimeLimit;
        public float RemainingStoppedTime => idleTimeLimit - stoppedTime;
        //trocar por PIECE (peças relativas ao game ojbects, com posição na celula do board, gema, relative position ao block e etc).
        public Dictionary<Vector2Int, Gem> PositionGemPair => new()
        {
            {transform.localPosition.AsCell()+Vector2Int.zero ,sequence.Gem0 },
            {transform.localPosition.AsCell()+Vector2Int.up   ,sequence.Gem1 },
            {transform.localPosition.AsCell()+Vector2Int.up*2 ,sequence.Gem2 }

        };

        public KeyValuePair<Vector2Int, Gem> GetPositionGemPair(int index)
        {
            int i = 0;
            foreach (KeyValuePair<Vector2Int, Gem> pair in PositionGemPair)
            {
                if (i == index) return pair;
                i++;
            }
            throw new ArgumentOutOfRangeException("index out of range. try index beetween 0 to 2");

        }


        private void Update()
        {
            UpdateStoppedTimeLogic();

        }

        public void UpdateStoppedTimeLogic()
        {
            if (transform.localPosition.y == previousYLocalPosition)
            {
                stoppedTime += Time.deltaTime;
            }
            else
            {
                previousYLocalPosition = transform.localPosition.y;
                stoppedTime = 0;
            }
        }




        public void SetupBlock(Gems.Sequence sequence)
        {
            this.sequence = sequence;
            UpdateSprites();

        }

        private void UpdateSprites()
        {
            gemRenderes[0].sprite = sequence.Gem2.Sprite;
            gemRenderes[1].sprite = sequence.Gem1.Sprite;
            gemRenderes[2].sprite = sequence.Gem0.Sprite;
        }

        public void SwitchSequence()
        {
            sequence.SwitchGems();
            UpdateSprites();
        }



    }
}
