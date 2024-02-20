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
        public Bounds Bounds => new Bounds(transform.position, new Vector3(1, 3));
        public Vector3 PointUnderLeft => new Vector3(Bounds.min.x, Bounds.min.y);
        public Vector3 PointUnderRight => new Vector3(Bounds.max.x, Bounds.min.y);

        private Sequence sequence;

        private void Awake()
        {
            gemRenderes = GetComponentsInChildren<SpriteRenderer>().ToList();
        }

        public void SetupBlock(Sequence sequence)
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
