using System;
using System.IO;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [Serializable]
    public class SettingsData
    {
        public bool multiHitBricks;
        public bool fasterBall;
        public bool endAfterLastBrick;

        public void Copy(SettingsData other)
        {
            multiHitBricks = other.multiHitBricks;
            fasterBall = other.fasterBall;
            endAfterLastBrick = other.endAfterLastBrick;
        }

        public override bool Equals(object obj)
        {
            return obj is SettingsData otherData && otherData.fasterBall == fasterBall
                    && otherData.multiHitBricks == multiHitBricks
                    && otherData.endAfterLastBrick == endAfterLastBrick;
        }

        public override int GetHashCode()
        {
            return fasterBall.GetHashCode() 
                | endAfterLastBrick.GetHashCode() << 1 
                | multiHitBricks.GetHashCode() << 2;  
        }
    }

    private static Settings _settings;
    public static Settings Instance { get { return _settings; } }

    public readonly SettingsData ActiveSettings = new SettingsData();

    void Awake()
    {
        if (_settings == null)
        {
            _settings = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadData();
    }

    private void LoadData()
    {
        var path = Utility.GetSaveDataFilename("settings");
        if (File.Exists(path))
        {
            var jsonString = File.ReadAllText(path);
            ActiveSettings.Copy(JsonUtility.FromJson<SettingsData>(jsonString));
        }
        else
        {
            Debug.LogWarning($"File {path} does not exist loading default");
            ActiveSettings.multiHitBricks = false;
            ActiveSettings.fasterBall = false;
            ActiveSettings.endAfterLastBrick = false;
        }
    }

    public void SaveData(SettingsData newSettings)
    {
        var jsonString = JsonUtility.ToJson(newSettings);
        File.WriteAllText(Utility.GetSaveDataFilename("settings"), jsonString);
        ActiveSettings.Copy(newSettings);
    }
}
