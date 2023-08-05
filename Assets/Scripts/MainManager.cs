using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighSoreText;

    private CurrentPlayer player;
    
    private bool m_Started = false;
    private int m_Points;    
    
    void Start()
    {
        player = GameObject.Find("DataObject").GetComponent<CurrentPlayer>();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        AddPoint(0);
        UpdateHighScore();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }

        if (Settings.Instance.ActiveSettings.endAfterLastBrick)
        {
            var bricks = GameObject.FindGameObjectsWithTag("Brick");
            if (bricks.Length == 0)
            {
                AddPoint(50);
                GameOver();
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {player.CurrentPlayerName} : {m_Points}";
    }

    public void GameOver()
    {
        HighScore.Instance.AddHighScore(player.CurrentPlayerName, m_Points);
        Utility.LoadBuildSettingsSceneByPath("Assets/Scenes/GameOver.unity");
    }

    private void UpdateHighScore()
    {
        HighSoreText.text = $"Best Score : {HighScore.Instance.HighestScorer} : {HighScore.Instance.HighestScore}";
    }
}
