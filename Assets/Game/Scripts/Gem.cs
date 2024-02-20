using System;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace  Game.Board.Gems
{
    [System.Serializable ]
    [CreateAssetMenu()]
    public class Gem : ScriptableObject
    {
        
        [field: SerializeField] public GemType Type { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public TileBase TileBase { get; private set; }

        public int Index => (int)Type;


    }

}

