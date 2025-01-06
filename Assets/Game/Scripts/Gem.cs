using UnityEngine;

namespace Game.Board
{
    [System.Serializable]
    [CreateAssetMenu()]
    public class Gem : ScriptableObject
    {

        [field: SerializeField] public GemType Type { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }

        public int Index => (int)Type;


    }

}

