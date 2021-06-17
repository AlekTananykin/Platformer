using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Models
{
    internal sealed class HeroModel
    {
        internal readonly float AnimationSpeed = 10.0f;
        internal readonly float WalkSpeed = 3.0f;
        internal readonly float RunSpeed = 10.0f;
        internal readonly float JumpForce = 30000.0f;
        internal readonly float JumpThreshuld = 0.1f;
        internal readonly float FlyThreshuld = 1f;
        internal readonly float MovingEpsilon = 0.1f;
        internal readonly float FlyEpsilon = 0.1f;

    }
}
