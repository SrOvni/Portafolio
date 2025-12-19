using System;
using System.Collections.Generic;
using UnityEngine;

namespace RG.Systems.HSM
{
    public class TransitionSequencer
    {
        public readonly StateMachine Machine;

        ISequencer sequencer;               //Current phase
        event Action nextPhase;
        (State from, State to)? pending;
        State lastFrom, lastTo;

        public TransitionSequencer(StateMachine machine)
        {
            Machine = machine;
        }

        public void RequenceTransition(State from, State to)
        {
            Machine.ChangeState(from, to);

           /*  if(to == null || from == to)return;
            if(sequencer != null)
            {
                pending = (from, to);
                return;
            }
            BeginTransition(from , to); */
        }

        void BeginTransition(State from, State to)
        {
            // 1. Deactivate old branch
            sequencer = new NoopPhase();
            sequencer.Start();
            nextPhase = () =>
            {
                // 2. ChangeState
                Machine.ChangeState(from, to);
                // 3. Actiavte the "new branch"
                sequencer = new NoopPhase();
                sequencer.Start();
            };
        }

        void EndTransition()
        {
            sequencer = null;

            if(pending.HasValue)
            {
                (State from, State to) p = pending.Value;
                pending = null;
                BeginTransition(p.from, p.to);
            }
        }

        public void Tick(float deltaTime)
        {
            if(sequencer != null)
            {
                if (sequencer.Update())
                {
                    if(nextPhase != null)
                    {
                        var n = nextPhase;
                        nextPhase = null;
                        n();
                    }
                    else
                    {
                        EndTransition();
                    }
                }
            }
        }

        //Lowest Common Ancestor
        public static State Lca(State a, State b)
        {
            // Set of all parents of 'a'
            var ap = new HashSet<State>();
            for (var s = a; s != null; s = s.Parent) ap.Add(s);

            //Find the first parent of b that also parent of 'a'
            for (var s = b; s != null; s = s.Parent)
                if (ap.Contains(s)) return s;

            return null;

        }
    
    
    }
    public interface ISequencer
    {
        bool IsDone {get;}
        void Start();
        bool Update();
    }
    public class NoopPhase : ISequencer
    {
        public bool IsDone {get; private set;}

        public void Start()
        {
            IsDone = true;
        }

        public bool Update() => IsDone;
    }
}
