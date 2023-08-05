using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private const int ScoreTableSize = 10;

    [Serializable]
    public class ScoreData
    {
        public int Score;
        public string Player;
        public DateTime dateTime;
    }

    [Serializable]
    public class BestScoreTable
    {
        public List<ScoreData> ScoreList;
    }

    public int LastScore;
    public string LastScorer;
    public int LastScorePositionOnTable;
    public List<ScoreData> ScoreList = new List<ScoreData>();

    public int HighestScore {  get { return ScoreList.Count > 0 ? ScoreList[ScoreList.Count - 1].Score : 0; } }
    public string HighestScorer { get { return ScoreList.Count > 0 ? ScoreList[ScoreList.Count - 1].Player : ""; } }

    private static HighScore _highScore;

    public static HighScore Instance { get { return _highScore; } }
    

    void Awake()
    {
        if(_highScore == null)
        {
            _highScore = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadData();
    }

    public void LoadData()
    {
        var path = Utility.GetSaveDataFilename("scoreData");

        if (File.Exists(path))
        {
            var jsonString = File.ReadAllText(path);
            var saveData = JsonUtility.FromJson<BestScoreTable>(jsonString);
            ScoreList = saveData.ScoreList;
        }
        else
        {
            Debug.LogWarning($"File {path} does not exist");
        }
    }

    private void OnDestroy()
    {
        var jsonString = JsonUtility.ToJson(new BestScoreTable
        {
            ScoreList = ScoreList

        });
        File.WriteAllText(Utility.GetSaveDataFilename("scoreData"), jsonString);
    }       

    public void AddHighScore(string playerName, int newScore)
    {
        LastScore = newScore;
        LastScorer = playerName;
        
        if (newScore > 0)
        {
            var newScoreData = new ScoreData
            {
                Score = newScore,
                Player = playerName,
                dateTime = DateTime.Now,
            };

            ScoreList.Add(newScoreData);
            ScoreList.Sort( Comparson );
            if(ScoreList.Count > ScoreTableSize )
            {
                ScoreList.RemoveAt(0);
            }

            LastScorePositionOnTable = ScoreList.IndexOf(newScoreData);
        }
                
    }

    private int Comparson(ScoreData x, ScoreData y)
    {
        return ((x.Score - y.Score) * 10 )+ (x.dateTime > y.dateTime ? -1 : 1);
    }
}
