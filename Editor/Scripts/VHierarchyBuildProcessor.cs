using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;
using VHierarchy;

namespace com.ktgame.vhierarchy.Editor
{
    /// <summary>
    /// This processor runs just before the scene is compiled into the build.
    /// It finds and destroys the vHierarchy Data Component to prevent
    /// "Missing (Mono Script)" warnings in the final build.
    /// </summary>
    public class VHierarchyBuildProcessor : IProcessSceneWithReport
    {
        public int callbackOrder => 0;

        public void OnProcessScene(Scene scene, BuildReport report)
        {
            // report is null when just entering Play Mode in the Editor
            // We ONLY want to strip this during an actual Build
            if (report == null) return;

            var components = Object.FindObjectsOfType<VHierarchyDataComponent>(true);
            foreach (var comp in components)
            {
                if (comp != null && comp.gameObject != null)
                {
                    Object.DestroyImmediate(comp.gameObject);
                }
            }
        }
    }
}
