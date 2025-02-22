using UnityEngine;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [Header("Pipe Settings")]
    [SerializeField] private Boru pipePrefab;
    [SerializeField] private float pipeSpeed = 5f;
    [SerializeField] private float desiredPipeSpacing = 2f;
    [SerializeField] private float minHeight = -2f;
    [SerializeField] private float maxHeight = 2f;
    [SerializeField] private float xSpawnPosition = 10f;
    [SerializeField] private float destroyXPosition = -10f;
    
    [Header("Difficulty Settings")]
    [SerializeField] private float speedIncreaseRate = 0.1f; // Speed increase per second
    [SerializeField] private float maxPipeSpeed = 15f; // Maximum speed limit
    [SerializeField] private float gameTime; // Track how long the game has been running
    

    private float _timer;
    private bool _isGameActive = true;
    private float _currentPipeSpeed;
    private float _currentSpawnInterval;

    private List<Boru> _pipes = new();


    private void Start()
    {
        _currentPipeSpeed = pipeSpeed;
        gameTime = 0f;
        CalculateSpawnInterval();
        _timer = _currentSpawnInterval; // Start spawning immediately

    }

    private void Update()
    {
        if (!_isGameActive) return;

        gameTime += Time.deltaTime;
        UpdateDifficulty();

        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            SpawnPipe();
            _timer = _currentSpawnInterval;
        }
    }

    private void UpdateDifficulty()
    {
        _currentPipeSpeed = pipeSpeed + (int)(gameTime / 30) * speedIncreaseRate ;
        _currentPipeSpeed = Mathf.Clamp(_currentPipeSpeed, pipeSpeed, maxPipeSpeed);
        _pipes.ForEach(x=>x.UpdateSpeed(_currentPipeSpeed));
        
        CalculateSpawnInterval();
    }
    private void CalculateSpawnInterval()
    {
        _currentSpawnInterval = desiredPipeSpacing / _currentPipeSpeed;
    }
    private void SpawnPipe()
    {
        float randomHeight = Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(xSpawnPosition, randomHeight, 0);
        Boru newPipe = Instantiate(pipePrefab, spawnPosition, Quaternion.identity);
        _pipes.Add(newPipe);
        newPipe.Initialize(_currentPipeSpeed
            , destroyXPosition,(() =>
            {
                _pipes.Remove(newPipe);
            }));
    }

    public void StopGame()
    {
        _isGameActive = false;
        scoreManager.StopScoring();
    }

}
