
#if UNITY_EDITOR

using UnityEngine;

namespace Game.Dev
{
    public class DevScript : MonoBehaviour
    {

        private void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 200, 20), "ESC: Change to next block.");
            GUI.Label(new Rect(0, 20, 200, 20), "SPACE: Switch gem.");
            GUI.Label(new Rect(0, 40, 200, 20), "Arrows: Move block.");

        }

    }
}

#endif
