using System.Collections.Generic;
using UnityEngine;

namespace Game.Board.Gems
{
    //provê sequencia de 3 gemas;
    public class GemSequenceProvider : MonoBehaviour
    {
        [SerializeField] private List<Sequence> sequencesReleased = new();

        public static GemSequenceProvider Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            sequencesReleased.Clear();
            GenerateNewSequences();

        }

        private void GenerateNewSequences()
        {
            for (int i = 0; i < 10; i++)
            {
                sequencesReleased.Add(GenerateSequence());
            }
        }

        private Sequence GenerateSequence()
        {
            Sequence sequence = new Sequence();
            sequence.Gem2 = GemProvider.Instance.GetGemRandomly();
            sequence.Gem1 = GemProvider.Instance.GetGemRandomly();
            sequence.Gem0 = GemProvider.Instance.GetGemRandomly();
            return sequence;


        }

        public Sequence FindSequence(int index)
        {
            if (index >= sequencesReleased.Count - 1)
            {
                GenerateNewSequences();
            }
            return sequencesReleased[index];
        }


    }


  
}



