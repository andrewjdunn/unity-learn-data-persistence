using TMPro;
using UnityEngine;


public class LastScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI LastSctoreText;
    void Start()
    {
        var newHighScore = HighScore.Instance.LastScorePositionOnTable >= 0;
        LastSctoreText.text = $"{HighScore.Instance.LastScorer} scored {HighScore.Instance.LastScore} {(newHighScore ? "New high score!" : "")}";
        LastSctoreText.color = newHighScore ? Color.green : Color.white;
    }    
}
