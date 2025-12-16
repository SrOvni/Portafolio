using System;
using Codice.CM.Common;
using ImprovedTimers;
using RG.Systems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDownGame
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : StateContext
    {
        StateMachine _stateMachine;
        Animator _animator;
        CharacterController _characterController;
        PlayerInput _playerInput;
        

        #region States
            private IdleState _idleState;
            private WalkState _walkState;
        #endregion
        #region Timers
            private CountdownTimer _dodgeTimer;
        #endregion
        [Header("Dodge")]
        [SerializeField] private float _dodgeDuration;
        [SerializeField] private bool _isGrounded;

        public Vector2 MovementDirection { get; private set; }
        public IMovementDirectionProvider movementDirectionProvider;

        void Awake()
        {
            if(!TryGetComponentInChildren(out _animator))Debug.Log($"No Animator component attached to {gameObject.name}");
            if(TryGetComponent(out _characterController))Debug.Log($"No ChartacterController attached to {gameObject.name}");
            if(TryGetComponent(out _playerInput))Debug.Log($"No Player Input attached to {gameObject.name}");
            
            movementDirectionProvider = new CameraRelativeMovement(Camera.main.transform, () => MovementDirection);
            _stateMachine = new();

            _idleState = new(this, _animator);
            _walkState = new(this, _animator);

            At(_idleState, _walkState, new(() => MovementDirection.magnitude > 0 && _characterController.isGrounded));
            
            //Timers
            _dodgeTimer = new(_dodgeDuration);


            _stateMachine.SetInitialState(_idleState);
        }
        void Update()
        {
            _isGrounded = _characterController.isGrounded;
            _stateMachine.Update();
            HandleRotation();
            _characterController.Move(movementDirectionProvider.GetMovementDirection() * 10 * Time.deltaTime); //Quitar velocidad harcodeada
        }
        void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }
        void OnEnable()
        {
            _playerInput.actions["Move"].started += OnMovePressed;
            _playerInput.actions["Move"].performed += OnMovementPerformed;
            _playerInput.actions["Move"].canceled += OnMovementCanceled;
            _playerInput.actions["DodgeAttack"].started += OnDodgeAttackPressed;
        }

        private void OnMovementCanceled(InputAction.CallbackContext context)
        {
            MovementDirection = Vector2.zero;
        }

        void OnDisable()
        {
            _playerInput.actions["Move"].started -= OnMovePressed;
            _playerInput.actions["Move"].performed -= OnMovementPerformed;
            _playerInput.actions["Move"].canceled -= OnMovementCanceled;
            _playerInput.actions["DodgeAttack"].started -= OnDodgeAttackPressed;
        }
        void HandleRotation()
    {
        Vector3 movementDirection = CurrentMovementDirection();
        if (movementDirection.magnitude < 0.1f)
            return;
        Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
        var value = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.fixedDeltaTime * 800);//Quitar velocidad de rotación hardcodeada
        // Debug.Log(value);
        transform.rotation = value;

    }

        public virtual Vector3 CurrentMovementDirection()
        {
            return CameraRelativeDirection();
        }

        public Vector3 CameraRelativeDirection()
    {

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 directionToMove = ((forward * MovementDirection.y) + (right * MovementDirection.x)).normalized;

        return directionToMove;
    }

        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            MovementDirection = context.ReadValue<Vector2>();
        }

        private void OnMovePressed(InputAction.CallbackContext context)
        {
            MovementDirection = context.ReadValue<Vector2>();
            Debug.Log("MovementPressed");
        }

        private void OnDodgeAttackPressed(InputAction.CallbackContext context)
        {
            
        }

        public void At(IState from, IState to, FuncPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        public void Any(IState to, FuncPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
        private bool TryGetComponentInChildren<T>(out T TParam)where T : Component
        {
            TParam = GetComponentInChildren<T>();
            if(TParam != null)return true;
            else return false;
            
        }

    }
    public abstract class HierarchicalStates : BaseState
    {
        public abstract bool IsRootState { get;}
        protected HierarchicalStates(StateContext ctx, Animator anim) : base(ctx, anim)
        {
            
        }
    }
    public class GroundState : HierarchicalStates
    {
        private bool _isRootState;

        public GroundState(StateContext ctx, Animator anim) : base(ctx, anim)
        {
            _isRootState = true;
        }

        public override bool IsRootState => _isRootState;
    }

    public class IdleState : HierarchicalStates
    {
        public override bool IsRootState => _isRootState;
        private readonly StateContext Ctx;
        private readonly string IDLE_HASH = "Idle";
        private bool _isRootState;

        public IdleState(StateContext ctx, Animator anim) : base(ctx, anim)
        {
            Ctx = ctx;
            _isRootState = false;
        }
        public override void OnEnter()
        {
            animator.CrossFade(IDLE_HASH, CROSS_FADE_DURATION);
            Debug.Log("On idle state");
        }
    }
    public class WalkState : BaseState
    {
        private readonly StateContext Ctx;
        private readonly string WALK_HASH = "Walk";
        public WalkState(StateContext ctx, Animator anim) : base(ctx, anim)
        {
            Ctx = ctx;
        }
        public override void OnEnter()
        {
            animator.CrossFade(WALK_HASH, CROSS_FADE_DURATION);
            Debug.Log("On Walk State");
        }
    }
    public class StateContext : MonoBehaviour, IStateContext
    {
        public static readonly StateContext Empty;
        public StateContext(){}

        #region Context
            
        #endregion
    }

    class CharacterLocomotion
    {
        private readonly ICollisionResolver _collisionResolver;
        public readonly Capsule _capsule;
        public Vector3 Position { get; private set; }
        public CharacterLocomotion(ICollisionResolver collisionResolver, Capsule capsule, Vector3 startPosition)
        {
            _collisionResolver = collisionResolver;
            _capsule = capsule;
            Position = startPosition;
        }

        public void Move(Vector3 velocity)
        {
            Vector3 displacement = _collisionResolver.Resolver(Position, velocity, _capsule);

            Position += displacement;
        }

    }
    public class CollideAndSliderSolver : ICollisionResolver
    {
        private const int MaxIterations = 5;
        private const float SkinWidth = 0.01f;

        public Vector3 Resolver(Vector3 position, Vector3 velocity, Capsule capsule)
        {
            Vector3 remaining = velocity;
            Vector3 currentPosition = position;

            for (int i = 0; i < MaxIterations; i++)
            {
                if (remaining.sqrMagnitude < 0.0001f)
                    break;
                capsule.GetContactPoint(currentPosition, out Vector3 bottom, out Vector3 top);
                if(Physics.CapsuleCast(bottom, top, capsule.Radius, remaining.normalized, out RaycastHit hit, remaining.magnitude + SkinWidth))
                {
                    float distance = Mathf.Max(hit.distance - SkinWidth, 0);
                    Vector3 moveToHit = remaining.normalized * distance;
                    currentPosition += moveToHit;
                    Vector3 normal = hit.normal;
                    remaining -= moveToHit;
                    
                    remaining = Vector3.ProjectOnPlane(remaining, normal);
                }
                else
                {
                    currentPosition += remaining;
                    break;
                }

            }
            return currentPosition - position;
        }
    }
    public interface ICollisionResolver
    {
        public Vector3 Resolver(Vector3 position, Vector3 velocity, Capsule collider);
    }
    public struct Capsule
    {
        public float Radius;
        public float Height;
        Vector3 Center;
        public Capsule(float radius, float height, Vector3 center)
        {
            Radius = radius;
            Height = height;
            Center = center;
        }

        public void GetContactPoint(Vector3 position, out Vector3 bottom, out Vector3 top)
        {
            float halfHeight = Mathf.Max(0, Height * .5f - Radius);
            Vector3 up = Vector3.up * halfHeight;

            bottom = position + Center - up;
            top = position + Center + up;
        }
    }
    public interface IMovementDirectionProvider
    {
        public Vector3 GetMovementDirection();
    }

    public class CameraRelativeMovement : IMovementDirectionProvider
    {
        private readonly Transform _camera;
        private readonly Func<Vector2> _input;

        public CameraRelativeMovement(Transform camera, Func<Vector2> input)
        {
            _camera = camera;
            _input = input;
        }
        public Vector3 GetMovementDirection()
        {
            Vector2 input = _input();
            Vector3 forward = _camera.forward;
            Vector3 right = _camera.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            return forward * input.y + right * input.x;
        }
    }

}
