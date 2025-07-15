using UnityEngine;

namespace Patterns
{
    public abstract class Singleton<T>:MonoBehaviour where T : Component
    {
        protected static T _instance; 
        public static T Instance{
            get{
                if(_instance == null){
                    _instance = FindAnyObjectByType<T>();
                    if(_instance == null){
                    var go = new GameObject(typeof(T).Name + "Singleton Auto-Generated");
                    go.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }
        public static bool HasInstance => Instance != null;

        protected void Awake() {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton()
        {
            if(!Application.isPlaying)return;

            _instance = this as T;

        }
    }
}
