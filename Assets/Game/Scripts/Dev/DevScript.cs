
#if UNITY_EDITOR

using Game.Board;
using Game.Board.Gems;
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

            Print("Cel: " + BlockController.Instance.transform.position.ToCell());
            Print("Pos: "+BlockController.Instance.transform.position.ToString());
            Print("Local Pos: "+BlockController.Instance.transform.localPosition.ToString());

        }


        private void Print(string message)
        {
            GUI.Label(new Rect(0, ymais20, 300, 20), message);

        }


    }
}

#endif
