﻿using UnityEditor;
using UnityEngine;
using System;
using System.IO;

namespace AppCenterEditor
{
    [InitializeOnLoad]
    public static partial class AppCenterEditorHelper
    {
        public static string EDEX_NAME = "AppCenterEditorExtensions";
        public static string EDEX_ROOT = Path.Combine(Application.dataPath, "AppCenterEditorExtensions/Editor");
        public static string APPCENTER_SETTINGS_TYPENAME = "AppCenterSettings";
        public static string APPCENTER_WRAPPER_SDK_TYPENAME = "WrapperSdk";
        public static string DEFAULT_SDK_LOCATION = "Assets/AppCenter";
        public static string DEFAULT_SDK_LOCATION_PATH = Application.dataPath + "/AppCenter";
        public static string MSG_SPIN_BLOCK = "{\"useSpinner\":true, \"blockUi\":true }";
        public static string ANALYTICS_SDK_DOWNLOAD_PATH = "/Resources/AppCenterAnalyticsUnitySdk.unitypackage";
        public static string CRASHES_SDK_DOWNLOAD_PATH = "/Resources/AppCenterCrashesUnitySdk.unitypackage";
        public static string DISTRIBUTE_SDK_DOWNLOAD_PATH = "/Resources/AppCenterDistributeUnitySdk.unitypackage";
        public static string EDEX_UPGRADE_PATH = "/Resources/AppCenterUnityEditorExtensions.unitypackage";
        public static string EDEX_PACKAGES_PATH = "/Resources/MostRecentPackage.unitypackage";

        private static GUISkin _uiStyle;

        public static GUISkin uiStyle
        {
            get
            {
                if (_uiStyle != null)
                    return _uiStyle;
                _uiStyle = GetUiStyle();
                return _uiStyle;
            }
        }

        public static void SharedErrorCallback(string error)
        {
            AppCenterEditor.RaiseStateUpdate(AppCenterEditor.EdExStates.OnError, error);
        }

        private static GUISkin GetUiStyle()
        {
            var searchRoot = string.IsNullOrEmpty(EDEX_ROOT) ? Application.dataPath : EDEX_ROOT;
            string[] guiPaths;

            if (Directory.Exists(searchRoot))
            {
                guiPaths = FindGuiSkinPaths(searchRoot);
            }
            else
            {
                EDEX_ROOT = FindEdexRoot();
                guiPaths = FindGuiSkinPaths(EDEX_ROOT);
            }            
                
            foreach (var eachPath in guiPaths)
            {
                var loadPath = eachPath.Substring(eachPath.LastIndexOf("Assets" + Path.DirectorySeparatorChar, StringComparison.Ordinal));
                return (GUISkin) AssetDatabase.LoadAssetAtPath(loadPath, typeof(GUISkin));
            }

            return null;
        }

        private static string[] FindGuiSkinPaths(string searchPath)
        {
            return Directory.GetFiles(searchPath, "AppCenterStyles.guiskin", SearchOption.AllDirectories);
        }
        
        private static string FindEdexRoot()
        {
            var directoryList = Directory.GetDirectories(Application.dataPath, "*" + EDEX_NAME, SearchOption.AllDirectories);
            if (directoryList.Length == 0)
            {
                throw new DirectoryNotFoundException(EDEX_NAME + " not found");
            }

            var relativePath = directoryList[0].Substring(directoryList[0].LastIndexOf("Assets" + Path.DirectorySeparatorChar, StringComparison.Ordinal));
            return Path.Combine(relativePath, "Editor");
        }
    }
}