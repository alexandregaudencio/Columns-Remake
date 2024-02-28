using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Game.Board
{


    public class GemMatchManager : MonoBehaviour
    {
        private BoardController boardController;

        private void Awake()
        {
            boardController = GetComponent<BoardController>();
        }

        private void OnEnable()
        {
            boardController.Cellsfilled += OnCellsFilled;
        }


        private void OnDisable()
        {
            boardController.Cellsfilled -= OnCellsFilled;
        }

        private void OnCellsFilled(List<Vector2Int> gemPositions)
        {
            List<List<Vector2Int>> allMatches = new();
            if (HasMatch(gemPositions, ref allMatches))
            {
                foreach (var match in allMatches)
                {
                    foreach(var gemPosition in match)
                    {
                        Debug.Log(gemPosition);

                    }
                }
            }
        }


        public bool HasMatch(List<Vector2Int> gemPositions, ref List<List<Vector2Int>> allMatches)
        {
            List<Vector2Int> matches = new();

            foreach (var position in gemPositions)
            {
                if (HasHorizontalMatch(position, out matches))
                {
                    allMatches.Add(matches);
                }
                // VerticalMatch
                // DiagonalUpperleft
                // DiagonalUpperRight



            }

            return matches.Count > 2 ? true : false;
        }

        public bool HasHorizontalMatch(Vector2Int gemPosition, out List<Vector2Int> matches)
        {
            matches = new List<Vector2Int> { gemPosition };
            matches.AddRange(FindMatch(gemPosition, Vector2Int.left));
            matches.AddRange(FindMatch(gemPosition, Vector2Int.right));
            Debug.Log("horizontal: " + matches.Count);
            return matches.Count > 2 ? true : false;
        }

        public List<Vector2Int> FindMatch(Vector2Int gemPoisition, Vector2Int direction)
        {
            List<Vector2Int> matches = new List<Vector2Int>();
            int multiplier = 1;
            int cellGemIndex = boardController.GetGemIndex(gemPoisition);

            Vector2Int targetCell = gemPoisition + direction * multiplier;
            while (boardController.HasGem(targetCell))
            {
                if (cellGemIndex == boardController.GetGemIndex(targetCell))
                {
                    matches.Add(targetCell);
                    targetCell = gemPoisition + direction * ++multiplier;

                }
                else
                {
                    break;
                }

            }

            return matches;
        }






    }
}
