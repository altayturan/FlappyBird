using System;
using UnityEngine;

public class Boru : MonoBehaviour
{
    private float _speed;
    private float _destroyXPosition;
    private Action _onDestroy;
    
    public void Initialize(float moveSpeed, float destroyPos, Action onDestroy)
    {
        _speed = moveSpeed;
        _destroyXPosition = destroyPos;
        _onDestroy = onDestroy;
    }

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
        if (transform.position.x < _destroyXPosition)
        {
            _onDestroy?.Invoke();
            Destroy(gameObject);
        }
    }

    public void UpdateSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    

}
