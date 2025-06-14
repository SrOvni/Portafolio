using UnityEngine;
namespace EnemyBaseClass
{
    public class Enemy : MonoBehaviour
    {
        public void Initialize(Vector3 spawPosiiton)
        {
            transform.position = spawPosiiton;
            gameObject.SetActive(true);
        }
    }
}