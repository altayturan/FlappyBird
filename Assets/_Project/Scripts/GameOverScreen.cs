using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text bestScoreText;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas gameStartCanvas;
    
    public int HighScore
    {
        get => PlayerPrefs.GetInt("HighScore", 0);
        set => PlayerPrefs.SetInt("HighScore", value);
    }
    public void OpenGameOverCanvas()
    {
        scoreText.text = ((int)scoreManager.CurrentScore).ToString();
        HighScore = Mathf.Max(HighScore, (int)scoreManager.CurrentScore);
        bestScoreText.text = HighScore.ToString();
        gameOverCanvas.enabled = true;
    }
    
    public void CloseGameOverCanvas()
    {
        gameOverCanvas.enabled = false;
        gameStartCanvas.enabled = true;
        
        var controller = FindObjectOfType<Controller>();
        var bird = FindObjectOfType<Bird>();
    
        if (controller != null)
        {
            controller.RestartGame();
        }
    
        if (bird != null)
        {
            bird.ResetBird();
        }
    }
}
