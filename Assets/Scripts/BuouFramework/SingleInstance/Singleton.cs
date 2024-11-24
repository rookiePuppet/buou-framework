using System;
using System.Reflection;

namespace BuouFramework.SingleInstance
{
    public class Singleton<T> where T : class
    {
        private static T _instance;
        private static readonly object _lockObj = new();

        protected Singleton()
        {
        }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        _instance ??= CreateInstance();
                    }
                }

                return _instance;
            }
        }

        private static T CreateInstance()
        {
            var constructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null,
                Type.EmptyTypes, null);
            return constructor?.Invoke(null) as T;
        }
    }
}