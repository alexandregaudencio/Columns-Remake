
#if UNITY_EDITOR

using Game.Board;
using Game.Board.Gems;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        private void OnGUI()
        {
            if (!showShortcuts) return;
            y = 0;
            Print("ESC: Change to next block.");
            Print("SPACE: Switch gem.");
            Print("Arrows: Move block.");
            foreach (KeyValuePair<Vector2Int, Gem> pairs in BlockController.Instance.GemBlock.PositionGemPair)
            {
                Print(string.Concat(pairs.Key, " : ", pairs.Value.Type));
            }

            Print("Local Pos: "+BlockController.Instance.GemBlock.LocalPositionInt);

            Print("TIME SCALE: "+ Time.timeScale.ToString("F"));

            float timeScale = GUI.HorizontalSlider(new Rect(0, ymais20, 200, 20), Time.timeScale,0, 1);
            Time.timeScale = timeScale;

        }


        private void Print(string message)
        {
            GUI.Label(new Rect(0, ymais20, 300, 20), message);

        }


        

    }
}

#endif
