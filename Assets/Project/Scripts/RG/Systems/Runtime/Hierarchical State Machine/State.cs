using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RG.Systems.HSM
{
    public abstract class State
    {
        public readonly StateMachine Machine;
        public readonly State Parent;
        public State ActiveChild;

        public State(StateMachine machine, State parent = null)
        {
            Machine = machine;
            Parent = parent;
        }

        public virtual State GetInitialState() => null;
        public virtual State GetTransition() => null;

        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
        protected virtual void OnUpdate(float deltaTime) { }
        internal void Enter()
        {
            if (Parent != null) Parent.ActiveChild = this;
            OnEnter();
            State init = GetInitialState();
            if (init != null) init.Enter();
        }
        internal void Exit()
        {
            if (ActiveChild != null) ActiveChild.Exit();
            ActiveChild = null;
            OnExit();
        }
        internal void Update(float deltaTime)
        {
            State to = GetTransition();
            if (to != null)
            {
                Machine.Sequencer.RequenceTransition(this, to);
                OnUpdate(deltaTime);
                return;
            }
            if (ActiveChild != null) ActiveChild.Update(deltaTime);
            OnUpdate(deltaTime);
        }
        public State Leaf()
        {
            State s = this;
            while(s.ActiveChild != null) s = s.ActiveChild;
            return s;
        }
        public IEnumerable<State> PathToRoot()
        {
            for(State s = this; s != null; s = s.Parent) yield return s;
        }
    }
}
