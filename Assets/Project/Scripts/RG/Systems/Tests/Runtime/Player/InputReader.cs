using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static RG.Systems.Tests.Player.PlayerInputActionsTest;
namespace RG.Systems.Tests.Player
{
    [CreateAssetMenu(fileName = "Input Reader", menuName = "RG.Tests/Input Reader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public event Action Attack = delegate { };
        public event Action<bool> Crouch = delegate { };
        public event Action<bool> Jump = delegate { };
        public event Action<bool> Run = delegate { };
        public event Action<Vector2, bool> Look = delegate { };
        public event Action<Vector2> Move = delegate { };
        public Vector2 Direction => _inputActions.Player.Move.ReadValue<Vector2>();
        PlayerInputActionsTest _inputActions;

        public void EnablePlayerInputActions()
        {
            if (_inputActions == null)
            {
                _inputActions = new PlayerInputActionsTest();
                _inputActions.Player.SetCallbacks(this);
            }
            _inputActions.Enable();
        }
        void OnDisable()
        {
            if (_inputActions != null)
            {
                _inputActions.Disable();
            }
        }

        public Vector2 LookDirection => _inputActions.Player.Look.ReadValue<Vector2>();
        public void OnAttack(InputAction.CallbackContext context)
        {
            //
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            //
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            //
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            //
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            //
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            //
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            //
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Started)
            {
                Run.Invoke(true);
            }else if(context.phase == InputActionPhase.Canceled)
            {
                Run.Invoke(false);
            }
        }

        public void OnZoom(InputAction.CallbackContext context)
        {
            //
        }
    }
}
