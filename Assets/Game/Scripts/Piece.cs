using Game.Board.Gems;
using ObjectPooling;
using UnityEngine;

namespace Game.Board
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Piece : PoolObject
    {
        private SpriteRenderer spriteRenderer;
        [SerializeField] private Gem gem;
        //[field: SerializeField] public bool IsSpecial { get; private set; }


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Setup(Gem gem, Vector2Int position)
        {
            SetGem(gem);
            SetPosition(position);
        }


        public void SetGem(Gem gem)
        {
            this.gem = gem;
            spriteRenderer.sprite = gem.Sprite;

        }



        public void SetPosition(Vector2 position)
        {
            
            transform.localPosition = position;
        }

        public void Move(Vector2 position)
        {
            transform.localPosition = position;
        }






    }


}
