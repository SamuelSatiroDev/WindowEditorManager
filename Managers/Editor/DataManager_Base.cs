#if UNITY_EDITOR
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using SirenixPowered.ExtensionMethods;
using static SirenixPowered.WindowSettingsManager;
using Sirenix.OdinInspector;

namespace SirenixPowered
{
    public abstract class DataManager_Base : ManagerWindow_Base
    {
        protected string _newNameData;
        protected Dictionary<Type, DataType> _datasTypeDic = new Dictionary<Type, DataType>();

        protected DataManagerWindow DataManagerWindow
        {
            get
            {
                return GetDataWindow(GetType());
            }
            set
            {
                DataManagerWindow dataManagerWindow = GetDataWindow(GetType());
                dataManagerWindow = value;
            }
        }

        private ScriptableObject CurrentSelected
        {
            get
            {
                if (MenuTree.Selection.SelectedValue != null && MenuTree.Selection.SelectedValue.GetType() != typeof(DataManagerWindow))
                {
                    ScriptableObject scriptableObject = (ScriptableObject)MenuTree.Selection.SelectedValue;

                    return scriptableObject;
                }

                return null;
            }
        }

        private DataType CurrentDataType
        {
            get
            {
                DataType dataType = null;

                if (MenuTree.Selection.SelectedValue != null && MenuTree.Selection.SelectedValue.GetType() != typeof(DataManagerWindow))
                {

                    if (_datasTypeDic.ContainsKey(MenuTree.Selection.SelectedValue.GetType()))
                    {
                        dataType = _datasTypeDic[MenuTree.Selection.SelectedValue.GetType()];
                    }

                    return dataType;
                }

                return dataType;
            }
        }

        protected override WindowOptions WindowOptions() => DataManagerWindow.WindowOptions;

        protected override void OnEnable()
        {
            base.OnEnable();

            AddDataWindow(GetType());
            CheckDataWindows();
        }

        protected override void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            {
                base.OnGUI();

                if (DataManagerWindow != null && DataManagerWindow.DefaultPath.IsNullOrEmpty())
                {
                    DataManagerWindow.DefaultPath = WindowSettingsEditor.GENERATE_DATA_PATH;
                }
            }
            if (EditorGUI.EndChangeCheck() || Event.current.type == EventType.MouseEnterWindow || Event.current.type == EventType.MouseLeaveWindow)
            {
                InitializeDataTypeDic();
                MenuTreeSetDatasType();
                SaveAndRefreshWindow();
            }
        }

        private void InitializeDataTypeDic()
        {
            _currentdatasCount = 0;

            _datasTypeDic = new Dictionary<Type, DataType>();

            foreach (WindowSettingsManager.DataType dataType in DataManagerWindow.DataTypes)
            {
                if (dataType.Type.IsNotNull())
                {
                    _datasTypeDic.AddIfNotExists(dataType.Type.GetType(), dataType);
                }

                if (Directory.Exists(dataType.FolderPath))
                {
                    List<ScriptableObject> files = new List<ScriptableObject>();
                    files.LoadFiles(dataType.FolderPath, FileFormatEnum.Native_asset, SearchOption.AllDirectories);

                    _currentdatasCount += files.Count; 
                }
            }    
        }

        private void MenuTreeSetDatasType()
        {
            foreach (WindowSettingsManager.DataType dataType in DataManagerWindow.DataTypes.ToList())
            {
                dataType.Type = null;
            }

            foreach (Type type in TypesToDisplay())
            {
                WindowSettingsManager.DataType checkDataType = DataManagerWindow.DataTypes.Find(entry => entry.TypeName == type.Name);

                if (checkDataType.IsNull())
                {
                    WindowSettingsManager.DataType data = new WindowSettingsManager.DataType();
                    data.Type = ScriptableObject.CreateInstance(type);
                    data.DefaultPath = DataManagerWindow.DefaultPath;

                    data.TypeName = type.Name;
                    data.FolderName = type.Name;

                    DataManagerWindow.DataTypes.Add(data);
                }
                else
                {
                    checkDataType.Type = ScriptableObject.CreateInstance(type);
                    checkDataType.DefaultPath = DataManagerWindow.DefaultPath;
                }
            }

            foreach (WindowSettingsManager.DataType dataType in DataManagerWindow.DataTypes.ToList())
            {
                if (dataType.Type.IsNull())
                {
                    DataManagerWindow.DataTypes.Remove(dataType);
                }
            }
        }

