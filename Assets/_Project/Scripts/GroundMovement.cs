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
            
            // Ensure proper initial spacing
            if (i > 0)
            {
                Vector3 position = initialPositions[0];
                position.x += groundWidth * i;
                grounds[i].transform.position = position;
                initialPositions[i] = position;
            }

        }
        
        resetXPosition = initialPositions[0].x - groundWidth;
    }

    private void Update()
    {
        for (int i = 0; i < grounds.Count; i++)
        {
            GameObject ground = grounds[i];
            ground.transform.Translate(Vector3.left * (controller.CurrentPipeSpeed * Time.deltaTime));

            if (ground.transform.position.x <= resetXPosition)
            {
                GameObject rightmostGround = grounds[0];
                foreach (GameObject other in grounds)
                {
                    if (other.transform.position.x > rightmostGround.transform.position.x)
                    {
                        rightmostGround = other;
                    }
                }


                // Position exactly one width away from the rightmost piece
                Vector3 newPosition = ground.transform.position;
                newPosition.x = groundWidth*3f;
                newPosition.y = initialPositions[i].y;
                newPosition.z = initialPositions[i].z;
                ground.transform.position = newPosition;

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
