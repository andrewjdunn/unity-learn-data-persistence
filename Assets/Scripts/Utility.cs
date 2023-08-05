using UnityEngine;
using UnityEngine.SceneManagement;

public class Utility
{
    public static void LoadBuildSettingsSceneByPath(string path)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (SceneUtility.GetScenePathByBuildIndex(i) == path)
            {
                SceneManager.LoadScene(i);
                return;
            }
        }

        Debug.LogError($"No Scene with path {path}");
    }

    public static string GetSaveDataFilename(string name)
    {
        return $"{Application.persistentDataPath}/{name}.json";
    }

}
