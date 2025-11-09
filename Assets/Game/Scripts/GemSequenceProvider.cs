using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Board.Gems
{
    //provê sequencia de 3 gemas;
    [Serializable]
    public class GemSequenceProvider
    {
        [SerializeField] private GemProvider GemProvider;
        [SerializeField] private List<Sequence> sequencesReleased = new();

        public void Initialize()
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
            sequence.Gem2 = GemProvider.GetGemRandomly();
            sequence.Gem1 = GemProvider.GetGemRandomly();
            sequence.Gem0 = GemProvider.GetGemRandomly();
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



