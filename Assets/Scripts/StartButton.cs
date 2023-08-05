using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button button;
    CurrentPlayer player;

    public void Start()
    {
        player = GameObject.Find("DataObject").GetComponent<CurrentPlayer>();
        player.CurrentPlayerUpdated += Player_CurrentPlayerUpdated;
        button = gameObject.GetComponentInChildren<Button>();
    }

    private void OnDestroy()
    {
        player.CurrentPlayerUpdated -= Player_CurrentPlayerUpdated;
    }

    private void Player_CurrentPlayerUpdated(CurrentPlayer sender, System.EventArgs e)
    {
        var buttonActive = player.CurrentPlayerName != null && player.CurrentPlayerName.Length > 0;
        button.interactable = buttonActive;
    }

    public void StartGameClicked()
    {
        SceneManager.LoadScene(1);
    }
}
