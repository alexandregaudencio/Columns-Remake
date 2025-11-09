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
        [SerializeField] private List<GemSO> gems;

        private List<GemSO> releasedGems = new();

        public GemSO GetGem(GemType gemType)
        {
            GemSO gem = Object.Instantiate(gems.Find(gem => gem.GemData.Type == gemType));
            releasedGems.Add(gem);
            return gem;
        }

        public GemSO GetGemRandomly()
        {
            int randomIndex = Random.Range(0, maxGemType);
            GemSO gem = Object.Instantiate(gems[randomIndex]);
            releasedGems.Add(gem);
            return gem;

        }




    }
}
