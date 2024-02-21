
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

            float blockY = (BlockController.Instance.GemBlock.PointUnderLeft.y + BlockController.Instance.GemBlock.PointUnderRight.y) / 2;
            Print("blocky: " + blockY.ToString());
            Print("blocky floor: " + Mathf.FloorToInt(blockY).ToString());
            Print(BlockController.Instance.GemBlock.GetPositionGemPair(2).ToString());

        }


        private void Print(string message)
        {
            GUI.Label(new Rect(0, ymais20, 200, 20), message);

        }


    }
}

#endif
