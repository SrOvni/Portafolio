using System.Collections.Generic;
using System.Reflection;

namespace RG.Systems.HSM
{
    public class StateMachineBuilder
    {
        readonly State _root;
        public StateMachineBuilder(State root)
        {
            _root = root;
        }

        public StateMachine Build()
        {
            StateMachine machine = new(_root);
            Wire(_root, machine, new());
            return machine;
        }
        void Wire(State s, StateMachine m, HashSet<State> visited)
        {
            if (s == null) return;
            if (!visited.Add(s)) return;
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
            var machineField = typeof(State).GetField("Machine", flags);
            if (machineField != null) machineField.SetValue(s, m);
            foreach (var fld in s.GetType().GetFields(flags))
            {

                if (!typeof(State).IsAssignableFrom(fld.FieldType)) continue;
                if (fld.Name == "Parent") continue;

                var child = (State)fld.GetValue(s);
                if (child == null) continue;
                if (!ReferenceEquals(child.Parent, s)) continue;

                Wire(child, m, visited);
            }

        }
    }
}
