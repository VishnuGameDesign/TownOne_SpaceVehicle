using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float gameTime = 120f; 
    public int vehiclesRepaired = 0;
    public int credits = 0;

    public TextMeshProUGUI timerText; 
    public TextMeshProUGUI scoreText; 

    public bool gameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (!gameOver)
        {
            gameTime -= Time.deltaTime;

            if (gameTime <= 0f)
            {
                gameTime = 0f;
                gameOver = true;
                GameOver();
            }

            UpdateTimerUI();
        }
    }

    public void AddVehicleRepaired()
    {
        vehiclesRepaired++;
        credits += 1000; 
        UpdateScoreUI();
    }

    void UpdateTimerUI()
    {
        // Format time as minutes:seconds
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Ƶ " + credits.ToString();
    }

    void GameOver()
    {
        // Load the Game Over scene
        SceneManager.LoadScene("GameOver"); 
    }

    public void ResetGame()
    {
        gameTime = 120f;
        vehiclesRepaired = 0;
        credits = 0;
        gameOver = false;
    }
}
