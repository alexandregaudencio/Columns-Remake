using UnityEngine;

namespace Game.Board.Gems
{
    [System.Serializable]
    public class Sequence
    {

        [SerializeField] private Gem gemUp;
        [SerializeField] private Gem gemMiddle;
        [SerializeField] private Gem gemDown;

        public Gem GemUp { get => gemUp; set => gemUp = value; }
        public Gem GemMiddle { get => gemMiddle; set => gemMiddle = value; }
        public Gem GemDown { get => gemDown; set => gemDown = value; }
    }
}
