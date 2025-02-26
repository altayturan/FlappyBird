using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")] [SerializeField]
    private TMP_Text scoreText;

    [SerializeField] private float pointsPerSecond = 1f;
    [SerializeField] private float bonusMultiplier = 2f; // Bonus multiplier for every 30 seconds
    [SerializeField] private float scoreCountSpeed = 10f; // Speed of counting animation

    private float _currentScore;

    public float CurrentScore => _currentScore;

    private void Start()
    {
        _currentScore = 0;
        UpdateScoreDisplay();
    }

    public void ResetScore()
    {
        _currentScore = 0;
        UpdateScoreDisplay();
    }
    private void UpdateScoreDisplay()
    {
        scoreText.text = $"{Mathf.Floor(_currentScore)}";
    }

    public void GiveScore()
    {
        _currentScore += 20;
        UpdateScoreDisplay();
        AudioController.Instance.PlayScoreSound();
    }
}