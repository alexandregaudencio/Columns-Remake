using Game.Board;
using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    //Acessa o quadro pra saber quando as celulas (cells da matriz foram modificadas)
    public class GemTilesController : MonoBehaviour
    {
        private BoardController boardController;
        [SerializeField] private Tilemap gemTilemap;
        private void Awake()
        {
            boardController = GetComponent<BoardController>();
        }






    }
}
