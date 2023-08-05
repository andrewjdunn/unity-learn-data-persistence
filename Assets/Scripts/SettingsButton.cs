using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public void OnClicked()
    {
        Utility.LoadBuildSettingsSceneByPath("Assets/Scenes/Settings.unity");
    }
}
