using UnityEngine;
using Game.Board.Gems;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Game.Board
{
    public class GemBlock : MonoBehaviour
    {
        private List<SpriteRenderer> gemRenderes;
        public Bounds Bounds => new Bounds(transform.position+Vector3.up, new Vector3(1, 3));
        public Vector3 PointUnderLeft => new Vector3(Bounds.min.x, Bounds.min.y);
        public Vector3 PointUnderRight => new Vector3(Bounds.max.x, Bounds.min.y);

        private Gems.Sequence sequence;

        public Dictionary<Vector2Int, Gem> PositionGemPair => new()
        {
            {transform.position.ToCell()+Vector2Int.up*2   ,sequence.GemUp },
            {transform.position.ToCell()+Vector2Int.up ,sequence.GemMiddle },
            {transform.position.ToCell()+Vector2Int.zero ,sequence.GemDown }

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




        private void Awake()
        {
            gemRenderes = GetComponentsInChildren<SpriteRenderer>().ToList();
        }

        public void SetupBlock(Gems.Sequence sequence)
        {
            this.sequence = sequence;
            UpdateSprites();

        }

        private void UpdateSprites()
        {
            gemRenderes[0].sprite = sequence.GemUp.Sprite;
            gemRenderes[1].sprite = sequence.GemMiddle.Sprite;
            gemRenderes[2].sprite = sequence.GemDown.Sprite;
        }

        public void SwitchSequence()
        {
            sequence.SwitchGems();
            UpdateSprites();
        }



    }
}
