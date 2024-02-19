using System;
using UnityEngine;

namespace  Game.Board.Gems
{
    [System.Serializable ]
    [CreateAssetMenu()]
    public class Gem : ScriptableObject
    {
        
        [field: SerializeField] public GemType Type { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }

        public int GemIndex => (int)Type;


    }

}

