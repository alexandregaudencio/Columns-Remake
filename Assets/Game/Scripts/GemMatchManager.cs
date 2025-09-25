using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Board
{
    //Encontra matches quando as células são preenchidas
    public class GemMatchManager : MonoBehaviour
    {
        private Board board;
        public event Action<List<Vector2Int[]>> Match;
        public event Action MatchFailed;

        public bool Logging = false;
        [SerializeField] private PiecesBoardController piecesController;
        private void Awake()
        {
            board = GetComponent<BoardController>().Board;
        }

        private void OnEnable()
        {
            piecesController.PiecesPlaced += OnPiecesPlaced;
        }


        private void OnDisable()
        {
            piecesController.PiecesPlaced -= OnPiecesPlaced;
        }

        private void OnPiecesPlaced(Dictionary<Vector2Int, Gem> positionGemPairs)
        {
            List<Vector2Int> positions = positionGemPairs.Keys.ToList();

            List<List<Vector2Int>> allMatches = new();
            if (HasMatch(positions, ref allMatches))
            {
                Match?.Invoke(allMatches.ToArrayList());
            }
            else
            {
                MatchFailed?.Invoke();
            }

        }


        public void LogMatch(List<List<Vector2Int>> allMatches)
        {
            Debug.Log("Total Matches: " + allMatches.Count);
            int i = 0;
            foreach (List<Vector2Int> matchList in allMatches)
            {
                i++;
                Debug.Log("match index: " + i);

                foreach (var pos in matchList)
                {
                    Debug.Log("match pos:" + pos);
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
                if (HasDiagonalUpperLeftMatch(position, out matches))
                {
                    allMatches.Add(matches);
                }
                if (HasDiagonalUpperRightMatch(position, out matches))
                {
                    allMatches.Add(matches);
                }
            }


            if (HasVerticalMatch(gemPositions, out matches))
            {
                allMatches.Add(matches);
            }

            return allMatches.Count > 0 ? true : false;
        }

        public bool HasHorizontalMatch(Vector2Int gemPosition, out List<Vector2Int> matches)
        {
            matches = new List<Vector2Int> { gemPosition };
            matches.AddRange(FindMatch(gemPosition, Vector2Int.left));
            matches.AddRange(FindMatch(gemPosition, Vector2Int.right));
            return matches.Count > 2 ? true : false;
        }

        public bool HasDiagonalUpperLeftMatch(Vector2Int gemPosition, out List<Vector2Int> matches)
        {
            matches = new List<Vector2Int> { gemPosition };
            matches.AddRange(FindMatch(gemPosition, Vector2Int.up.UpperLeft()));
            matches.AddRange(FindMatch(gemPosition, Vector2Int.up.LowerRight()));
            return matches.Count > 2 ? true : false;
        }
        public bool HasDiagonalUpperRightMatch(Vector2Int gemPosition, out List<Vector2Int> matches)
        {
            matches = new List<Vector2Int> { gemPosition };
            matches.AddRange(FindMatch(gemPosition, Vector2Int.up.UpperRight()));
            matches.AddRange(FindMatch(gemPosition, Vector2Int.up.LowerLeft()));
            return matches.Count > 2 ? true : false;
        }

        public bool HasVerticalMatch(List<Vector2Int> gems, out List<Vector2Int> matches)
        {
            //gem 1: gem middle (0,1)
            matches = new List<Vector2Int> { gems[1] };
            matches.AddRange(FindMatch(gems[1], Vector2Int.up));
            matches.AddRange(FindMatch(gems[1], Vector2Int.down));
            if (matches.Count < 2) matches.Clear();
            //gem 0: gem Lower (0,0)
            if (!matches.Contains(gems[0]))
            {
                List<Vector2Int> matchToDown = FindMatch(gems[0], Vector2Int.down);
                if (matchToDown.Count > 1) matches.Add(gems[0]);

            }

            //gem 2: gem Upper (0,2)
            if (!matches.Contains(gems[2]))
            {
                List<Vector2Int> matchToUp = FindMatch(gems[2], Vector2Int.up);
                if (matchToUp.Count > 1) matches.Add(gems[2]);
            }
            return matches.Count > 2 ? true : false;
        }

        public List<Vector2Int> FindMatch(Vector2Int gemPoisition, Vector2Int direction)
        {
            List<Vector2Int> matches = new List<Vector2Int>();
            int multiplier = 1;
            int cellGemIndex = board.GetGemIndex(gemPoisition);

            Vector2Int targetCell = gemPoisition + direction * multiplier;
            while (board.HasGem(targetCell))
            {
                if (cellGemIndex == board.GetGemIndex(targetCell))
                {
                    matches.Add(targetCell);
                    targetCell = gemPoisition + direction * ++multiplier;
                }
                else { break; }
            }

            return matches;
        }






    }
}
