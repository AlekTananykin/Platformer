using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Configs
{

    [CreateAssetMenu(fileName = "AIConfog",
        menuName = "Configs/AIConfig")]
    public class AIConfig : ScriptableObject
    {
        internal readonly Vector2 speed;
        internal float minSqrDistanceToTarget;
    }
}
