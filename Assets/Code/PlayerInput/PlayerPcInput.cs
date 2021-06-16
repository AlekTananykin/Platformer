using UnityEngine;

namespace Assets.Code.PlayerInput
{
    sealed class PlayerPcInput : IPlayerInput
    {
        public float MoveX => Input.GetAxis("Horizontal");
        public bool IsRun => 
            Input.GetKeyDown(KeyCode.LeftShift) 
            || Input.GetKeyDown(KeyCode.RightShift);

        public bool IsJump => Input.GetAxis("Vertical") > 0.0f;
        public bool IsAttack => Input.GetKeyDown(KeyCode.Space);
    }
}
