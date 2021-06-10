﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Animation
{
    public enum Track
    {
        walk,
        jump
    }

    [CreateAssetMenu (fileName ="SpriteAnimationconfig", 
        menuName ="Configs/SpriteAnimationconfig")]
    public class SpriteAnimationConfig: ScriptableObject
    {
        [Serializable]
        public class SpriteSequence
        {
            public Track Track;
            public List<Sprite> Sprites = new List<Sprite>();
        }

        public List<SpriteSequence> Sequences = new List<SpriteSequence>();
    }
}
