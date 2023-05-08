#if UNITY_EDITOR
using System;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;
using SirenixPowered.ExtensionMethods;
using UnityEditor;
using static SirenixPowered.WindowSettingsManager;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using Sirenix.OdinInspector;

namespace SirenixPowered
{
    public abstract class MonoBehaviourManager_Base : ManagerWindow_Base
    {
        protected List<MonoBehaviour> MonoBehaviours = new List<MonoBehaviour>();
        protected string _newNameData;

        private MonoBehaviourWindow MonoBehaviourWindow
        {
            get
            {
                return GetMonoWindow(GetType());
            }
            set
            {
                MonoBehaviourWindow monoBehaviourWindow = GetMonoWindow(GetType());
                monoBehaviourWindow = value;
            }
        }

        private MonoBehaviour CurrentSelected
        {
            get
            {
                if (MenuTree.Selection.SelectedValue != null && MenuTree.Selection.SelectedValue.GetType() != typeof(MonoBehaviourWindow))
                {
                    return (MonoBehaviour) MenuTree.Selection.SelectedValue;
                }

                return null;
            }
        }

        protected override WindowOptions WindowOptions() => MonoBehaviourWindow.WindowOptions;

        protected override void OnEnable()
        {
            base.OnEnable();

            AddMonoWindow(GetType());
            CheckMonoWindows();
            GetDatas();
        }

        protected override void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            {
                base.OnGUI();
            }
            if (EditorGUI.EndChangeCheck() || Event.current.type == EventType.MouseEnterWindow || Event.current.type == EventType.MouseLeaveWindow)
            {
                SaveAndRefreshWindow();
            }
        }

        private void GetDatas()
        {
            MonoBehaviour[] allMono = (MonoBehaviour[])GameObject.FindObjectsOfType(typeof(MonoBehaviour));
            MonoBehaviours = new List<MonoBehaviour>();

            foreach (MonoBehaviour mono in allMono)
            {
                foreach (Type type in TypesToDisplay())
                {
                    if (mono.GetType() == type)
                    {
                        MonoBehaviours.Add(mono);
                        break;
                    }
                }
            }

            _currentdatasCount = MonoBehaviours.Count();
        }

        private void MenuTreeSetDatasType()
        {
            foreach (MonoType monoType in MonoBehaviourWindow.MonoTypes.ToList())
            {
                monoType.Type = null;
            }

            foreach (MonoBehaviour monoBehaviour in MonoBehaviours)
            {
                MonoType checkDataType = MonoBehaviourWindow.MonoTypes.Find(entry => entry.TypeName == monoBehaviour.GetType().Name);

                if (checkDataType.IsNull())
                {
                    MonoType data = new MonoType();

                    data.Type = monoBehaviour;
                    data.TypeName = monoBehaviour.GetType().Name;
                    data.FolderName = monoBehaviour.GetType().Name;

                    MonoBehaviourWindow.MonoTypes.Add(data);
                }
                else
                {
                    checkDataType.Type = monoBehaviour;
                }
            }

            foreach (MonoType monoType in MonoBehaviourWindow.MonoTypes.ToList())
            {
                if (monoType.Type.IsNull())
                {
                    MonoBehaviourWindow.MonoTypes.Remove(monoType);
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

                CurrentSelected.name = _newNameData;
                _newNameData = string.Empty;

                ForceMenuTreeRebuild();

                bool CanRename()
                {
                    bool check = _newNameData != string.Empty && CurrentSelected.name != _newNameData;
                    return check;
                }
            }

            void SearchCurrentData() => Selection.activeObject = MenuTree.Selection.SelectedValue as Object;

            void DeleteCurrentData()
            {
                if (EditorUtility.DisplayDialog("Warning", $"Really want to delete '{CurrentSelected.name}' ?", "Confirm", "Cancel").IsFalse())
                {
                    return;
                }

                DestroyImmediate(CurrentSelected);

                ForceMenuTreeRebuild();
            }
        }

        protected override void OnBeginDrawEditors()
        {
            ShowDataOptions();

            if (CurrentSelected == null)
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
            GetDatas();
            MenuTreeSetDatasType();

            OdinMenuTree menuTree = new OdinMenuTree();

            MenuTreeDataSettings(ref menuTree, MonoBehaviourWindow);

            foreach (WindowSettingsManager.MonoType dataType in MonoBehaviourWindow.MonoTypes.ToList())
            {
                List<MonoBehaviour> files = new List<MonoBehaviour>(MonoBehaviours);
                List<MonoBehaviour> fileType = new List<MonoBehaviour>();

                MenuTreeFolderAll(ref menuTree, files.ToArray(), dataType.Type, dataType.Icon, dataType.IconDefault);

                if(dataType.Type  == null)
                {
                    continue;
                }

                bool enable = dataType.DataAmount > 1 && WindowOptions().FolderGroup == DataManagerFolder.Auto || WindowOptions().FolderGroup == DataManagerFolder.Ever;
                string amountFormmat = enable.IsTrue() && WindowOptions().FolderGroup != DataManagerFolder.None && WindowOptions().ShowAmount == ToggleEnum.Enable ? WindowOptions().AmountFormmat : string.Empty;
                amountFormmat = amountFormmat.Replace(WindowSettingsEditor.AMOUNT_DATA_ID, dataType.DataAmount.ToString("00"));

                string name = dataType.FolderName.IsNullOrEmpty() ? amountFormmat + dataType.Type.GetType().Name : amountFormmat + dataType.FolderName;

                Sprite icon = WindowOptions().Icons != DataManagerIcons.None ? dataType.Icon : null;
                SdfIconType iconDefault = WindowOptions().Icons != DataManagerIcons.None ? dataType.IconDefault : SdfIconType.None;

                switch (WindowOptions().FolderGroup)
                {
                    case DataManagerFolder.None:
                        MenuTreeDataFolderNone(ref menuTree, files.ToArray(), dataType.Type, ref icon, ref iconDefault);
                        break;

                    case DataManagerFolder.Ever:
                        MenuTreeDataFolderEver(ref menuTree, files.ToArray(), dataType.Type, ref icon, ref iconDefault, name);
                        break;

                    case DataManagerFolder.Auto:
                        MenuTreeDataFolderAuto(ref menuTree, files.ToArray(), dataType.Type, ref icon, ref iconDefault, name);
                        break;
                }            
            }

            return menuTree;
        }
    }
}
#endif