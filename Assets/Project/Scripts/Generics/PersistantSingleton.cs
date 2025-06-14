using UnityEngine;

namespace Generic
{
    public abstract class PersistantSingleton<T>: Singleton<T> where T : Component{
        public bool AutoUnparentOnAwake = true;

        protected override void InitializeSingleton()
        {
            if(!Application.isPlaying)return;
            if(AutoUnparentOnAwake)transform.SetParent(null);
            if(Instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }else{
                if(_instance != this){
                    Destroy(gameObject);
                }
            }
        }
    }
}