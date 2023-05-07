#if UNITY_EDITOR
using SirenixPowered.ExtensionMethods;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SirenixPowered
{
    public sealed partial class WindowSettingsManager : ScriptableSingleton<WindowSettingsManager>
    {
        [System.Serializable]
        public sealed class WindowOptions
        {
            [TabGroup("Options", Icon = SdfIconType.Toggles2)]
            [EnumToggleButtons]
            public ToggleEnum FolderAll = ToggleEnum.Enable;

            [TabGroup("Options", Icon = SdfIconType.Toggles2)]
            [EnumToggleButtons]
            public ToggleEnum ShowAmount = ToggleEnum.Enable;

            [TabGroup("Options", Icon = SdfIconType.Toggles2)]
            [ShowIf(nameof(ShowAmount), ToggleEnum.Enable)]
            public string AmountFormmat = "{amount}•";

            [EnumPaging]
            [TabGroup("Options", Icon = SdfIconType.Toggles2)]
            public DataManagerIcons Icons = DataManagerIcons.All;

            [EnumPaging]
            [TabGroup("Options", Icon = SdfIconType.Toggles2)]
            public DataManagerFolder FolderGroup = DataManagerFolder.Auto;

            [TabGroup("Settings Icon", Icon = SdfIconType.ImageFill)]
            public SdfIconType SettingsIconDefault = SdfIconType.GearFill;

            [TabGroup("Settings Icon", Icon = SdfIconType.ImageFill)]
            public Sprite SettingsIcon;

            [TabGroup("Title", Icon = SdfIconType.TvFill)]
            public string Title = "MANAGER";

            [TabGroup("Title", Icon = SdfIconType.TvFill)]
            public Font Font;

            [TabGroup("Title", Icon = SdfIconType.TvFill)]
            public byte FontSize = 15;

            [TabGroup("Title", Icon = SdfIconType.TvFill)]
            public FontStyle FontStyle = FontStyle.Normal;

            [TabGroup("Title", Icon = SdfIconType.TvFill)]
            public TextAnchor Align = TextAnchor.MiddleLeft;

            [TabGroup("Title", Icon = SdfIconType.TvFill)]
            public byte Height = 0;

            [TabGroup("Title", Icon = SdfIconType.TvFill)]
            public Vector4 OffSet = new Vector4(20, 20, 20,20);

            [Space(10)]

            [TabGroup("Title", Icon = SdfIconType.TvFill)]
            public Texture titleIcon = null;

            [TabGroup("Title", Icon = SdfIconType.TvFill)]
            public byte iconDistance = 2;


            [TabGroup("Color", Icon = SdfIconType.PaletteFill)]
            public Color TitleColor = Color.gray;

            [TabGroup("Color", Icon = SdfIconType.PaletteFill)]
            public Color MenuColor = Color.gray;

            [TabGroup("Color", Icon = SdfIconType.PaletteFill)]
            public Color BackgroundColor = Color.black;
        }
    }
}
#endif