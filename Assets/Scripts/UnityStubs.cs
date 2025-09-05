#if !UNITY_5_3_OR_NEWER
using System;
using System.Reflection;

namespace UnityEngine
{
    public class MonoBehaviour { }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SerializeField : Attribute { }

    public class GameObject
    {
        public T AddComponent<T>() where T : new()
        {
            var instance = new T();
            var method = typeof(T).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            method?.Invoke(instance, null);
            return instance;
        }
    }
}
#endif
