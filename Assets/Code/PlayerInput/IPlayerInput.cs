using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.PlayerInput
{
    interface IPlayerInput
    {
        float MoveX { get; }
        bool IsRun { get; }
        bool IsJump { get; }
        bool IsAttack { get; }
    }
}
