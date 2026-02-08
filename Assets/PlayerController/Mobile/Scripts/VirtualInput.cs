using UnityEngine;

namespace PlayerController
{
    public class VirtualInput : MonoBehaviour
    {
        [Header("Output")] public PlayerInputActions PlayerInputs;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            PlayerInputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            PlayerInputs.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            PlayerInputs.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            PlayerInputs.SprintInput(virtualSprintState);
        }
    }
}