        private void ShowDataOptions()
        {
            if (CurrentSelected == null)
            {
                return;
            }

            GUIStyle style = new GUIStyle("Toolbar");
            style.normal.background = EditorExtension.MakeTex(2, 2, ColorExtension.Color(Tones.DimGray_696969));

            SirenixEditorGUI.BeginHorizontalToolbar(style);
            {
                style = new GUIStyle("ToolbarButton");
                style.normal.background = EditorExtension.MakeTex(2, 2, WindowOptions().MenuColor);

                EditorExtension.Button(WindowSettingsEditor.RENAME_ICON, style, WindowOptions().MenuColor, RenameDataSelected, GUILayout.Width(30));

                _newNameData = GUILayout.TextField(_newNameData, GUILayout.MinWidth(150), GUILayout.Height(16));

                EditorExtension.Button(WindowSettingsEditor.SEARCH_ICON, style, WindowOptions().MenuColor, SearchCurrentData, GUILayout.Width(30));
                EditorExtension.Button(WindowSettingsEditor.DELETE_ICON, style, Tones.OrangeRed_FF4500, DeleteCurrentData, GUILayout.Width(30));
            }
            SirenixEditorGUI.EndHorizontalToolbar();

            void RenameDataSelected()
            {
                InitializeDataTypeDic();

                if (CurrentSelected.name == WindowSettingsEditor.WINDOW_SETTINGS_MANAGER_NAME)
                {
                    return;
                }

                if (CanRename().IsFalse())
                {
                    EditorUtility.DisplayDialog("Warning", "This name is not available!", "Ok");
                    return;
                }

                if (EditorUtility.DisplayDialog("Warning", $"Do you want to rename the data '{CurrentSelected.name}' for '{_newNameData}' ?", "Confirm", "Cancel").IsFalse())
                {
                    return;
                }

                CurrentSelected.RenameAsset(_newNameData);
                _newNameData = string.Empty;

                ForceMenuTreeRebuild();

                bool CanRename()
                {
                    bool check = false;

                    if(CurrentDataType == null)
                    {
                        Debug.Log("EStá nulo");
                    }

                    List<ScriptableObject> existingData = new List<ScriptableObject>();

                    existingData.LoadFiles(CurrentDataType.FolderPath, existingData.GetType().GetListType(), FileFormatEnum.Native_asset, SearchOption.AllDirectories);
                    ScriptableObject data = existingData.Find(entry => entry.name == _newNameData);

                    check = _newNameData != string.Empty && CurrentSelected.name != _newNameData && data.IsNull();

                    return check;
                }
            }

            void SearchCurrentData() => Selection.activeObject = MenuTree.Selection.SelectedValue as Object;

            void DeleteCurrentData()
            {
                ScriptableObject scriptableObject = MenuTree.Selection.SelectedValue as ScriptableObject;

                if (scriptableObject.name == WindowSettingsEditor.WINDOW_SETTINGS_MANAGER_NAME)
                {
                    return;
                }

                if (EditorUtility.DisplayDialog("Warning", $"Really want to delete '{scriptableObject.name}' ?", "Confirm", "Cancel").IsFalse())
                {
                    return;
                }

                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(MenuTree.Selection.SelectedValue as Object));
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                ForceMenuTreeRebuild();
            }
        }
        protected override void OnBeginDrawEditors()
        {
            ShowDataOptions();

            if(CurrentSelected == null)
            {
                return;
            }

            GUIStyle style = new GUIStyle();
            style.normal.background = EditorExtension.MakeTex(2, 2, Color.black);
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.gray;

            GUILayout.Label(CurrentSelected.name, style, GUILayout.Height(20));
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree menuTree = new OdinMenuTree();

            MenuTreeDataSettings(ref menuTree, DataManagerWindow);

            foreach (WindowSettingsManager.DataType dataType in DataManagerWindow.DataTypes.ToList())
            {
                List<ScriptableObject> files = new List<ScriptableObject>();
                List<ScriptableObject> fileType = new List<ScriptableObject>();

                if (Directory.Exists(dataType.FolderPath) && dataType.FolderPath.IsNullOrEmpty() == false)
                {
                    files.LoadFiles(dataType.FolderPath, FileFormatEnum.Native_asset, SearchOption.AllDirectories);

                    MenuTreeFolderAll(ref menuTree, files.ToArray(), dataType.Type, dataType.Icon, dataType.IconDefault);

                    bool enable = dataType.DataAmount > 1 && WindowOptions().FolderGroup == DataManagerFolder.Auto || WindowOptions().FolderGroup == DataManagerFolder.Ever;
                    string amountFormmat = enable.IsTrue() && WindowOptions().FolderGroup != DataManagerFolder.None && WindowOptions().ShowAmount == ToggleEnum.Enable ? WindowOptions().AmountFormmat : string.Empty;
                    amountFormmat = amountFormmat.Replace(WindowSettingsEditor.AMOUNT_DATA_ID, dataType.DataAmount.ToString("00"));

                    string name = dataType.FolderName.IsNullOrEmpty() ? amountFormmat + dataType.Type.GetType().Name : amountFormmat + dataType.FolderName;

                    Sprite icon = WindowOptions().Icons != DataManagerIcons.None ? dataType.Icon : null;
                    SdfIconType iconDefault = WindowOptions().Icons != DataManagerIcons.None ? dataType.IconDefault : SdfIconType.None;

                    switch (WindowOptions().FolderGroup)
                    {
                        case DataManagerFolder.None:
                            MenuTreeDataFolderNone(ref menuTree, files.ToArray(), dataType.Type,ref icon,ref iconDefault);
                            break;

                        case DataManagerFolder.Ever:
                            MenuTreeDataFolderEver(ref menuTree, files.ToArray(), dataType.Type, ref icon, ref iconDefault, name);
                            break;

                        case DataManagerFolder.Auto:
                            MenuTreeDataFolderAuto(ref menuTree, files.ToArray(), dataType.Type, ref icon, ref iconDefault, name);
                            break;
                    }
                }
            }

            return menuTree;
        }
    }
}
#endif