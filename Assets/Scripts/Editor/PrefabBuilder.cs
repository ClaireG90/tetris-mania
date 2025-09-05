#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace TetrisMania.EditorTools
{
    /// <summary>
    /// Builds required prefabs for the project from sprites.
    /// </summary>
    public static class PrefabBuilder
    {
        private const string SpritePath = "Assets/Sprites/block.png";
        private const string PrefabPath = "Assets/Prefabs/Block.prefab";

        /// <summary>
        /// Generates the block prefab used across the project.
        /// </summary>
        [MenuItem("Tools/Build Prefabs")]
        public static void Build()
        {
            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(SpritePath);
            if (sprite == null)
            {
                Debug.LogError($"Sprite not found at {SpritePath}");
                return;
            }

            var go = new GameObject("Block");
            var renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            go.transform.localScale = Vector3.one;

            var dir = System.IO.Path.GetDirectoryName(PrefabPath);
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir!);
            }

            PrefabUtility.SaveAsPrefabAsset(go, PrefabPath);
            Object.DestroyImmediate(go);
            AssetDatabase.SaveAssets();
            Debug.Log("Block prefab built");
        }
    }
}
#endif
