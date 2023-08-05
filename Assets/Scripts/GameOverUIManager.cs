using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    public void OnPlayAgainClicked()
    {
        Utility.LoadBuildSettingsSceneByPath("Assets/Scenes/main.unity");
    }

    public void OnExitClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void OnNewPlayerClicked()
    {
        Utility.LoadBuildSettingsSceneByPath("Assets/Scenes/startMenu.unity");
    }    
}
