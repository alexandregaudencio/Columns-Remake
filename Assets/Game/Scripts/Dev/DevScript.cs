
#if UNITY_EDITOR

using Game.Board;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dev
{
    public class DevScript : MonoBehaviour
    {

        public bool showShortcuts = true;

        int y = 0;
        public int ymais20
        {
            get
            {
                return y += 20;
            }
        }

        public bool showCellsValue = true;

        private void OnGUI()
        {
            if (!showShortcuts) return;
            y = 0;
            GUILayout.BeginVertical();
            GUILayout.Label("UP arrow: Set Gems in grid cells. SPACE: Switch gem.");
            GUILayout.Label("Arrows: Move block.");
            foreach (KeyValuePair<Vector2Int, Gem> pairs in PiecesBlockBehaviour.Instance.GemBlock.PositionGemPair)
            {
                GUILayout.Label(string.Concat(pairs.Key, " : ", pairs.Value.Type));
            }

            GUILayout.Label("Block local pos: " + PiecesBlockBehaviour.Instance.GemBlock.transform.localPosition);
            GUILayout.Label("Block pos Int: " + PiecesBlockBehaviour.Instance.GemBlock.LocalPositionInt);

            GUILayout.Label("");
            GUILayout.Label("TIME SCALE: " + Time.timeScale.ToString("F"));

            float timeScale = GUI.HorizontalSlider(new Rect(0, ymais20, 200, 20), Time.timeScale, 0, 1);
            Time.timeScale = timeScale;
            if (PiecesBlockBehaviour.Instance.GemBlock.RemainingStoppedTime >= 0)
                GUILayout.Label("Time in cell: " + PiecesBlockBehaviour.Instance.GemBlock.RemainingStoppedTime.ToString("F"));


            showCellsValue = GUILayout.Toggle(showCellsValue, "Show Cells");
            if (showCellsValue) PrintGemCellsDebug();

            GUILayout.EndVertical();

        }



        private void PrintGemCellsDebug()
        {
            var gemCells = BoardController.Instance.Board.gemCells;
            string gemCellIndexesDebug = "";
            for (int j = 0; j < gemCells.GetLength(1); j++)
            {
                string aux = "";
                for (int i = 0; i < gemCells.GetLength(0); i++)
                {
                    string nextIndex = gemCells[i, j] == -1 ? "__" : " " + gemCells[i, j] + " ";
                    aux = string.Concat(aux, nextIndex, " ");
                }
                gemCellIndexesDebug = string.Concat(aux, "\n", gemCellIndexesDebug);

            }
            GUILayout.Label(gemCellIndexesDebug);
        }


    }
}

#endif
