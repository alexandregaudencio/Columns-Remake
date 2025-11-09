using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Game.Board.Gems
{
    //provê gems a pedido
    [Serializable]
    public class GemProvider
    {
        [SerializeField][Range(1, 9)] private int maxGemType = 6;
        [SerializeField] private List<Gem> gems;

        private List<Gem> releasedGems = new();

        public Gem GetGem(GemType gemType)
        {
            Gem gem = Object.Instantiate(gems.Find(gem => gem.Type == gemType));
            releasedGems.Add(gem);
            return gem;
        }

        public Gem GetGemRandomly()
        {
            int randomIndex = Random.Range(0, maxGemType);
            Gem gem = Object.Instantiate(gems[randomIndex]);
            releasedGems.Add(gem);
            return gem;

        }




    }
}
