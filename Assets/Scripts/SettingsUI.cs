using UnityEngine;
using UnityEngine.UI;


public class SettingsUI : MonoBehaviour
{
    public Toggle multihitBricks;
    public Toggle fasterBall;
    public Toggle endGameAfterLastBrick;
    public Button saveButton;

    public readonly Settings.SettingsData SettingsPageSettings = new Settings.SettingsData();

    public void Awake()
    {
        SettingsPageSettings.Copy(Settings.Instance.ActiveSettings);
        multihitBricks.isOn = SettingsPageSettings.multiHitBricks;
        fasterBall.isOn = SettingsPageSettings.fasterBall;
        endGameAfterLastBrick.isOn = SettingsPageSettings.endAfterLastBrick;
    }

    public void OnMultiHitToggled()
    {
        SettingsPageSettings.multiHitBricks = multihitBricks.isOn;
        CheckSaveButton();
    }

    public void OnFasterBallToggled()
    {
        SettingsPageSettings.fasterBall = fasterBall.isOn;
        CheckSaveButton();
    }

    public void OnEndAfterLastBrickToggled()
    {
        SettingsPageSettings.endAfterLastBrick = endGameAfterLastBrick.isOn;
        CheckSaveButton();
    }

    public void OnSaveClicked()
    {
        Settings.Instance.SaveData(SettingsPageSettings);
        Utility.LoadBuildSettingsSceneByPath("Assets/Scenes/startMenu.unity");
    }

    public void OnClickCancel()
    {
        SettingsPageSettings.Copy(Settings.Instance.ActiveSettings);
        Utility.LoadBuildSettingsSceneByPath("Assets/Scenes/startMenu.unity");
    }

    private void CheckSaveButton()
    {
        saveButton.interactable = !SettingsPageSettings.Equals(Settings.Instance.ActiveSettings);
    }
}
