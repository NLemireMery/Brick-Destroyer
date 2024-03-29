using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public GameObject GameOverText;
    public Text NameScoreText;
    public Text HighestScoreText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
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

    }

    private void Update()
    {
        /* Prevent errors if the game is played directly from Main scene */
        if (DataManager.Instance == null)
        {
            NameScoreText.text = "Stranger | Your Score: " + m_Points;
            HighestScoreText.text = "Where is the high score?!";
        }
        else
        {
            NameScoreText.text = DataManager.Instance.playerName.text + " | Your Score: " + m_Points;
            if(System.String.IsNullOrEmpty(DataManager.Instance.bestPlayerName))
            {
                HighestScoreText.text = "No high scores yet!";
            }
            else
            {
                HighestScoreText.text = DataManager.Instance.bestPlayerName + " got the highest score: " + DataManager.Instance.playerFinalScore;
            }
        }

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
            if (DataManager.Instance != null)
            {
                /* When game over, save the score data in the DataManager */
                if(DataManager.Instance.playerFinalScore < m_Points)
                {
                    DataManager.Instance.bestPlayerName = DataManager.Instance.playerName.text;
                    DataManager.Instance.playerFinalScore = m_Points;
                    DataManager.Instance.SaveNameHighScore();
                };
            }

            if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(0);
                }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
