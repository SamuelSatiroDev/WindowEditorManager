#if UNITY_EDITOR
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using SirenixPowered.ExtensionMethods;
using System.IO;
using UnityEditor;

namespace SirenixPowered
{
    public sealed partial class WindowSettingsManager
    {
        [ListDrawerSettings(IsReadOnly = true)]
        [SerializeField] private List<DataManagerWindow> _dataManagerWindows = new List<DataManagerWindow>();
        [SerializeField] private static Dictionary<string, DataManagerWindow> _dataManagerWindowsDic = new Dictionary<string, DataManagerWindow>();

        public static DataManagerWindow GetDataWindow(System.Type windowID)
        {
            ResetDataWindowsDic();
            return _dataManagerWindowsDic.ContainsKey(windowID.Name) ? _dataManagerWindowsDic[windowID.Name] : AddDataWindow(windowID);
        }

        public static void CheckDataWindows()
        {
            System.Type[] types = System.AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(DataManager_Base));

            for (int i = 0; i < Instance._dataManagerWindows.Count; i++)
            {
                bool remove = true;

                foreach (System.Type type in types)
                {
                    if (Instance._dataManagerWindows[i].WindowType == type.Name)
                    {
                        remove = false;
                        break;
                    }
                }

                if (remove.IsTrue())
                {
                    Instance._dataManagerWindows.RemoveAt(i);
                }
            }

            ResetDataWindowsDic();
        }

        private static void ResetDataWindowsDic()
        {
            _dataManagerWindowsDic = new Dictionary<string, DataManagerWindow>();

            foreach (DataManagerWindow window in Instance._dataManagerWindows)
            {
                _dataManagerWindowsDic.AddIfNotExists(window.WindowType, window);
            }
        }

        [System.Serializable]
        public class DataManagerWindow
        {
            [BoxGroup(), HideLabel, ReadOnly]
            [SerializeField] private string _windowType;

            [HideLabel]
            [SerializeField] private WindowOptions _windowOptions = new WindowOptions();

            [Title("Generate Data", TitleAlignment = TitleAlignments.Centered)]

            [BoxGroup("_dataTypes", false)]
            [SerializeField] private string _defaultPath = "Assets/Data/DataManager";

            [BoxGroup("_dataTypes", false)]
            [TableList(AlwaysExpanded = true, DrawScrollView = false, IsReadOnly = true, HideToolbar = true)]
            [SerializeField] private List<DataType> _dataTypes = new List<DataType>();

            public string WindowType { get => _windowType; set => _windowType = value; }
            public WindowOptions WindowOptions { get => _windowOptions; set => _windowOptions = value; }
            public string DefaultPath { get => _defaultPath; set => _defaultPath = value; }
            public List<DataType> DataTypes { get => _dataTypes; set => _dataTypes = value; }      
        }

        public static DataManagerWindow AddDataWindow(System.Type windowType)
        {
            DataManagerWindow dataManagerWindow = new DataManagerWindow();
            dataManagerWindow.WindowType = windowType.Name;

            bool canAdd = true;

            foreach (DataManagerWindow window in Instance._dataManagerWindows)
            {
                if (window.WindowType == windowType.Name)
                {
                    canAdd = false;
                    break;
                }
            }

            if (canAdd == true)
            {
                Instance._dataManagerWindows.Add(dataManagerWindow);
            }

            ResetDataWindowsDic();

            return dataManagerWindow;
        }

        [System.Serializable]
        public sealed class DataType
        {
            [HideInInspector, SerializeField] private string _typeName = string.Empty;
            [HideInInspector, SerializeField] private ScriptableObject _type = null;
            [HideInInspector, SerializeField] private string _defaultPath = null;

            [PreviewField(Height = 20, Alignment = ObjectFieldAlignment.Center), TableColumnWidth(40, Resizable = false)]
            [SerializeField] private Sprite _icon;

            [TableColumnWidth(150, Resizable = false)]
            [SerializeField] private SdfIconType _iconDefault = SdfIconType.None;

            [SerializeField] private string _folderName = string.Empty;

            [TableColumnWidth(90, Resizable = false)]
            [SerializeField] private Object _folder = null;

            [TableColumnWidth(90, Resizable = false)]
            [InlineButton(nameof(CreateData), SdfIconType.Plus, Label = "")]
            [SerializeField] private int _limit = 100;

            private void CreateData()
            {
                if (DataAmount >= Limit)
                {
                    EditorUtility.DisplayDialog("Warning", $"Data limit reached '{Limit}'", "Ok");
                    return;
                }

                if (Directory.Exists(FolderPath).IsFalse())
                {
                    Directory.CreateDirectory(FolderPath);
                }

                var _asset = ScriptableObject.CreateInstance(Type.GetType().Name);
                string name = AssetDatabase.GenerateUniqueAssetPath($"{FolderPath}/New {Type.GetType().Name}.asset");
                AssetDatabase.CreateAsset(_asset, name);
                AssetDatabase.SaveAssets();
            }

            public string FolderPath
            {
                get
                {
                    string folderPath = Directory.Exists(DefaultPath) ? DefaultPath : WindowSettingsEditor.GENERATE_DATA_PATH;

                    if (_folder != null)
                    {
                        folderPath = AssetDatabase.GetAssetPath(Folder);
                    }

                    return folderPath;
                }
            }

            public int DataAmount
            {
                get
                {                   
                    List<ScriptableObject> amount = new List<ScriptableObject>();
                    amount.LoadFiles(FolderPath, Type.GetType(), FileFormatEnum.Native_asset, SearchOption.AllDirectories);

                    return amount.Count;
                }
            }

            public string TypeName { get => _typeName; set => _typeName = value; }
            public Sprite Icon { get => _icon; set => _icon = value; }
            public SdfIconType IconDefault { get => _iconDefault; set => _iconDefault = value; }
            public string FolderName { get => _folderName; set => _folderName = value; }
            public int Limit { get => _limit; set => _limit = value; }
            public ScriptableObject Type { get => _type; set => _type = value; }
            public Object Folder { get => _folder; set => _folder = value; }
            public string DefaultPath { get => _defaultPath; set => _defaultPath = value; }
        }
    }
}
#endif