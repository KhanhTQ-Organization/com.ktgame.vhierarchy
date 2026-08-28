using UnityEditor;
using UnityEngine;
using VHierarchy;

namespace com.ktgame.vhierarchy.editor
{
    public static class VHierarchyDataMenu
    {
        [MenuItem("Ktgame/vHierarchy/Find or Create Data", priority = 1000)]
        public static void OpenSettings()
        {
            // 1. Ensure Folder Exists
            if (!AssetDatabase.IsValidFolder("Assets/Editor"))
            {
                AssetDatabase.CreateFolder("Assets", "Editor");
            }

            // 2. Find or Create Data
            string[] dataGuids = AssetDatabase.FindAssets("t:VHierarchyData");
            VHierarchyData data = null;

            if (dataGuids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(dataGuids[0]);
                data = AssetDatabase.LoadAssetAtPath<VHierarchyData>(path);
                Debug.Log($"[vHierarchy] Found Data at: {path}");
            }
            else
            {
                data = ScriptableObject.CreateInstance<VHierarchyData>();
                AssetDatabase.CreateAsset(data, "Assets/Editor/vHierarchy Data.asset");
                Debug.Log("[vHierarchy] Created new Data at Assets/Editor/vHierarchy Data.asset");
            }

            // 3. Find or Create Palette
            string[] paletteGuids = AssetDatabase.FindAssets("t:VHierarchyPalette");
            VHierarchyPalette palette = null;

            if (paletteGuids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(paletteGuids[0]);
                palette = AssetDatabase.LoadAssetAtPath<VHierarchyPalette>(path);
                Debug.Log($"[vHierarchy] Found Palette at: {path}");
            }
            else
            {
                palette = ScriptableObject.CreateInstance<VHierarchyPalette>();
                AssetDatabase.CreateAsset(palette, "Assets/Editor/vHierarchy Palette.asset");
                Debug.Log("[vHierarchy] Created new Palette at Assets/Editor/vHierarchy Palette.asset");
            }

            AssetDatabase.SaveAssets();

            // 4. Focus Data in Inspector
            if (data != null)
            {
                Selection.activeObject = data;
                EditorGUIUtility.PingObject(data);
            }
        }
    }
}
