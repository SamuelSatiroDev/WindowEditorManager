#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace SirenixPowered.ExtensionMethods
{
    #region FileFormat
    public enum FileFormatEnum
    {
        All,
        //AUDIO ASSETS
        Audio,
        Audio_ogg = 2,
        Audio_aif,
        Audio_aiff,
        Audio_flac,
        Audio_wav,
        Audio_mp3,
        Audio_mod,
        Audio_it,
        Audio_s3m,
        Audio_xm = 11,

        //3D ASSETS
        Mesh,
        Mesh_fbx = 13,
        Mesh_mb,
        Mesh_ma,
        Mesh_max,
        Mesh_jas,
        Mesh_dae,
        Mesh_dxf,
        Mesh_obj,
        Mesh_c4d,
        Mesh_blend,
        Mesh_lxo,
        Mesh_3ds,
        Mesh_skp,
        Mesh_spm,
        Mesh_st,
        Mesh_sbsar = 28,

        //IHV IMAGE FORMAT
        IHVImageFormat,
        IHVImageFormat_astc = 30,
        IHVImageFormat_dds,
        IHVImageFormat_ktx,
        IHVImageFormat_pvr = 33,

        //NATIVE
        Native,
        Native_anim = 35,
        Native_animset,
        Native_asset,
        Native_blendtree,
        Native_buildreport,
        Native_colors,
        Native_controller,
        Native_cubemap,
        Native_curves,
        Native_curvesNormalized,
        Native_flare,
        Native_fontsettings,
        Native_giparams,
        Native_gradients,
        Native_guiskin,
        Native_ht,
        Native_mask,
        Native_mat,
        Native_mesh,
        Native_mixer,
        Native_overrideController,
        Native_particleCurves,
        Native_particleCurvesSigned,
        Native_particleDoubleCurves,
        Native_particleDoubleCurvesSigned,
        Native_physicMaterial,
        Native_physicsMaterial2D,
        Native_playable,
        Native_preset,
        Native_renderTexture,
        Native_shadervariants,
        Native_spriteatlas,
        Native_state,
        Native_statemachine,
        Native_texture2D,
        Native_transition,
        Native_webCamTexture,
        Native_brush,
        Native_terrainlayer,
        Native_signal = 74,

        //PLUGIN
        Plugin,
        Plugin_dll = 76,
        Plugin_winmd,
        Plugin_so,
        Plugin_jar,
        Plugin_java,
        Plugin_kt,
        Plugin_aar,
        Plugin_suprx,
        Plugin_prx,
        Plugin_rpl,
        Plugin_cpp,
        Plugin_cc,
        Plugin_c,
        Plugin_h,
        Plugin_jslib,
        Plugin_jspre,
        Plugin_bc,
        Plugin_a,
        Plugin_m,
        Plugin_mm,
        Plugin_swift,
        Plugin_xib,
        Plugin_bundle,
        Plugin_dylib,
        Plugin_config = 100,

        //SHADER
        Shader,
        Shader_cginc = 102,
        Shader_cg,
        Shader_glslinc,
        Shader_hlsl,
        Shader_shader,
        Shader_compute = 107,

        //TEXT
        Text,
        Text_txt = 109,
        Text_html,
        Text_htm,
        Text_xml,
        Text_json,
        Text_csv,
        Text_yaml,
        Text_bytes,
        Text_fnt,
        Text_manifest,
        Text_md,
        Text_js,
        Text_boo,
        Text_rsp,
        Text_cs = 123,

        //TEXTURE
        Texture,
        Texture_jpg = 125,
        Texture_jpeg,
        Texture_tif,
        Texture_tiff,
        Texture_tga,
        Texture_gif,
        Texture_png,
        Texture_psd,
        Texture_bmp,
        Texture_iff,
        Texture_pict,
        Texture_pic,
        Texture_pct,
        Texture_exr,
        Texture_hdr = 139,

        //FONT
        Font,
        Font_ttf = 141,
        Font_dfont,
        Font_otf,
        Font_ttc = 144,

        //VIDEO CLIP
        VideoClip,
        VideoClip_avi = 146,
        VideoClip_asf,
        VideoClip_wmv,
        VideoClip_mov,
        VideoClip_dv,
        VideoClip_mp4,
        VideoClip_m4v,
        VideoClip_mpg,
        VideoClip_mpeg,
        VideoClip_ogv,
        VideoClip_vp8,
        VideoClip_webm = 157,

        //VISUAL EFFECT
        VisualEffect,
        VisualEffect_vfx = 159,
        VisualEffect_vfxoperator,
        VisualEffect_vfxblock = 161,

        //OTHERS
        Other,
        Other_raytrace = 163,
        Other_prefab,
        Other_po,
        Other_rsp,
        Other_unity,
        Other_asmref,
        Other_asmdef,
        Other_uss,
        Other_uxml = 171,
    }
    #endregion

    public static class LoadAssetsExtension
    {
        private static Vector2 AUDIO_INDEX = new Vector2((int)FileFormatEnum.Audio_ogg, (int)FileFormatEnum.Audio_xm);
        private static Vector2 MESH_INDEX = new Vector2((int)FileFormatEnum.Mesh_fbx, (int)FileFormatEnum.Mesh_sbsar);
        private static Vector2 IHV_IMAGE_FORMAT_INDEX = new Vector2((int)FileFormatEnum.IHVImageFormat_astc, (int)FileFormatEnum.IHVImageFormat_pvr);
        private static Vector2 NATIVE_INDEX = new Vector2((int)FileFormatEnum.Native_anim, (int)FileFormatEnum.Native_signal);
        private static Vector2 PLUGIN_INDEX = new Vector2((int)FileFormatEnum.Plugin_dll, (int)FileFormatEnum.Plugin_config);
        private static Vector2 SHADER_INDEX = new Vector2((int)FileFormatEnum.Shader_cginc, (int)FileFormatEnum.Shader_compute);
        private static Vector2 TEXT_INDEX = new Vector2((int)FileFormatEnum.Text_txt, (int)FileFormatEnum.Text_cs);
        private static Vector2 TEXTURE_INDEX = new Vector2((int)FileFormatEnum.Texture_jpg, (int)FileFormatEnum.Texture_hdr);
        private static Vector2 FONT_INDEX = new Vector2((int)FileFormatEnum.Font_ttf, (int)FileFormatEnum.Font_ttc);
        private static Vector2 VIDEOCLIP_INDEX = new Vector2((int)FileFormatEnum.VideoClip_avi, (int)FileFormatEnum.VideoClip_webm);
        private static Vector2 VISUAL_EFFECT_INDEX = new Vector2((int)FileFormatEnum.VisualEffect_vfx, (int)FileFormatEnum.VisualEffect_vfxblock);
        private static Vector2 OTHER_INDEX = new Vector2((int)FileFormatEnum.Other_raytrace, (int)FileFormatEnum.Other_uxml);

        #region GetFolders
        public static List<string> GetFoldersPath(this string RootPath, SearchOption SearchOption)
        {
            List<string> foldersPaths = new List<string>();
            foldersPaths = Directory.GetDirectories(RootPath, "*", SearchOption).ToList();

            return foldersPaths;
        }

        public static List<string> GetFoldersPath(this Object RootFolder, SearchOption SearchOption)
        {
            List<string> foldersPaths = new List<string>();
            string folderPath = AssetDatabase.GetAssetPath(RootFolder);
            foldersPaths = Directory.GetDirectories(folderPath, "*", SearchOption).ToList();

            return foldersPaths;
        }

        public static List<Object> GetFoldersObject(this Object RootFolder, SearchOption SearchOption)
        {
            string folderPath = AssetDatabase.GetAssetPath(RootFolder);
            string[] files = Directory.GetDirectories(folderPath, "*.", SearchOption);

            List<Object> folders = new List<Object>();
            foreach (string filePath in files)
            {
                Object asset = (Object)AssetDatabase.LoadAssetAtPath(filePath, typeof(Object));
                folders.Add(asset);
            }

            return folders;
        }

        public static List<Object> GetFoldersObject(this string RootPath, SearchOption SearchOption)
        {
            if(RootPath == string.Empty)
            {
                return new List<Object>();
            }

            string[] files = Directory.GetDirectories(RootPath, "*.", SearchOption);

            List<Object> folders = new List<Object>();
            foreach (string filePath in files)
            {
                Object asset = (Object)AssetDatabase.LoadAssetAtPath(filePath, typeof(Object));
                folders.Add(asset);
            }

            return folders;
        }
        #endregion

        #region GetFolder
        public static Object GetFolder(this string FolderName, Object RootFolder, SearchOption SearchOption)
        {
            string folderPath = AssetDatabase.GetAssetPath(RootFolder);
            string[] files = Directory.GetDirectories(folderPath, "*.", SearchOption);

            Object folder = null;
            foreach (string filePath in files)
            {
                Object asset = (Object)AssetDatabase.LoadAssetAtPath(filePath, typeof(Object));
                if (asset.name == FolderName)
                {
                    folder = asset;
                    return folder;
                }
            }

            return folder;
        }

        public static Object GetFolder(this string FolderName, string RootPath, SearchOption SearchOption)
        {
            string[] files = Directory.GetDirectories(RootPath, "*.", SearchOption);

            Object folder = null;
            foreach (string filePath in files)
            {
                Object asset = (Object)AssetDatabase.LoadAssetAtPath(filePath, typeof(Object));
                if (asset.name == FolderName)
                {
                    folder = asset;
                    return folder;
                }
            }

            return folder;
        }

        public static string GetFolderPath(this string FolderName, Object RootFolder, SearchOption SearchOption)
        {
            string folderPath = AssetDatabase.GetAssetPath(RootFolder);
            string[] files = Directory.GetDirectories(folderPath, "*.", SearchOption);

            string folder = string.Empty;
            foreach (string filePath in files)
            {
                Object asset = (Object)AssetDatabase.LoadAssetAtPath(filePath, typeof(Object));
                if (asset.name == FolderName)
                {
                    folder = filePath;
                    return folder;
                }
            }

            return folder;
        }

        public static string GetFolderPath(this string FolderName, string RootPath, SearchOption SearchOption)
        {
            string[] files = Directory.GetDirectories(RootPath, "*.", SearchOption);

            string folder = string.Empty;
            foreach (string filePath in files)
            {
                Object asset = (Object)AssetDatabase.LoadAssetAtPath(filePath, typeof(Object));
                if (asset.name == FolderName)
                {
                    folder = filePath;
                    return folder;
                }
            }

            return folder;
        }
        #endregion


        #region CreateFolder
        public static void CreateFolder(this string FolderPath)
        {
            if (Directory.Exists(FolderPath) == false)
            {
                Directory.CreateDirectory(FolderPath);
                AssetDatabase.Refresh();
            }
        }

        public static void CreateFolder(this string FolderName, string RootPath)
        {
            string newDirectory = $"{RootPath}/{FolderName}";

            if (Directory.Exists(newDirectory) == false)
            {
                Directory.CreateDirectory(newDirectory);
                AssetDatabase.Refresh();
            }
        }

        public static void CreateFolder(this string FolderName, Object RootFolder)
        {
            string folderPath = AssetDatabase.GetAssetPath(RootFolder);
            string newDirectory = $"{folderPath}/{FolderName}";

            if (Directory.Exists(newDirectory) == false)
            {
                Directory.CreateDirectory(newDirectory);
                AssetDatabase.Refresh();
            }
        }
        #endregion

        #region DeleteFolder
        public static void DeleteFolder(this Object Folder)
        {
            string folderPath = AssetDatabase.GetAssetPath(Folder);
            if (Directory.Exists(folderPath) == false)
            {
                return;
            }

            DirectoryInfo directory = new DirectoryInfo(folderPath);
            directory.Delete(true);

            File.Delete($"{folderPath}.meta");

            Directory.CreateDirectory(folderPath);

            AssetDatabase.Refresh();
        }

        public static void DeleteFolder(this string FolderPath)
        {
            if (Directory.Exists(FolderPath) == false)
            {
                return;
            }

            Directory.CreateDirectory(FolderPath);

            DirectoryInfo directory = new DirectoryInfo(FolderPath);
            directory.Delete(true);

            File.Delete($"{FolderPath}.meta");

            AssetDatabase.Refresh();
        }

        public static void DeleteFolder(this string FolderName, string RootPath, SearchOption SearchOption)
        {
            string _folder = string.Empty;
            _folder = RootPath.GetFolderPath(FolderName, SearchOption);

            if (Directory.Exists(_folder) == false)
            {
                return;
            }

            Directory.CreateDirectory(_folder);

            DirectoryInfo directory = new DirectoryInfo(_folder);
            directory.Delete(true);

            File.Delete($"{_folder}.meta");

            AssetDatabase.Refresh();
        }

        public static void DeleteFolder(this string FolderName, Object RootFolder, SearchOption SearchOption)
        {
            string _folder = string.Empty;
            _folder = FolderName.GetFolderPath(RootFolder, SearchOption);

            if (Directory.Exists(_folder) == false)
            {
                return;
            }

            Directory.CreateDirectory(_folder);

            DirectoryInfo directory = new DirectoryInfo(_folder);
            directory.Delete(true);

            File.Delete($"{_folder}.meta");

            AssetDatabase.Refresh();
        }

        public static void DeleteAllFolders(this Object RootFolder)
        {
            List<string> _folders = RootFolder.GetFoldersPath(SearchOption.TopDirectoryOnly);

            for (int i = 0; i < _folders.Count; i++)
            {
                DirectoryInfo directory = new DirectoryInfo(_folders[i]);
                directory.Delete(true);
                File.Delete($"{_folders[i]}.meta");
            }

            AssetDatabase.Refresh();
        }

        public static void DeleteAllFolders(this string RootPath)
        {
            List<string> _folders = RootPath.GetFoldersPath(SearchOption.TopDirectoryOnly);

            for (int i = 0; i < _folders.Count; i++)
            {
                DirectoryInfo directory = new DirectoryInfo(_folders[i]);
                directory.Delete(true);
                File.Delete($"{_folders[i]}.meta");
            }

            AssetDatabase.Refresh();
        }

        #endregion

        #region ClearFolder
        public static void ClearFolder(this string FolderName, Object RootFolder, FileFormatEnum FileFormat, SearchOption SearchItems = SearchOption.AllDirectories, SearchOption SearchFolder = SearchOption.AllDirectories)
        {
            string folderPath = GetFolderPath(FolderName, RootFolder, SearchFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchItems);

            foreach (string filePath in files)
            {
                File.Delete(filePath);
                File.Delete($"{filePath}.meta");
            }

            AssetDatabase.Refresh();
        }

        public static void ClearFolder(this string FolderName, string RootPath, FileFormatEnum FileFormat, SearchOption SearchItems = SearchOption.AllDirectories, SearchOption SearchFolder = SearchOption.AllDirectories)
        {
            string folderPath = GetFolderPath(FolderName, RootPath, SearchFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchItems);

            foreach (string filePath in files)
            {
                File.Delete(filePath);
                File.Delete($"{filePath}.meta");
            }

            AssetDatabase.Refresh();
        }

        public static void ClearFolder(this Object RootFolder, FileFormatEnum FileFormat, SearchOption SearchOption)
        {
            string folderPath = AssetDatabase.GetAssetPath(RootFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchOption);

            foreach (string filePath in files)
            {
                File.Delete(filePath);
                File.Delete($"{filePath}.meta");
            }

            AssetDatabase.Refresh();
        }

        public static void ClearFolder(this string RootPath, FileFormatEnum FileFormat, SearchOption SearchOption)
        {
            List<string> files = GetFilePathAsPerFormat(FileFormat, RootPath, SearchOption);

            foreach (string filePath in files)
            {
                File.Delete(filePath);
                File.Delete($"{filePath}.meta");
            }

            AssetDatabase.Refresh();
        }
        #endregion


        #region LoadFiles
        public static List<T> LoadFiles<T>(this List<T> FileList, string FolderName, Object RootFolder, FileFormatEnum FileFormat, SearchOption SearchItems = SearchOption.AllDirectories, SearchOption SearchFolder = SearchOption.AllDirectories) where T : Object
        {
            string folderPath = GetFolderPath(FolderName, RootFolder, SearchFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchItems);

            FileList.Clear();
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                FileList.Add(file);
            }

            return FileList;
        }

        public static List<T> LoadFiles<T>(this List<T> FileList, string FolderName, string RootPath, FileFormatEnum FileFormat, SearchOption SearchItems = SearchOption.AllDirectories, SearchOption SearchFolder = SearchOption.AllDirectories) where T : Object
        {
            string folderPath = GetFolderPath(FolderName, RootPath, SearchFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchItems);

            FileList.Clear();
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                FileList.Add(file);
            }

            return FileList;
        }

        public static List<T> LoadFiles<T>(this List<T> FileList, Object RootFolder, FileFormatEnum FileFormat, SearchOption SearchOption) where T : Object
        {
            string folderPath = AssetDatabase.GetAssetPath(RootFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchOption);

            FileList.Clear();
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                FileList.Add(file);
            }

            return FileList;
        }

        public static List<T> LoadFiles<T>(this List<T> FileList, string RootPath, FileFormatEnum FileFormat, SearchOption SearchOption) where T : Object
        {
            List<string> files = GetFilePathAsPerFormat(FileFormat, RootPath, SearchOption);

            FileList.Clear();
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                FileList.Add(file);
            }

            return FileList;
        }

        public static List<T> LoadFile<T>(this List<T> FileList, string FolderName, Object RootFolder, System.Type type, FileFormatEnum FileFormat, SearchOption SearchItems = SearchOption.AllDirectories, SearchOption SearchFolder = SearchOption.AllDirectories) where T : Object
        {
            string folderPath = GetFolderPath(FolderName, RootFolder, SearchFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchItems);

            FileList.Clear();
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file != null && file.GetType() == type)
                    FileList.Add(file);
            }

            FileList.RemoveMissingObjects();

            return FileList;
        }

        public static List<T> LoadFiles<T>(this List<T> FileList, string FolderName, string RootPath, System.Type type, FileFormatEnum FileFormat, SearchOption SearchItems = SearchOption.AllDirectories, SearchOption SearchFolder = SearchOption.AllDirectories) where T : Object
        {
            string folderPath = GetFolderPath(FolderName, RootPath, SearchFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchItems);

            FileList.Clear();
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file != null && file.GetType() == type)
                    FileList.Add(file);
            }

            FileList.RemoveMissingObjects();

            return FileList;
        }

        public static List<T> LoadFiles<T>(this List<T> FileList, Object RootFolder, System.Type type, FileFormatEnum FileFormat, SearchOption SearchOption) where T : Object
        {
            string folderPath = AssetDatabase.GetAssetPath(RootFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchOption);

            FileList.Clear();
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file != null && file.GetType() == type)
                    FileList.Add(file);
            }

            FileList.RemoveMissingObjects();

            return FileList;
        }

        public static List<T> LoadFiles<T>(this List<T> FileList, string RootPath, System.Type type, FileFormatEnum FileFormat, SearchOption SearchOption) where T : Object
        {
            List<string> files = GetFilePathAsPerFormat(FileFormat, RootPath, SearchOption);

            FileList.Clear();
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if(file != null && file.GetType() == type)
                    FileList.Add(file);
            }

            return FileList;
        }

        private static List<string> GetFilePathAsPerFormat(FileFormatEnum FileFormat, string FolderPath, SearchOption SearchOption)
        {
            List<string> filesPath = new List<string>();
            List<string> formatList = new List<string>();
            switch (FileFormat)
            {
                case FileFormatEnum.All:
                    formatList = new List<string>();
                    AddFileFormat(formatList, AUDIO_INDEX);
                    AddFileFormat(formatList, MESH_INDEX);
                    AddFileFormat(formatList, IHV_IMAGE_FORMAT_INDEX);
                    AddFileFormat(formatList, PLUGIN_INDEX);
                    AddFileFormat(formatList, SHADER_INDEX);
                    AddFileFormat(formatList, TEXT_INDEX);
                    AddFileFormat(formatList, TEXTURE_INDEX);
                    AddFileFormat(formatList, FONT_INDEX);
                    AddFileFormat(formatList, VIDEOCLIP_INDEX);
                    AddFileFormat(formatList, VISUAL_EFFECT_INDEX);
                    AddFileFormat(formatList, OTHER_INDEX);
                    break;

                case FileFormatEnum.Audio:
                    formatList = new List<string>();
                    AddFileFormat(formatList, AUDIO_INDEX);               
                    break;

                case FileFormatEnum.Mesh:
                    formatList = new List<string>();
                    AddFileFormat(formatList, MESH_INDEX);
                    break;

                case FileFormatEnum.IHVImageFormat:
                    formatList = new List<string>();
                    AddFileFormat(formatList, IHV_IMAGE_FORMAT_INDEX);
                    break;

                case FileFormatEnum.Native:
                    formatList = new List<string>();
                    AddFileFormat(formatList, NATIVE_INDEX);
                    break;

                case FileFormatEnum.Plugin:
                    formatList = new List<string>();
                    AddFileFormat(formatList, PLUGIN_INDEX);
                    break;

                case FileFormatEnum.Shader:
                    formatList = new List<string>();
                    AddFileFormat(formatList, SHADER_INDEX);
                    break;

                case FileFormatEnum.Text:
                    formatList = new List<string>();
                    AddFileFormat(formatList, TEXT_INDEX);
                    break;

                case FileFormatEnum.Texture:
                    formatList = new List<string>();
                    AddFileFormat(formatList, TEXTURE_INDEX);
                    break;

                case FileFormatEnum.Font:
                    formatList = new List<string>();
                    AddFileFormat(formatList, FONT_INDEX);
                    break;

                case FileFormatEnum.VideoClip:
                    formatList = new List<string>();
                    AddFileFormat(formatList, VIDEOCLIP_INDEX);
                    break;

                case FileFormatEnum.VisualEffect:
                    formatList = new List<string>();
                    AddFileFormat(formatList, VISUAL_EFFECT_INDEX);
                    break;

                case FileFormatEnum.Other:
                    formatList = new List<string>();
                    AddFileFormat(formatList, OTHER_INDEX);
                    break;

                default:
                    string[] formmat = FileFormat.ToString().Split(new string[] { "_" }, System.StringSplitOptions.RemoveEmptyEntries);
                    filesPath = Directory.GetFiles(FolderPath, $"*.{formmat[1]}", SearchOption).ToList();
                    break;
            }

            for (int i = 0; i < formatList.Count; i++)
            {
                List<string> newFilesPath = Directory.GetFiles(FolderPath, $"*.{formatList[i]}", SearchOption).ToList();
                for (int x = 0; x < newFilesPath.Count; x++)
                {
                    filesPath.Add(newFilesPath[x]);
                }
            }

            return filesPath;
        }

        private static void AddFileFormat(List<string> FormatList, Vector2 FormatEnumIndex)
        {
            int enumLength = System.Enum.GetValues(typeof(FileFormatEnum)).Length;
            FileFormatEnum fileFormatEnum = FileFormatEnum.All;

            int fileFormatIndex = (int) FormatEnumIndex.x;
            while (fileFormatIndex <= (int) FormatEnumIndex.y)
            {
                fileFormatEnum = (FileFormatEnum)fileFormatIndex;
                string[] formmat = fileFormatEnum.ToString().Split(new string[] { "_" }, System.StringSplitOptions.RemoveEmptyEntries);
                FormatList.Add(formmat[1]);

                fileFormatIndex++;
            }
        }
        #endregion

        #region LoadFile
        public static T LoadFile<T>(this string FileName, string FolderName, Object RootFolder, FileFormatEnum FileFormat, SearchOption SearchItems = SearchOption.AllDirectories, SearchOption SearchFolder = SearchOption.AllDirectories) where T : Object
        {
            string folderPath = GetFolderPath(FolderName, RootFolder, SearchFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchItems);

            T newFile = null;
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file.name == FileName)
                {
                    newFile = file;
                }
            }

            return newFile;
        }

        public static T LoadFile<T>(this string FileName, string FolderName, string RootPath, FileFormatEnum FileFormat, SearchOption SearchItems = SearchOption.AllDirectories, SearchOption SearchFolder = SearchOption.AllDirectories) where T : Object
        {
            string folderPath = GetFolderPath(FolderName, RootPath, SearchFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchItems);

            T newFile = null;
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file.name == FileName)
                {
                    newFile = file;
                }
            }

            return newFile;
        }

        public static T LoadFile<T>(this string FileName, Object RootFolder, FileFormatEnum FileFormat, SearchOption SearchOption) where T : Object
        {
            string folderPath = AssetDatabase.GetAssetPath(RootFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchOption);

            T newFile = null;
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file.name == FileName)
                {         
                    newFile = file;
                }
            }

            return newFile;
        }

        public static T LoadFile<T>(this string FileName, string RootPath, FileFormatEnum FileFormat, SearchOption SearchOption) where T : Object
        {
            List<string> files = GetFilePathAsPerFormat(FileFormat, RootPath, SearchOption);

            T newFile = null;
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file.name == FileName)
                {
                    newFile = file;
                }
            }

            return newFile;
        }


        public static T LoadFileType<T>(this string FileName, string FolderName, Object RootFolder, System.Type type, FileFormatEnum FileFormat, SearchOption SearchItems = SearchOption.AllDirectories, SearchOption SearchFolder = SearchOption.AllDirectories) where T : Object
        {
            string folderPath = GetFolderPath(FolderName, RootFolder, SearchFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchItems);

            T newFile = null;
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file.name == FileName && file.GetType() == type)
                {
                    newFile = file;
                }
            }

            return newFile;
        }

        public static T LoadFile<T>(this string FileName, string FolderName, string RootPath, System.Type type, FileFormatEnum FileFormat, SearchOption SearchItems = SearchOption.AllDirectories, SearchOption SearchFolder = SearchOption.AllDirectories) where T : Object
        {
            string folderPath = GetFolderPath(FolderName, RootPath, SearchFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchItems);

            T newFile = null;
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file.name == FileName && file.GetType() == type)
                {
                    newFile = file;
                }
            }

            return newFile;
        }

        public static T LoadFile<T>(this string FileName, Object RootFolder, System.Type type, FileFormatEnum FileFormat, SearchOption SearchOption) where T : Object
        {
            string folderPath = AssetDatabase.GetAssetPath(RootFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchOption);

            T newFile = null;
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file.name == FileName && file.GetType() == type)
                {
                    newFile = file;
                }
            }

            return newFile;
        }

        public static T LoadFile<T>(this string FileName, string RootPath, System.Type type, FileFormatEnum FileFormat, SearchOption SearchOption) where T : Object
        {
            List<string> files = GetFilePathAsPerFormat(FileFormat, RootPath, SearchOption);

            T newFile = null;
            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file.name == FileName && file.GetType() == type)
                {
                    newFile = file;
                }
            }

            return newFile;
        }
        #endregion


        #region DeleteFile
        public static void DeleteFile<T>(this string FileName, string FolderName, Object RootFolder, FileFormatEnum FileFormat, SearchOption SearchItems = SearchOption.AllDirectories, SearchOption SearchFolder = SearchOption.AllDirectories) where T : Object
        {
            string folderPath = GetFolderPath(FolderName, RootFolder, SearchFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchItems);

            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file.name == FileName)
                {
                    File.Delete(filePath);
                }
            }

            AssetDatabase.Refresh();
        }

        public static void DeleteFile<T>(this string FileName, string FolderName, string RootPath, FileFormatEnum FileFormat, SearchOption SearchItems = SearchOption.AllDirectories, SearchOption SearchFolder = SearchOption.AllDirectories) where T : Object
        {
            string folderPath = GetFolderPath(FolderName, RootPath, SearchFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchItems);

            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file.name == FileName)
                {
                    File.Delete(filePath);
                }
            }

            AssetDatabase.Refresh();
        }

        public static void DeleteFile<T>(this string FileName, Object RootFolder, FileFormatEnum FileFormat, SearchOption SearchOption) where T : Object
        {
            string folderPath = AssetDatabase.GetAssetPath(RootFolder);
            List<string> files = GetFilePathAsPerFormat(FileFormat, folderPath, SearchOption);

            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file.name == FileName)
                {
                    File.Delete(filePath);
                }
            }

            AssetDatabase.Refresh();
        }

        public static void DeleteFile<T>(this string FileName, string RootPath, FileFormatEnum FileFormat, SearchOption SearchOption) where T : Object
        {
            List<string> files = GetFilePathAsPerFormat(FileFormat, RootPath, SearchOption);

            foreach (string filePath in files)
            {
                T file = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
                if (file.name == FileName)
                {
                    File.Delete(filePath);
                }
            }

            AssetDatabase.Refresh();
        }
        #endregion  
    }
}
#endif