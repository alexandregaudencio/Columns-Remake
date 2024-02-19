using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Board.Gems
{
    //provê gems a pedido
    public class GemProvider : MonoBehaviour
    {
        [SerializeField] [Range(1,9)] private int maxGemType = 6;
        [SerializeField] private List<Gem> gems;

        private List<Gem> releasedGems = new();

        public static GemProvider Instance { get; private set; }
        public void Awake()
        {
            Instance = this;
        }

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
