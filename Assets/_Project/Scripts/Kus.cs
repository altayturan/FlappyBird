using UnityEngine;
using UnityEngine.SceneManagement;

public class Kus : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;   
    [SerializeField] private float gravity = -9.8f;   
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float maxRotationAngle = 35f;
    [SerializeField] private float minRotationAngle = -90f; 

    
    private Vector2 _velocity;
    private readonly bool _isAlive = true;
    private float _currentRotation;

    private void Start()
    {
        _velocity = Vector2.zero;
        _currentRotation = 0f;

    }

    private void Update()
    {
        if (!_isAlive) return;

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

}