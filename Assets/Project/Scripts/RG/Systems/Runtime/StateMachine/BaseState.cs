using UnityEngine;
namespace RG.Systems
{
    public abstract class BaseState : IState
    {
        protected IPlayerControllerContext controller;
        protected Animator animator;

        public BaseState(IPlayerControllerContext ctx, Animator anim)
        {
            controller = ctx;
            animator = anim;
        }

        public virtual void FixedUpdate()
        {
            //noop
        }

        public virtual void OnEnter()
        {
            //noop
        }

        public virtual void OnExit()
        {
            //noop
        }

        public virtual void Update()
        {
            //noop
        }
    }

}