#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

namespace SirenixPowered.ExtensionMethods
{
    public static class ScriptableObjectExtension
    {
        public static ScriptableObject CreateAsset(this ScriptableObject data, System.Type type, string path)
        {
            ScriptableObject _asset = ScriptableObject.CreateInstance(type.Name);
            string name = AssetDatabase.GenerateUniqueAssetPath($"Assets/{path}.asset");
            AssetDatabase.CreateAsset(_asset, name);
            AssetDatabase.SaveAssets();

            return _asset;
        }

        public static void CreateAsset(System.Type type, string path)
        {
            ScriptableObject _asset = ScriptableObject.CreateInstance(type.Name);
            string name = AssetDatabase.GenerateUniqueAssetPath($"{path}.asset");
            AssetDatabase.CreateAsset(_asset, name);
            AssetDatabase.SaveAssets();
        }

        public static ScriptableObject CreateAsset(string type, string folderPath, string fileName)
        {
            if (Directory.Exists($"Assets/{folderPath}").IsFalse())
            {
                Directory.CreateDirectory($"Assets/{folderPath}");
            }

            ScriptableObject _asset = ScriptableObject.CreateInstance(type);
            string name = AssetDatabase.GenerateUniqueAssetPath($"Assets/{folderPath}/{fileName}.asset");
            AssetDatabase.CreateAsset(_asset, name);
            AssetDatabase.SaveAssets();

            return _asset;
        }

        public static void RenameAsset(this ScriptableObject data, string newName)
        {
            string assetPath = AssetDatabase.GetAssetPath(data.GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, newName);
            AssetDatabase.SaveAssets();
        }
    }
}
#endif