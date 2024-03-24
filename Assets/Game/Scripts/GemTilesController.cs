using Game.Board.Gems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Board

{
    //Acessa o quadro pra saber quando as celulas (cells da matriz foram modificadas)
    public class GemTilesController : MonoBehaviour
    {
        private Board board;
        [SerializeField] private Tilemap gemTilemap;
        private void Awake()
        {
            board = GetComponent<BoardController>().Board;
        }

        private void OnEnable()
        {
            board.CellsCleaned += OnCellsCleaned;
            board.Cellsfilled += OnCellsFilled;

        }

        private void OnDisable()
        {
            board.CellsCleaned -= OnCellsCleaned;
            board.Cellsfilled -= OnCellsFilled;
        }

        private void OnCellsFilled(Dictionary<Vector2Int, Gem> gemPositionPairs)
        {

            foreach (var pair in gemPositionPairs)
            {
                gemTilemap.SetTile((Vector3Int)pair.Key, pair.Value.TileBase);

            }


        }

        private void OnCellsCleaned(List<Vector2Int> positions)
        {
            foreach (var position in positions)
            {

                gemTilemap.SetTile((Vector3Int)position, null);
            }


        }


    }
}
