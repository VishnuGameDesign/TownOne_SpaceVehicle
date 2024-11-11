using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText; 

    void Start()
    {
        int finalScore = GameManager.Instance.credits;
        finalScoreText.text = "Final Score: Ƶ " + finalScore.ToString();
    }

    public void RestartGame()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("MainScene"); 
    }

    public void ExitGame()
    {
        
    }
}
