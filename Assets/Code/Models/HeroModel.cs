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
        internal readonly float JumpStartSpeed = 7.0f;
        internal readonly float MovingEpsilon = 0.1f;
        internal readonly float FlyEpsilon = 0.1f;
        internal readonly float GroundLevel = -2f;
        internal readonly float Gravity = 9.8f;



        internal float YVelocity;
        internal bool IsGrounded;



    }
}
