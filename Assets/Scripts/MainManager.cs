using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public GameObject returnToMenuButton;

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    public GameObject NewHighScoreText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
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

        HighScoreText.text = $"High Score: {HighscoreManager.instance.highScoreString}"; // Show high score at top of screen
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
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score: {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        returnToMenuButton.SetActive(true);

        // Check for new high score and update value
        if (m_Points > HighscoreManager.instance.highScoreValue)
        {
            HighscoreManager.instance.UpdateHighScore(m_Points, HighscoreManager.instance.userName);
            NewHighScoreText.SetActive(true); // Show game over text celebriting new high score
            HighScoreText.text = $"High Score: {HighscoreManager.instance.highScoreString}"; // Update high score display
        }
        else
        {
            GameOverText.SetActive(true); // Show regular game over text
        }
    }

    // Return to menu button so the user can change their name
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
