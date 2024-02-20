using UnityEngine;

namespace Game.Board
{
    public class BlockController: MonoBehaviour
    {
        [SerializeField] private GemBlock gemBlock;
        [SerializeField] private float forceDownSpeed = 10;


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gemBlock.Move(Vector2.left);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gemBlock.Move(Vector2.right);

            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                gemBlock.Move(Vector2.down * forceDownSpeed * Time.deltaTime);
            }

        }

        //public bool IsValidMoviment(Vector2 direction)
        //{
        //    //if()
        //}

    }

}
