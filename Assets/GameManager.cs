using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float gameTime = 0f;
    public int vehiclesRepaired = 0;
    public int credits = 0;

    public TextMeshProUGUI timerText; 
    public TextMeshProUGUI scoreText; 

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        UpdateTimerUI();
    }

    public void AddVehicleRepaired()
    {
        vehiclesRepaired++;
        credits += 1000; // Add 1000 credits per repair
        UpdateScoreUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Ƶ " + credits.ToString();
    }
}
