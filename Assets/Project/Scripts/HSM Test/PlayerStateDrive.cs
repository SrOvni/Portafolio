using System;
using System.Linq;
using UnityEngine;

namespace RG.Systems.HSM
{
    public class PlayerStateDrive : MonoBehaviour
    {
        public PlayerContext _ctx = new();
        [SerializeField] private bool _isGrounded;
        public bool IsGrounded { get => _isGrounded; set => _isGrounded = _ctx.CharacterController.isGrounded; }
        CharacterController _characterController;
        Animator _animator;
        StateMachine _stateMachine;
        State _root;
        [SerializeField] private bool drawGizmos = false;
        string _lastPath;

        void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _ctx.CharacterController = _characterController;
            _animator = GetComponent<Animator>();
            _ctx.Anim = _animator;

            //Machine initialization
        }
        void OnDrawGizmosSelected()
        {
            if (!drawGizmos || _characterController == null) return;

            Gizmos.color = Color.white;
            // Gizmos.DrawSphere()
        }

        public static string StatePath(State s)
        {
            return string.Join("> ", s.PathToRoot().Reverse().Select(n => n.GetType().Name));//Remplazar con ZLinq
        }
        void Update()
        {
            _stateMachine.Tick(Time.deltaTime);

            var path = StatePath(_stateMachine.Root.Leaf());
            if (_lastPath != path)
            {
                Debug.Log(string.Format("State", path));
                _lastPath = path;
            }
        }

    }
}
