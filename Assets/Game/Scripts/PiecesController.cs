using ObjectPooling;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Board

{
    //Acessa o quadro pra saber quando as celulas (cells da matriz foram modificadas)
    public class PiecesController : MonoBehaviour
    {
        private Board board;
        [SerializeField] private Tilemap gemTilemap;
        [SerializeField] private GameObject piecePrefab;

        private static ObjectPool<PoolObject> objectPool;
        private List<PoolObject> objectsActive = new List<PoolObject>();


        private void Awake()
        {
            board = GetComponent<BoardController>().Board;
        }

        private void OnEnable()
        {
            objectPool = new ObjectPool<PoolObject>(piecePrefab);

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
                //gemTilemap.SetTile((Vector3Int)pair.Key, pair.Value.TileBase);
                Piece piece = GetPeace((Vector2)pair.Key);


                piece.Setup(pair.Value, pair.Key);

            }


        }


        private void OnCellsCleaned(List<Vector2Int> positions)
        {

            foreach (var position in positions)
            {
                Debug.Log("cleaned: " + position.ToString());
                gemTilemap.SetTile((Vector3Int)position, null);
            }


        }



        public Piece GetPeace(Vector3 position, Quaternion rotation = default)
        {
            PoolObject poolObject;

            GameObject pieceGameObject = objectPool.PullGameObject(position, rotation, out poolObject);
            poolObject.returnToPool += (t) => { objectsActive.Remove(poolObject); };
            
            objectsActive.Add(poolObject);

            pieceGameObject.transform.SetParent(transform);
            Piece piece = poolObject.GetComponent<Piece>();
            return piece;

        }

    }
}

