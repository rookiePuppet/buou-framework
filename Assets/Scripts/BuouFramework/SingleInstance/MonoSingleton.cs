using UnityEngine;

namespace BuouFramework.SingleInstance
{
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        protected MonoSingleton() { }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = CreateInstance();
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                Debug.LogWarning($"Found duplication of {typeof(T).Name}: {gameObject.name}");
            }
            else
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
        }

        private static T CreateInstance()
        {
            var gameObject = new GameObject($"{typeof(T).Name} [Auto Generated]");
            DontDestroyOnLoad(gameObject);
            var instance = gameObject.AddComponent<T>();
            return instance;
        }
    }
}

