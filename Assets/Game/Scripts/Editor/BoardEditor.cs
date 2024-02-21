
using UnityEditor;
using UnityEngine;

namespace Game.Board
{
    [CustomEditor(typeof(BoardController))]
    public class BoardEditor : Editor
    {
        BoardController board;

        private void OnEnable()
        {
            board = (BoardController)target;
        }
        private void OnDisable()
        {
            board = null;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (board.gemCells == null) return;


            string indexes = "";
            for (int j = 0; j < board.gemCells.GetLength(1); j++)
            {
                string aux = "";
                for (int i = 0; i < board.gemCells.GetLength(0); i++)
                {

                    aux = string.Concat(aux, board.gemCells[i, j], "   ");
                }
                indexes = string.Concat(aux, "\n", indexes);

            }
            GUILayout.Label("\n Gem Indexes Throughout array2D", GUILayout.Height(40), GUILayout.Width(300));

            GUILayout.Label(indexes, GUILayout.Height(200), GUILayout.Width(350));
        }
    }
}
