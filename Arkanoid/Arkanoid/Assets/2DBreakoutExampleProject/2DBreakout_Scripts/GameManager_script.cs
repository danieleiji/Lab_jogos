using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_script : MonoBehaviour
{
    public GameObject gameOverPanel;
    public static bool gameOver = false;
    public string nextLevelName = "LV2"; // <--- IMPORTANTE: Vai ser alterado dinamicamente.
    public string menuSceneName = "Menu";
    public KeyCode cheatKey = KeyCode.F12;

    private int totalBricks;
    private int destroyedBricks = 0;

    public Text scoreText;
    public Text highScoreText;
    private int currentScore = 0;
    private string highScoreKey = "HighScore";

    void Start()
    {
        totalBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        destroyedBricks = 0;
        UpdateScoreText();
        LoadHighScore();

        // Define o nextLevelName com base na cena atual.  ISSO É IMPORTANTE!
        if (SceneManager.GetActiveScene().name == "LV1")
        {
            nextLevelName = "LV2";
        }
        else if (SceneManager.GetActiveScene().name == "LV2")
        {
            nextLevelName = "LV3";
        }
        // Adicione mais else if aqui se você tiver mais níveis (LV4, LV5, etc.)
        else
        {
            nextLevelName = menuSceneName; // Volta para o menu se não houver mais níveis.
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals(menuSceneName))
        {
            return;
        }

        if (Input.GetKeyDown(cheatKey))
        {
            LoadNextLevel();
        }

        if (Input.GetButtonDown("Jump") && gameOver == true)
        {
            gameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void BrickDestroyed(int brickScore)
    {
        destroyedBricks++;
        currentScore += brickScore;
        UpdateScoreText();
        CheckForNewHighScore();

        if (destroyedBricks >= totalBricks)
        {
            LoadNextLevel();
        }
    }


    private void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogWarning("Next Level Name not set!");
        }

    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    private void LoadHighScore()
    {
        if (PlayerPrefs.HasKey(highScoreKey))
        {
            currentScore = PlayerPrefs.GetInt(highScoreKey);
            UpdateScoreText();
        }
    }

    private void CheckForNewHighScore()
    {
        if (currentScore > PlayerPrefs.GetInt(highScoreKey, 0))
        {
            PlayerPrefs.SetInt(highScoreKey, currentScore);
            PlayerPrefs.Save();
        }
    }
}