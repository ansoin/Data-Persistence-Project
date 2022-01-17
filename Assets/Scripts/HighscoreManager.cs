using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager instance;
    public string userName;
    public int highScoreValue;
    public string highScoreString;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();

        Debug.Log("Name is currently: " + userName);
    }

    public void SetName(string s)
    {
        userName = s;
    }

    // Record high score
    public void UpdateHighScore(int newScore, string highScoreUser)
    {
        highScoreValue = newScore;
        highScoreString = highScoreUser + ": " + newScore.ToString();

        SaveHighScore(highScoreString, newScore);
    }

    [System.Serializable]
    class SaveData
    {
        public string highScoreText;
        public int highScoreNumber;
    }

    // Save high score to Json
    public void SaveHighScore(string scoreText, int scoreNumber)
    {
        SaveData data = new SaveData();
        data.highScoreText = scoreText;
        data.highScoreNumber = scoreNumber;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // Load high score from Json, if available
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreString = data.highScoreText;
            highScoreValue = data.highScoreNumber;
        }
    }
}
