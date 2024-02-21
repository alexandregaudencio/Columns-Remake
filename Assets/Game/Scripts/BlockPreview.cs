using Game.Player;
using System;
using UnityEngine;
using Sequence = Game.Board.Gems.Sequence;

namespace Game.Board
{
    public class BlockPreview : MonoBehaviour
    {
        [SerializeField] private PlayerSessionProperties sessionProperties;
        [SerializeField] private SpriteRenderer[] renderes;

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
            SetPreview(sessionProperties.NextSequence);

        }

        private void Start()
        {
        }


        public void SetPreview(Sequence sequence)
        {
            renderes[0].sprite = sequence.Gem2.Sprite;
            renderes[1].sprite = sequence.Gem1.Sprite;
            renderes[2].sprite = sequence.Gem0.Sprite;
        }
    }
}
