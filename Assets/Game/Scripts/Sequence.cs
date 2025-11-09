using System;
using UnityEngine;

namespace Game.Board.Gems
{
    /// <summary>
    /// SEQUÊNCIA DE 3 GEMAS.
    /// USADAS PARA CONFIGURAR UM BLOCO.
    /// </summary>
    [Serializable]
    public class Sequence
    {

        [SerializeField] private GemSO gem2;
        [SerializeField] private GemSO gem1;
        [SerializeField] private GemSO gem0;

        /// <summary>
        /// GEMA DE CIMA
        /// </summary>
        public GemSO Gem2 { get => gem2; set => gem2 = value; }
        /// <summary>
        /// GEMA DO MEIO
        /// </summary>
        public GemSO Gem1 { get => gem1; set => gem1 = value; }
        /// <summary>
        /// GEMA DE BAIXO
        /// </summary>
        public GemSO Gem0 { get => gem0; set => gem0 = value; }
        public event Action GemChange;
        public void SwitchGems()
        {
            GemSO lastGemDown = gem0;
            gem0 = Gem1;
            Gem1 = gem2;
            gem2 = lastGemDown;
            GemChange?.Invoke();

        }
    }
}
