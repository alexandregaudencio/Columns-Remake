using UnityEditor;
using UnityEngine;

namespace Game.Board.Gems
{

    [CustomEditor(typeof(GemSO))]
    public class GemEditor : Editor
    {
        GemSO gem;
        private void OnEnable()
        {
            gem = (GemSO)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (gem.GemData.Sprite == null) return;
            Texture2D texture = AssetPreview.GetAssetPreview(gem.GemData.Sprite);
            GUILayout.Label("", GUILayout.Height(100), GUILayout.Width(100));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }

    }
}
