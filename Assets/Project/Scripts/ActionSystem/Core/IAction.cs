using UnityEngine;

namespace RG.Systems
{

    public interface IAction<in TTarget>
    {
        void Apply(TTarget target);
    }

}