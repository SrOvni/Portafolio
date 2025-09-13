using System;
using UnityEngine;
namespace RG.Systems
{
    public abstract class BaseState : IState
    {
        protected IStateContext _ctx;
        protected Animator animator;

        protected const float CROSS_FADE_DURATION = 0.1f;

        public BaseState(IStateContext ctx, Animator anim)
        {
            _ctx = ctx;
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