using UnityEngine;
using UnityEngine.InputSystem;
using RG.Systems.Input;
using static RG.Systems.Tests.Player.PlayerInputActions;
using UnityEngine.Events;
using System.Collections;
using System;

namespace RG.Systems.Tests.Player
{
    [RequireComponent(typeof(PlayerInput)), CreateAssetMenu(fileName = "Input reader")]
    public class PlayerInputReader : BaseInputReaderScriptableObject, IPlayerActions
    {
        public event UnityAction Attack = delegate {};
        public event UnityAction<bool> Crouch = delegate { };
        public event UnityAction<bool> Jump = delegate { };
        public event UnityAction<bool> Run = delegate { };
        public event UnityAction<Vector2, bool> Look = delegate { };
        public event UnityAction<Vector2> Move = delegate { };
        public PlayerInputActions inputActions;
        public override Vector2 Direction => inputActions.Player.Move.ReadValue<Vector2>();
        public Vector2 LookDirection => inputActions.Player.Look.ReadValue<Vector2>();

        public override void EnablePlayerActions()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerInputActions();
                inputActions.Player.SetCallbacks(this);
            }
            inputActions.Enable();
        }

        public bool IsJumpKeyPressed() => inputActions.Player.Jump.IsPressed();

        public void OnAttack(InputAction.CallbackContext context)
        {
            Attack?.Invoke();
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            InvokeActionWith(context, Crouch);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            InvokeActionWith(context, Jump);
        }

        private void InvokeActionWith(InputAction.CallbackContext context, UnityAction<bool> action)
        {
            var actionPhase = context.phase switch
            {
                InputActionPhase.Started => true,
                InputActionPhase.Canceled => false,
                _ => default
            };
            action?.Invoke(actionPhase);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            Look?.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
        }

        bool IsDeviceMouse(InputAction.CallbackContext context)
        {
            return context.control.device is Mouse;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            Run?.Invoke(context.ReadValueAsButton());
        }
    }
}
