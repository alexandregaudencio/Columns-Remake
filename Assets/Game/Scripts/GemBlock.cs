using UnityEngine;
using Game.Board.Gems;
using System.Collections.Generic;
using System;

namespace Game.Board
{
    /// <summary>
    /// UM BLOCO DE 3 GEMAS
    /// </summary>
    public class GemBlock : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> gemRenderes;
        public Bounds Bounds => new Bounds(transform.position+Vector3.up, new Vector3(1, 3));
        public Vector3 PointUnderLeft => new Vector3(Bounds.min.x, Bounds.min.y);
        public Vector3 PointUnderRight => new Vector3(Bounds.max.x, Bounds.min.y);

        private Sequence sequence;
        //public Vector2Int PositionInt => new Vector2Int(transform.localPosition.x, transform.localPosition.y);
        public Vector2Int LocalPositionInt => new Vector2Int((int)transform.localPosition.x, (int)transform.localPosition.y);
        public Dictionary<Vector2Int, Gem> PositionGemPair => new()
        {
            {transform.localPosition.ToCell()+Vector2Int.up*2   ,sequence.Gem2 },
            {transform.localPosition.ToCell()+Vector2Int.up ,sequence.Gem1 },
            {transform.localPosition.ToCell()+Vector2Int.zero ,sequence.Gem0 }

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
