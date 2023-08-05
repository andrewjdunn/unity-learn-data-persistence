using System.Linq;
using TMPro;
using UnityEngine;

public class BestScoreTable : MonoBehaviour
{

    public GameObject BestScoreTablePanel;
    void Start()
    {
        var scores = BestScoreTablePanel.gameObject.GetComponentsInChildren<TextMeshProUGUI>(true);

        var tableIndex = HighScore.Instance.ScoreList.Count;
        foreach(var scoreData in HighScore.Instance.ScoreList)
        {

            var scoreName = $"Score{tableIndex}";
            var nameName = $"Name{tableIndex}";
            var posName = $"Pos{tableIndex}";

            var pos = scores.First(t => t.name == posName);
            var name = scores.First(t => t.name == nameName);
            var score = scores.First(t => t.name == scoreName);

            pos.gameObject.SetActive(true);
            name.gameObject.SetActive(true);
            score.gameObject.SetActive(true);

            name.text = scoreData.Player;
            score.text = scoreData.Score.ToString();
            HighlightLastScoreInTable(tableIndex, pos, name, score);

            tableIndex--;
        }

    }

    private static void HighlightLastScoreInTable(int tableIndex, TextMeshProUGUI pos, TextMeshProUGUI name, TextMeshProUGUI score)
    {
        if (HighScore.Instance.ScoreList.Count - HighScore.Instance.LastScorePositionOnTable == tableIndex)
        {
            pos.color = Color.yellow;
            name.color = Color.yellow;
            score.color = Color.yellow;
        }
        else
        {
            pos.color = Color.white;
            name.color = Color.white;
            score.color = Color.white;
        }
    }
}
