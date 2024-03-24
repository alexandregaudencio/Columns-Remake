using Game.Board.Gems;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Board
{

    [Serializable]
    public class Board
    {
        [field: SerializeField] public Vector2Int Size { get; private set; } = new Vector2Int(7, 13);
        public int[,] gemCells;

        public event Action<Dictionary<Vector2Int, Gem>> Cellsfilled;
        //public event Action<Vector2Int> CellCleaned;
        public event Action<List<Vector2Int>> CellsCleaned;
        public Vector2Int GemBlockInitialPosition => new Vector2Int(Size.x / 2, (int)Size.y);

        public Board()
        {
            gemCells = new int[Size.x, Size.y];

        }



        #region Validation
        public bool IsValidCell(Vector2Int position)
        {
            return (position.x >= 0 && position.y >= 0 && position.x < Size.x && position.y < Size.y);
        }

        public bool HasGem(Vector2Int position)
        {
            if (IsValidCell(position))
                return gemCells[position.x, position.y] != -1;
            return false;
        }
        #endregion


        #region Getters
        public int GetGemIndex(Vector2Int position)
        {
            return gemCells[position.x, position.y];
        }

        #endregion


        #region setters
        public void SetGemInCell(Vector2Int position, Gem gem)
        {
            if (!IsValidCell(position))
                throw new ArgumentOutOfRangeException(string.Concat(position, " is not valid cell position."));
            gemCells[position.x, position.y] = gem.Index;

        }

        public void SetGemsInCells(Dictionary<Vector2Int, Gem> positionGemPairs)
        {
            foreach (KeyValuePair<Vector2Int, Gem> positionGemPair in positionGemPairs)
            {
                SetGemInCell(positionGemPair.Key, positionGemPair.Value);
            }
            //List<Vector2Int> cells = new List<Vector2Int>(positionGemPairs.Keys);
            Cellsfilled?.Invoke(positionGemPairs);

        }
        #endregion


        #region remove
        public void ResetCells()
        {
            for (int i = 0; i < gemCells.GetLength(0); i++)
            {
                for (int j = 0; j < gemCells.GetLength(1); j++)
                {
                    gemCells[i, j] = -1;
                }
            }

        }

        public void RemoveGem(Vector2Int position)
        {
            if (!IsValidCell(position)) throw new ArgumentOutOfRangeException(string.Concat(position, " is not valid cell position."));
            gemCells[position.x, position.y] = -1;

        }

        public void RemoveGems(List<Vector2Int> positions)
        {
            foreach (var position in positions)
            {
                RemoveGem(position);
            }
            CellsCleaned?.Invoke(positions);
        }

        #endregion



    }
}

