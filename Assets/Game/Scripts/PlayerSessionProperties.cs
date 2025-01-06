using Game.Board.Gems;
using System;
using UnityEngine;

namespace Game.Player
{
    public class PlayerSessionProperties : MonoBehaviour
    {

        [SerializeField] private int currentSequenceIndex = -1;
        public event Action<int> SequenceIndexUpdate;
        public Sequence CurrentSequence
        {
            get
            {
                return GemSequenceProvider.Instance.FindSequence(currentSequenceIndex);
            }
        }

        public Sequence NextSequence
        {
            get
            {
                return GemSequenceProvider.Instance.FindSequence(currentSequenceIndex + 1);
            }
        }


        private void Start()
        {
            SetNextSequenceIndex();
        }


        public void SetNextSequenceIndex()
        {
            currentSequenceIndex++;
            SequenceIndexUpdate?.Invoke(currentSequenceIndex);
        }



    }
}
