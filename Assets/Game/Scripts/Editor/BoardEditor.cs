using UnityEditor;
using UnityEngine;

namespace Game.Board
{
    [CustomEditor(typeof(Board))]
    public class BoardEditor : Editor
    {
        BoardController boardController;

        private void OnEnable()
        {
            boardController = (BoardController)target;
        }
        private void OnDisable()
        {
            boardController = null;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (boardController.Board.gemCells == null) return;


            string indexes = "";
            for (int j = 0; j < boardController.Board.gemCells.GetLength(1); j++)
            {
                string aux = "";
                for (int i = 0; i < boardController.Board.gemCells.GetLength(0); i++)
                {

                    aux = string.Concat(aux, boardController.Board.gemCells[i, j], "   ");
                }
                indexes = string.Concat(aux, "\n", indexes);

            }
            GUILayout.Label("\n Gem Indexes Throughout array2D", GUILayout.Height(40), GUILayout.Width(300));

            GUILayout.Label(indexes, GUILayout.Height(200), GUILayout.Width(350));
        }
    }
}
