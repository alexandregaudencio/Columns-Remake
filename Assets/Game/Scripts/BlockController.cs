using UnityEngine;

namespace Game.Board
{
    public class BlockController: MonoBehaviour
    {


        [SerializeField] private GemBlock gemBlock;


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
                gemBlock.Move(Vector2.down * 10 * Time.deltaTime);
            }

        }

    }
}
