#if !UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
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

    public static class PlayerPrefs
    {
        private static readonly Dictionary<string, int> IntStore = new Dictionary<string, int>();
        public static void SetInt(string key, int value) => IntStore[key] = value;
        public static int GetInt(string key, int defaultValue = 0) => IntStore.TryGetValue(key, out var v) ? v : defaultValue;
        public static void Save() { }
    }

    public static class Debug
    {
        public static void Log(object message) { }
    }

    public struct Vector3
    {
        public float x, y, z;
        public Vector3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
        public static Vector3 one => new Vector3(1,1,1);
    }

    public struct Color
    {
        public static Color gray => new Color();
    }

    public static class Gizmos
    {
        public static Color color { get; set; }
        public static void DrawWireCube(Vector3 center, Vector3 size) { }
    }
}

namespace TMPro
{
    public class TextMeshProUGUI
    {
        public string text = string.Empty;
    }
}
#endif
