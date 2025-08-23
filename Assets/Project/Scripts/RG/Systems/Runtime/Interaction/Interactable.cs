using UnityEngine;

namespace RG.Systems
{
    public abstract class Interactable: MonoBehaviour
    {
        public abstract void Interact(Interactor interactor);
    }
    public abstract class Interactor : MonoBehaviour
    {

    }
    public interface ICarryable
    {
        void OnPickup(Transform carryPoint);
        void OnDrop();
    }
    public interface IThrowable : ICarryable
    {
        void OnThrow(Vector3 direction);
    }
    public class CarryableObject : Interactable, ICarryable
    {
        public override void Interact(Interactor interactor)
        {
            throw new System.NotImplementedException();
        }

        public void OnDrop()
        {
            throw new System.NotImplementedException();
        }

        public void OnPickup(Transform carryPoint)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ThrowableObject : Interactable, IThrowable
    {
        public override void Interact(Interactor interactor)
        {
            throw new System.NotImplementedException();
        }

        public void OnDrop()
        {
            throw new System.NotImplementedException();
        }

        public void OnPickup(Transform carryPoint)
        {
            throw new System.NotImplementedException();
        }

        public void OnThrow(Vector3 direction)
        {
            throw new System.NotImplementedException();
        }
    }
}
