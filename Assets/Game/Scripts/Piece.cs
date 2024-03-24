using Game.Board.Gems;
using UnityEngine;

namespace Game.Board
{
    public class Piece : MonoBehaviour
    {
        [SerializeField] private Gem gem;
        [field: SerializeField] public bool IsSpecial { get; private set; }

        public void SetGem(Gem gem)
        {
            this.gem = gem;
        }


        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void Move(Vector2 position)
        {
            transform.position = position;
        }






    }


}
