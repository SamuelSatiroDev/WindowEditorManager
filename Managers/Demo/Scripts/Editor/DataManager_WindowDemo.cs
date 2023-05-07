#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;

namespace SirenixPowered.Demo
{
    public sealed class DataManager_WindowDemo : DataManager_Base
    {
        [MenuItem("Rage Machine/Data Manager/ScriptableObject")]
        public static void OpenWindow() => GetWindow<DataManager_WindowDemo>();

        protected override Type[] TypesToDisplay() => TypeCache.GetTypesWithAttribute<ManageableDataAttribute>().OrderBy(m => m.Name).ToArray();
    }
}
#endif