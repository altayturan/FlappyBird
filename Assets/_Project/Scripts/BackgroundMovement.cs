using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private List<GameObject> backgrounds = new List<GameObject>();
    
    private float backgroundWidth;
    private float resetXPosition;
    private Vector3[] initialPositions;

    private void Start()
    {
        if (backgrounds.Count == 0)
        {
            Debug.LogWarning("No backgrounds assigned to the Background Manager!");
            return;
        }
        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
        initialPositions = new Vector3[backgrounds.Count];
        for (int i = 0; i < backgrounds.Count; i++)
        {
            initialPositions[i] = backgrounds[i].transform.position;
        }
        
        resetXPosition = initialPositions[0].x - backgroundWidth;
    }

    private void Update()
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            GameObject bg = backgrounds[i];
            bg.transform.Translate(Vector3.left * (scrollSpeed * Time.deltaTime));

            if (bg.transform.position.x <= resetXPosition)
            {
                float rightmostX = float.MinValue;
                foreach (GameObject checkBg in backgrounds)
                {
                    if (checkBg != bg && checkBg.transform.position.x > rightmostX)
                    {
                        rightmostX = checkBg.transform.position.x;
                    }
                }

                Vector3 newPosition = bg.transform.position;
                newPosition.x = rightmostX + backgroundWidth;
                newPosition.y = initialPositions[i].y;
                newPosition.z = initialPositions[i].z;
                bg.transform.position = newPosition;
            }
        }
    }

    public void ResetPositions()
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            if (backgrounds[i] != null)
            {
                backgrounds[i].transform.position = initialPositions[i];
            }
        }
    }
}
