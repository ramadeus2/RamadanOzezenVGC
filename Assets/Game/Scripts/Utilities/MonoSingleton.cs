using UnityEngine;

namespace WheelOfFortune {
    public class MonoSingleton<T>: MonoBehaviour where T : MonoBehaviour {
        [SerializeField] private bool _dontDestroyOnLoad;
        private static volatile T instance = null;

        public static T Instance => instance;

        protected virtual void Awake()
        {
            Singleton();
        }

        private void Singleton()
        {
            if(instance == null)
            {
                instance = this as T;
                if(_dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            } else
            {
                Destroy(gameObject);
            }
        }
    }
}