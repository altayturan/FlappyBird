using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float pointsPerSecond = 1f;
    [SerializeField] private float bonusMultiplier = 2f; // Bonus multiplier for every 30 seconds
    [SerializeField] private float scoreCountSpeed = 10f; // Speed of counting animation

    private float _currentScore;
    private float _displayedScore;
    private bool _isScoring = true;

    public float CurrentScore => _currentScore;

    private void Start()
    {
        _currentScore = 0;
        _displayedScore = 0;
        UpdateScoreDisplay();
    }

    public void ResetScore()
    {
        _currentScore = 0;
        _displayedScore = 0;
        _isScoring = true;
        UpdateScoreDisplay();
    }

    private void Update()
    {
        if (!_isScoring) return;
        
        // Increase actual score based on time
        float timeBonus = (1 + (int)(Time.time / 30) * (bonusMultiplier - 1));
        _currentScore += pointsPerSecond * Time.deltaTime * timeBonus;

        // Smoothly animate the displayed score
        if (_displayedScore < _currentScore)
        {
            _displayedScore = Mathf.MoveTowards(_displayedScore, _currentScore, scoreCountSpeed * Time.deltaTime);
            UpdateScoreDisplay();
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{Mathf.Floor(_displayedScore)}";
        }
    }

    public void StopScoring()
    {
        _isScoring = false;
        StartCoroutine(AnimateRemainingScore());
    }

    private System.Collections.IEnumerator AnimateRemainingScore()
    {
        while (_displayedScore < _currentScore)
        {
            _displayedScore = Mathf.MoveTowards(_displayedScore, _currentScore, scoreCountSpeed * Time.deltaTime);
            UpdateScoreDisplay();
            yield return null;
        }
    }

    public int GetCurrentScore()
    {
        return Mathf.FloorToInt(_currentScore);
    }
}