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

        [SerializeField] Health health;
        

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

        public void Heal(int amount)
        {
            health.Heal(amount);
        }
    }

}
