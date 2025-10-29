using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine;
using ImprovedTimers;
using System;

namespace RG.Systems.Tests.Player
{
    [RequireComponent(typeof(GroundCheck), typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour, IPlayerControllerContext, ISaveable
    {
        public GameObject player => gameObject;
        [SerializeField] Animator animator;
        new Rigidbody rigidbody;
        GroundCheck groundCheck;
        [SerializeField] InputReader input;
        StateMachine stateMachine;
        List<Timer> timers;

        [Header("Camera")]
        Transform mainCamera;

        [Header("Jump Settings")]
        [SerializeField, Min(0)] float _jumpDuration;
        [SerializeField, Min(0)] float _jumpCoolDownDuration;
        CountdownTimer jumpTimer;
        CountdownTimer jumpCooldownTimer;
        private float jumpVelocity;
        [Header("Run")]
        StopwatchTimer RunStopWatch;
        [Header("Jump")]
        public bool IsGrounded { get; private set; }
        private Ray groundRay;
        [SerializeField] private float sphereRadius;
        [SerializeField] float maxDistance;
        [SerializeField] LayerMask groundLayer;

        [Header("Movement")]
        Vector3 movement;
        public float CurrentSpeed { get; set; }
        float velocity;
        [SerializeField] PlayerMovementData movementData;
        public PlayerMovementData MovementData => movementData;

        [Header("Rotation")]
        [SerializeField] float _rotationSpeed;

        [Header("Animation")]
        private float currentLocomotionSpeed;
        public float CurrentLocomotionSpeed { get => currentLocomotionSpeed; set => currentLocomotionSpeed = Mathf.Clamp(value, 0, 1); }
        float locomotionAnimationSpeed;

        [Header("Stun state")]
        CountdownTimer stunTimer = new(1);
        #region Saveable
        public string SaveID => typeof(PlayerController).ToString();

        [SerializeField] PlayerStats stats = new();

        public JsonData SavedData
        {
            get
            {
                JsonData data = new JsonData();
                return data;
            }
        }


        [SerializeField] float smoothTime = 0.1f;
        private RunState runState;
        public RunState RunState { get => runState; set => runState = value; }
        static readonly int SpeedHashValue = Animator.StringToHash("Speed");

        #endregion

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

            jumpTimer.OnTimerStart += () => jumpVelocity = movementData.JumpForce;
            jumpTimer.OnTimerStop += () => jumpCooldownTimer.Start();

            //RUnTimer
            RunStopWatch = new();

            //Stun timer


            stateMachine = new StateMachine();

            //Declare States

            var locomotionState = new LocomotionState(this, animator);
            var jumpState = new JumpState(this, animator);
            var stunState = new StunState(this, animator);
            runState = new RunState(this, animator);

            //Define Transitions
            At(locomotionState, jumpState, new FuncPredicate(() => jumpTimer.IsRunning));
            At(locomotionState, stunState, new FuncPredicate(() => stunTimer.IsRunning));
            At(locomotionState, runState, new FuncPredicate(() => RunStopWatch.IsRunning));
            At(runState, locomotionState, new FuncPredicate(() => !RunStopWatch.IsRunning));
            At(jumpState, stunState, new FuncPredicate(() => stunTimer.IsRunning));

            Any(locomotionState, new FuncPredicate(() => ReturnToLocomotionState()));


            stateMachine.SetInitialState(locomotionState);

            CurrentSpeed = stats.baseData.WalkSpeed;

            var numbers = Enumerable.Range(0, 10).Except(new[] { 2, 8 });

        }

        private bool ReturnToLocomotionState()
        {
            return groundCheck.IsGrounded
            && !jumpTimer.IsRunning
            && !stunTimer.IsRunning
            ;
        }

        void Update()
        {
            IsGrounded = Physics.SphereCast(groundRay, sphereRadius, maxDistance, groundLayer);
            movement = new Vector3(input.Direction.x, 0, input.Direction.y);
            // Debug.Log("X: " + input.Direction.x + ", Y: " + input.Direction.y);
            stateMachine.Update();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            animator.SetFloat(SpeedHashValue, locomotionAnimationSpeed);//Falta quitar current speed para que sea de un valor de 0 a
        }


        void FixedUpdate()
        {
            stateMachine.FixedUpdate();
            HandleJump();
        }

        private void HandleJump()
        {
            if(jumpTimer.IsRunning && IsGrounded)
            {
                rigidbody.AddForce(transform.up * jumpVelocity, ForceMode.Force);
            }
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
            // Debug.Log("Animation speed: " + locomotionAnimationSpeed);
            locomotionAnimationSpeed = Mathf.SmoothDamp(locomotionAnimationSpeed, value, ref velocity, smoothTime);
        }

        private void HandleHorizontalMovement(Vector3 direction)
        {
            Vector3 velocity = direction * CurrentSpeed * Time.fixedDeltaTime;
            rigidbody.linearVelocity = new Vector3(velocity.x * locomotionAnimationSpeed, rigidbody.linearVelocity.y, velocity.z * locomotionAnimationSpeed);
        }
        private void HandleRotation(Vector3 direction)
        {
            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        public void LoadFromData(JsonData data)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeToStunState()
        {
            throw new System.NotImplementedException();
        }
        private void OnEnable()
        {
            input.Run += OnRun;
        }

        private void OnRun(bool running)
        {
            if (running)
            {
                Debug.Log("Sprint pressed");
                RunStopWatch.Start();
            }
            else if (!running)
            {
                Debug.Log("Sprint canceled");
                RunStopWatch.Stop();
            }
        }
    }

    interface IChangeState
    {
        void ChangeToStunState();
    }
}
