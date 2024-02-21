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

        [SerializeField] private Gem gem2;
        [SerializeField] private Gem gem1;
        [SerializeField] private Gem gem0;

        /// <summary>
        /// GEMA DE CIMA
        /// </summary>
        public Gem Gem2 { get => gem2; set => gem2 = value; }
        /// <summary>
        /// GEMA DO MEIO
        /// </summary>
        public Gem Gem1 { get => gem1; set => gem1 = value; }
        /// <summary>
        /// GEMA DE BAIXO
        /// </summary>
        public Gem Gem0 { get => gem0; set => gem0 = value; }
        public event Action GemChange;
        public void SwitchGems()
        {
            Gem lastGemDown = gem0;
            gem0 = Gem1;
            Gem1 = gem2;
            gem2 = lastGemDown;
            GemChange?.Invoke();

        }
    }
}
