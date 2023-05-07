#if UNITY_EDITOR
using SirenixPowered.ExtensionMethods;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace SirenixPowered
{
    public sealed partial class WindowSettingsManager
    {
        [ListDrawerSettings(IsReadOnly = true)]
        [SerializeField] private List<MonoBehaviourWindow> _monoBehaviourWindows = new List<MonoBehaviourWindow>();
        [SerializeField] private static Dictionary<string, MonoBehaviourWindow> _monoBehaviourWindowsDic = new Dictionary<string, MonoBehaviourWindow>();

        public static MonoBehaviourWindow AddMonoWindow(System.Type windowType)
        {
            MonoBehaviourWindow newMonoBehaviourWindow = new MonoBehaviourWindow();
            newMonoBehaviourWindow.WindowType = windowType.Name;

            bool canAdd = true;

            foreach (MonoBehaviourWindow window in Instance._monoBehaviourWindows)
            {
                if (window.WindowType == windowType.Name)
                {
                    canAdd = false;
                    break;
                }
            }

            if (canAdd == true)
            {
                Instance._monoBehaviourWindows.Add(newMonoBehaviourWindow);
            }

            ResetMonoBehaviourWindowsDic();

            return newMonoBehaviourWindow;
        }

        public static MonoBehaviourWindow GetMonoWindow(System.Type windowType)
        {
            ResetMonoBehaviourWindowsDic();
            return _monoBehaviourWindowsDic.ContainsKey(windowType.Name) ? _monoBehaviourWindowsDic[windowType.Name] : AddMonoWindow(windowType);
        }

        public static void CheckMonoWindows()
        {
            System.Type[] types = System.AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(MonoBehaviourManager_Base));

            for (int i = 0; i < Instance._monoBehaviourWindows.Count; i++)
            {
                bool remove = true;

                foreach (System.Type type in types)
                {
                    if (Instance._monoBehaviourWindows[i].WindowType == type.Name)
                    {
                        remove = false;
                        break;
                    }
                }

                if (remove.IsTrue())
                {
                    Instance._monoBehaviourWindows.RemoveAt(i);
                }
            }

            ResetMonoBehaviourWindowsDic();
        }

        private static void ResetMonoBehaviourWindowsDic()
        {
            _monoBehaviourWindowsDic = new Dictionary<string, MonoBehaviourWindow>();

            foreach (MonoBehaviourWindow window in Instance._monoBehaviourWindows)
            {
                _monoBehaviourWindowsDic.AddIfNotExists(window.WindowType, window);
            }
        }

        [System.Serializable]
        public sealed class MonoBehaviourWindow
        {
            [BoxGroup(), HideLabel, ReadOnly]
            [SerializeField] private string _windowType;

            [HideLabel]
            [SerializeField] private WindowOptions _windowOptions = new WindowOptions();

            [Title("MonoBehaviour", TitleAlignment = TitleAlignments.Centered)]

            [BoxGroup("_monoTypes", false)]
            [TableList(AlwaysExpanded = true, DrawScrollView = false, IsReadOnly = true, HideToolbar = true)]
            [SerializeField] private List<MonoType> _monoTypes = new List<MonoType>();

            public WindowOptions WindowOptions { get => _windowOptions; set => _windowOptions = value; }
            public string WindowType { get => _windowType; set => _windowType = value; }
            public List<MonoType> MonoTypes { get => _monoTypes; set => _monoTypes = value; }
        }

        [System.Serializable]
        public sealed class MonoType
        {
            [SerializeField, HideInInspector] private string _typeName = string.Empty;
            [SerializeField, HideInInspector] private MonoBehaviour _type = null;

            [PreviewField(Height = 20, Alignment = ObjectFieldAlignment.Center), TableColumnWidth(40, Resizable = false)]
            [SerializeField] private Sprite _icon;

            [TableColumnWidth(150, Resizable = false)]
            [SerializeField] private SdfIconType _iconDefault = SdfIconType.None;

            [SerializeField] private string _folderName = string.Empty;

            public string TypeName { get => _typeName; set => _typeName = value; }
            public Sprite Icon { get => _icon; set => _icon = value; }
            public string FolderName { get => _folderName; set => _folderName = value; }
            public MonoBehaviour Type { get => _type; set => _type = value; }
            public SdfIconType IconDefault { get => _iconDefault; set => _iconDefault = value; }

            public int DataAmount
            {
                get
                {
                    if(Type != null)
                    {
                        MonoBehaviour[] allMono = (MonoBehaviour[])GameObject.FindObjectsOfType(typeof(MonoBehaviour));

                        List<MonoBehaviour> monosSelected = new List<MonoBehaviour>();

                        foreach(MonoBehaviour mono in allMono)
                        {
                            if (mono.GetType() == Type.GetType())
                            {
                                monosSelected.Add(mono);
                            }
                        }

                        return monosSelected.Count;
                    }

                    return 0;
                }
            }
        }
    }
}
#endif