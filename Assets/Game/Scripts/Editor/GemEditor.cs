using UnityEditor;
using UnityEngine;

namespace Game.Board.Gems
{

    [ CustomEditor(typeof(Gem))]
    public class GemEditor : Editor
    {
        Gem gem;
        private void OnEnable()
        {
            gem = (Gem)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(gem.Sprite == null) return;
            Texture2D texture = AssetPreview.GetAssetPreview(gem.Sprite);
            GUILayout.Label("", GUILayout.Height(100), GUILayout.Width(100));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }

    }
}
