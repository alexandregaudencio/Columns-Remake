using Game.Board.Gems;
using System;
using UnityEngine;

namespace Game.Player
{
    public class PlayerSessionProperties : MonoBehaviour
    {

        private int currentSequenceIndex = 0;
        [SerializeField] private GemSequenceProvider gemSequenceProvider;
        public event Action<int> SequenceIndexUpdate;

        public Sequence CurrentSequence
        {
            get
            {
                return gemSequenceProvider.FindSequence(currentSequenceIndex);
            }
        }

        public Sequence NextSequence
        {
            get
            {
                return gemSequenceProvider.FindSequence(currentSequenceIndex + 1);
            }
        }

        private void Awake()
        {
            gemSequenceProvider.Initialize();
        }
        private void Start()
        {
            SequenceIndexUpdate?.Invoke(currentSequenceIndex);
        }


        public void SetNextSequenceIndex()
        {
            currentSequenceIndex++;
            SequenceIndexUpdate?.Invoke(currentSequenceIndex);
        }



    }
}
