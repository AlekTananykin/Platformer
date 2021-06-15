using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Configs
{
    [CreateAssetMenu(fileName ="CannonConfig", 
        menuName="Configs/CreateCannonConfig")]
    public class CannonConfig: ScriptableObject
    {
        public Sprite Carriage;
        public Vector3 CarriagePivot;
        public Sprite Mazzle;
        public Vector3 MazzlePivot;
        public Sprite Bullet;

    }
}
