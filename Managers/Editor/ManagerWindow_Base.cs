#if UNITY_EDITOR
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using SirenixPowered.ExtensionMethods;
using Sirenix.OdinInspector;
using UnityEditor;
using System.Collections.Generic;

namespace SirenixPowered
{
    public abstract class ManagerWindow_Base : OdinMenuEditorWindow
    {
        private enum VariablesType
        {
            MonoBehaviour,
            ScriptableObject,
        }

        private static Dictionary<System.Type, VariablesType> TypeDict = new Dictionary<System.Type, VariablesType>
        {
        {typeof(MonoBehaviour), VariablesType.MonoBehaviour},
        {typeof(ScriptableObject), VariablesType.ScriptableObject},
        };

        protected int _currentdatasCount;

        protected override void OnGUI()
        {
            wantsMouseEnterLeaveWindow = true;

            TitleLayout();
            base.OnGUI();

            WindowOptions().AmountFormmat = WindowOptions().AmountFormmat == string.Empty ? WindowSettingsEditor.AMOUNT_DATA_ID : WindowOptions().AmountFormmat;
        }

        protected abstract System.Type[] TypesToDisplay();

        protected abstract WindowSettingsManager.WindowOptions WindowOptions();

        protected void SaveAndRefreshWindow()
        {
            ForceMenuTreeRebuild();

            Undo.RecordObject(WindowSettingsManager.Instance, "_settingsDataManager");
            EditorUtility.SetDirty(WindowSettingsManager.Instance);
        }

        private string GetDataName(object type)
        {
            string name = string.Empty;

            switch (type)
            {
                case MonoBehaviour:

                    MonoBehaviour mono = type as MonoBehaviour;

                    if(mono != null)
                    {
                        name = $"[{mono.GetInstanceID()}] {mono.name}";
                    }                 
                    break;

                case ScriptableObject:

                    ScriptableObject data = type as ScriptableObject;
                    name = $"{data.name}";
                    break;
            }

            return name;
        }

        protected void MenuTreeDataSettings(ref OdinMenuTree menuTree, object managerWindow)
        {
            if (WindowOptions().SettingsIconDefault == SdfIconType.None)
            {
                menuTree.Add("Settings", managerWindow, WindowOptions().SettingsIcon);
            }
            else
            {
                menuTree.Add("Settings", managerWindow, WindowOptions().SettingsIconDefault);
            }
        }

        protected void MenuTreeDataFolderNone(ref OdinMenuTree menuTree, object[] files, object type, ref Sprite icon, ref SdfIconType iconType)
        {
            if(type == null)
            {
                return;
            }

            if (WindowOptions().Icons != DataManagerIcons.OnlyGroup && WindowOptions().Icons != DataManagerIcons.All)
            {
                icon = null;
                iconType = SdfIconType.None;
            }

            foreach (object file in files)
            {
                if (file.GetType() == type.GetType())
                {
                    MenuTreeAdd(ref menuTree, file, GetDataName(file), icon, iconType);
                }
            }
        }

        protected void MenuTreeDataFolderEver(ref OdinMenuTree menuTree, object[] files, object type,ref Sprite icon,ref SdfIconType iconType, string path)
        {
            if (type == null)
            {
                return;
            }

            if (WindowOptions().Icons != DataManagerIcons.OnlyGroup && WindowOptions().Icons != DataManagerIcons.All)
            {
                icon = null;
                iconType = SdfIconType.None;
            }

            List<object> fileType = new List<object>();

            foreach (object file in files)
            {
                if (file.GetType() == type.GetType())
                {
                    fileType.Add(file);
                }
            }

            foreach (object file in fileType)
            {
                MenuTreeAdd(ref menuTree, file, $"{path}/{GetDataName(file)}", icon, iconType);
            }
        }

        protected void MenuTreeDataFolderAuto(ref OdinMenuTree menuTree, object[] files, object type, ref Sprite icon,ref SdfIconType iconType, string path)
        {
            if (type == null)
            {
                return;
            }

            if (files.Length >= 2)
            {
                List<object> fileType = new List<object>();

                foreach (object file in files)
                {
                    if (file.GetType() == type.GetType())
                    {
                        fileType.Add(file);
                    }
                }

                if (fileType.Count >= 2)
                {
                    if (WindowOptions().Icons != DataManagerIcons.OnlyGroup && WindowOptions().Icons != DataManagerIcons.All)
                    {
                        icon = null;
                        iconType = SdfIconType.None;
                    }

                    foreach (object file in fileType)
                    {
                        MenuTreeAdd(ref menuTree, file, $"{path}/{GetDataName(file)}", icon, iconType);
                    }
                }
                else
                {
                    if (WindowOptions().Icons != DataManagerIcons.OnlyFolder && WindowOptions().Icons != DataManagerIcons.All)
                    {
                        icon = null;
                        iconType = SdfIconType.None;
                    }

                    if (fileType.Count > 0)
                    {
                        MenuTreeAdd(ref menuTree, fileType[0], path, icon, iconType);
                    }
                }
            }
            else
            {
                if (WindowOptions().Icons != DataManagerIcons.OnlyFolder && WindowOptions().Icons != DataManagerIcons.All)
                {
                    icon = null;
                    iconType = SdfIconType.None;
                }

                if (files.Length > 0)
                {
                    MenuTreeAdd(ref menuTree, files[0], path, icon, iconType);
                }
            }
        }

       protected void MenuTreeFolderAll(ref OdinMenuTree menuTree, object[] files, object type, Sprite icon, SdfIconType iconType)
        {
            if (WindowOptions().FolderAll == ToggleEnum.Enable && type != null)
            {
                string amountFormmat = WindowOptions().ShowAmount == ToggleEnum.Enable ? WindowOptions().AmountFormmat : string.Empty;
                amountFormmat = amountFormmat.Replace(WindowSettingsEditor.AMOUNT_DATA_ID, _currentdatasCount.ToString("00"));

                foreach (object file in files)
                {
                    if (file.GetType() == type.GetType())
                    {
                        MenuTreeAdd(ref menuTree, file, $"{amountFormmat}All/{GetDataName(file)}", icon, iconType);
                    }
                }
            }
        }

        protected void MenuTreeAdd(ref OdinMenuTree menuTree, object type, string path, Sprite icon, SdfIconType iconType)
        {
            switch (iconType)
            {
                case SdfIconType.None:
                    menuTree.Add($"{path}", type, icon);
                    break;
                default:
                    menuTree.Add($"{path}", type, iconType);
                    break;
            }
        }

        private void TitleLayout()
        {
            EditorExtension.Line(WindowOptions().TitleColor);

            GUIStyle style = new GUIStyle();
            style.font = WindowOptions().Font;
            style.fontStyle = WindowOptions().FontStyle;
            style.fontSize = WindowOptions().FontSize;
            style.alignment = WindowOptions().Align;
            style.normal.textColor = WindowOptions().TitleColor;

            EditorExtension.TitleWithIcon(WindowOptions().Title, WindowOptions().BackgroundColor, style, WindowOptions().titleIcon, WindowOptions().iconDistance, WindowOptions().OffSet,
                    GUILayout.Height(WindowOptions().Height));
        }
    }
}
#endif