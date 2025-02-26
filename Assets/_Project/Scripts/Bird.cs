using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float maxRotationAngle = 35f;
    [SerializeField] private float minRotationAngle = -90f;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private Controller controller;
    [SerializeField] private GameOverScreen gameOverScreen;

    private Vector2 _velocity;
    private readonly bool _isAlive = true;
    private float _currentRotation;
    private Vector2 _initialPosition;

    private void Start()
    {
        _velocity = Vector2.zero;
        _currentRotation = 0f;
        _initialPosition = transform.position;
    }

    private void Update()
    {
        if (!_isAlive || !controller.IsGameActive) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        _velocity.y += gravity * Time.deltaTime;
        transform.position += (Vector3)_velocity * Time.deltaTime;
        UpdateRotation();
    }

    private void Jump()
    {
        _velocity.y = jumpForce;
        AudioController.Instance.PlayWingSound();
    }

    private void UpdateRotation()
    {
        if (_velocity.y < 0)
        {
            _currentRotation -= rotationSpeed * Time.deltaTime;
            _currentRotation = Mathf.Clamp(_currentRotation, minRotationAngle, maxRotationAngle);
        }
        else
        {
            _currentRotation += rotationSpeed * Time.deltaTime * 5;
            _currentRotation = Mathf.Clamp(_currentRotation, minRotationAngle, maxRotationAngle);
        }

        transform.rotation = Quaternion.Euler(0, 0, _currentRotation);
    }

    public void ResetBird()
    {
        // Reset position to starting position
        transform.position = _initialPosition; // Or your desired starting position
        transform.rotation = Quaternion.identity;

        // Reset physics
        _velocity = Vector2.zero;
        _currentRotation = 0f;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Pipe") || other.collider.CompareTag("Ground"))
        {
            controller.StopGame();
            gameOverScreen.OpenGameOverCanvas();
            AudioController.Instance.PlayDieSound();
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScoreArea"))
        {
            scoreManager.GiveScore();
        }
    }
}