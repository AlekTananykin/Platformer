using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Configs
{
    [CreateAssetMenu(fileName ="platformConfig", 
        menuName ="Configs/CreatePlatformConfig")]
    public class PlatformsConfig: ScriptableObject
    {
        [Serializable]
        public class Brick
        {
            public Vector3 BrickPostioon;
            public Sprite BrickSprite;
        }

        public List<Brick> Bricks = new List<Brick>();
    }
}
