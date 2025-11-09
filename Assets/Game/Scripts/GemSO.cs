using System;
using UnityEngine;

namespace Game.Board
{
    [System.Serializable]
    [CreateAssetMenu()]
    public class GemSO : ScriptableObject
    {
        [field: SerializeField] public Gem GemData { get; private set; }

    }

    [Serializable]
    public class Gem
    {
        [field: SerializeField] public GemType Type { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }

        public int Index => (int)Type;
    }
}

