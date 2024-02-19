using UnityEngine;
using UnityEngine.Tilemaps;


namespace Game.Board
{
    public class BoardController : MonoBehaviour
    {

        [field: SerializeField] public Vector2Int Size { get; private set; } = new Vector2Int(7, 13);
        public Grid grid;
        private Tilemap tilemap;
        public static BoardController Instance { get; private set; }


        private void Awake()
        {
            Instance = this;

            grid = GetComponent<Grid>();
            tilemap = GetComponentInChildren<Tilemap>();


        }


    }
}
