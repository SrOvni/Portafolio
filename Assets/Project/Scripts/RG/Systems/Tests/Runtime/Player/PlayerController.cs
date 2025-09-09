using System;
using System.Collections.Generic;
using Codice.Client.BaseCommands.Merge.IncomingChanges;
using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace RG.Systems.Tests.Player
{
    [RequireComponent(typeof(GroundCheck), typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour, IPlayerControllerContext
    {
        public GameObject player => gameObject;
        [SerializeField] Animator animator;
        Rigidbody rigidbody;
        GroundCheck groundCheck;
        [SerializeField] InputReader input;
        StateMachine stateMachine;
        List<Timer> timers;

        [Header("Camera")]
        Transform mainCamera;
        CinemachineCamera _cinemachineCamera;

        [Header("Jump Settings")]
        [SerializeField, Min(0)] float _jumpDuration;
        [SerializeField, Min(0)] float _jumpCoolDownDuration;
        CountdownTimer jumpTimer;
        CountdownTimer jumpCooldownTimer;
        private float jumpVelocity;

        [Header("Movement")]
        Vector3 movement;
        public float CurrentSpeed { get; private set; } = 0;
        float velocity;
        [SerializeField] PlayerMovementSettings movementSettings;
        
        [Header("Rotation")]
        [SerializeField] float _rotationSpeed;

        [Header("Animation")]
        private float currentLocomotionSpeed;
        public float CurrentLocomotionSpeed { get => currentLocomotionSpeed; set => currentLocomotionSpeed = Mathf.Clamp(value, 0, 1);}
        [SerializeField] float smoothTime = 0.2f;
        static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            groundCheck = GetComponent<GroundCheck>();
            animator = GetComponent<Animator>();
            if (animator == null) Debug.Log("Animator is null");
            input.EnablePlayerInputActions();

            mainCamera = Camera.main.transform;

            rigidbody.freezeRotation = true;

            jumpTimer = new CountdownTimer(_jumpDuration);
            jumpCooldownTimer = new CountdownTimer(_jumpCoolDownDuration);

            jumpTimer.OnTimeStart += () => jumpVelocity = movementSettings.JumpForce;
            jumpTimer.OnTimeStop += () => jumpCooldownTimer.Start();

            stateMachine = new StateMachine();

            //Declare States
            var locomotionState = new LocomotionState(this, animator);
            var jumpState = new JumpState(this, animator);

            //Define Transitions
            At(locomotionState, jumpState, new FuncPredicate(() => jumpTimer.IsRunning));
            At(jumpState, locomotionState, new FuncPredicate(() => !jumpTimer.IsRunning && groundCheck.IsGrounded));

            stateMachine.SetInitialState(locomotionState);

        }

        void Update()
        {
            movement = new Vector3(input.Direction.x, 0, input.Direction.y);
            stateMachine.Update();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            animator.SetFloat(Speed, CurrentSpeed);
        }


        void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }

        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

        public void HandleMovement()
        {
            var adjustedDirection = Quaternion.AngleAxis(mainCamera.eulerAngles.y, Vector3.up) * movement;
            if (adjustedDirection.magnitude > 0f)
            {
                HandleRotation(adjustedDirection);
                HandleHorizontalMovement(adjustedDirection);
                SmoothLocomotionAnimSpeed(adjustedDirection.magnitude);
                SmoothSpeed(adjustedDirection.magnitude);
            }
            else
            {
                SmoothSpeed(0);
                rigidbody.linearVelocity = new Vector3(0, rigidbody.linearVelocity.y, 0);
            }

            
        }

        void SmoothSpeed(float value)
        {
            CurrentSpeed = Mathf.SmoothDamp(CurrentSpeed, value, ref velocity, smoothTime); 
        }

        private void HandleHorizontalMovement(Vector3 direction)
        {
            Vector3 velocity = direction * CurrentSpeed * Time.fixedDeltaTime;
            rigidbody.linearVelocity = new Vector3(velocity.x, rigidbody.linearVelocity.y, velocity.z);
        }

        private void SmoothLocomotionAnimSpeed(float magnitude)
        {
            CurrentLocomotionSpeed = Mathf.SmoothDamp(currentLocomotionSpeed, magnitude, ref velocity, smoothTime);
        }

        private void HandleRotation(Vector3 direction)
        {
            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
