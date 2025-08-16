#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace GameTemplate.Scripts.Editor
{
#if UNITY_EDITOR
    [InitializeOnLoad]
    public static class StartAtFirstScene
    {
        static StartAtFirstScene()
        {
            EditorApplication.playModeStateChanged += LoadStartScene;
        }

        private static void LoadStartScene(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                if (EditorSceneManager.GetActiveScene().name != "Bootstrap")
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene("Assets/GameTemplate/Scenes/Bootstrap.unity");
                }
            }
        }
    }
#endif
}