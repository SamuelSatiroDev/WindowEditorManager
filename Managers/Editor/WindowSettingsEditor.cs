namespace SirenixPowered
{
    public enum DataManagerFolder
    {
        Auto,
        Ever,
        None,
    }

    public enum DataManagerIcons
    {
        All,
        OnlyGroup,
        OnlyFolder,
        None,
    }

    public static class WindowSettingsEditor
    {
        public const string GENERATE_DATA_PATH = "Assets/Data/DataManager";
        public const string WINDOW_SETTINGS_MANAGER_NAME = "WindowSettingsManager";
        public const string AMOUNT_DATA_ID = "{amount}";

        public const string MANAGER_TITLE_ICON_DEFAULT = "ManagerIcons/Manager_TitleIconDefault";
        public const string RENAME_ICON = "ManagerIcons/Manager_RenameIcon";
        public const string SEARCH_ICON = "ManagerIcons/Manager_SearchIcon";
        public const string DELETE_ICON = "ManagerIcons/Manager_DeleteIcon";
    }
}