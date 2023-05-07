#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;

namespace SirenixPowered.Demo
{
    public sealed class MonoBehaviourManager_WindowDemo : MonoBehaviourManager_Base
    {
        [MenuItem("Rage Machine/MonoBehaviour Manager/MonoBehaviour")]
        public static void OpenWindow() => GetWindow<MonoBehaviourManager_WindowDemo>();

        protected override Type[] TypesToDisplay() => TypeCache.GetTypesWithAttribute<ManageableMonoAttribute>().OrderBy(m => m.Name).ToArray();
    }
}
#endif