using UnityEngine;
using System.Collections.Generic;

public class GroundMovement : MonoBehaviour
{
    [SerializeField] private List<GameObject> grounds = new List<GameObject>();
    [SerializeField] private Controller controller;
    
    private float groundWidth;
    private float resetXPosition;
    private Vector3[] initialPositions;

    private void Start()
    {
        if (grounds.Count == 0)
        {
            Debug.LogWarning("No backgrounds assigned to the Background Manager!");
            return;
        }
        groundWidth = grounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
        initialPositions = new Vector3[grounds.Count];
        for (int i = 0; i < grounds.Count; i++)
        {
            initialPositions[i] = grounds[i].transform.position;
        }
        
        resetXPosition = initialPositions[0].x - groundWidth;
    }

    private void Update()
    {
        for (int i = 0; i < grounds.Count; i++)
        {
            GameObject bg = grounds[i];
            bg.transform.Translate(Vector3.left * (controller.CurrentPipeSpeed * Time.deltaTime));

            if (bg.transform.position.x <= resetXPosition)
            {
                float rightmostX = float.MinValue;
                foreach (GameObject checkBg in grounds)
                {
                    if (checkBg != bg && checkBg.transform.position.x > rightmostX)
                    {
                        rightmostX = checkBg.transform.position.x;
                    }
                }

                Vector3 newPosition = bg.transform.position;
                newPosition.x = rightmostX + groundWidth;
                newPosition.y = initialPositions[i].y;
                newPosition.z = initialPositions[i].z;
                bg.transform.position = newPosition;
            }
        }
    }
    
    public void ResetPositions()
    {
        for (int i = 0; i < grounds.Count; i++)
        {
            if (grounds[i] != null)
            {
                grounds[i].transform.position = initialPositions[i];
            }
        }
    }
}
