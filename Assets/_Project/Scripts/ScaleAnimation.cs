using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private float scaleAmount = 0.2f;    // How much to scale up/down
    [SerializeField] private float speed = 2f;            // Speed of the animation
    private Vector3 startScale;                           // Initial scale of the object
    
    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float scaleOffset = Mathf.Sin(Time.time * speed) * scaleAmount;
        transform.localScale = startScale + (Vector3.one * scaleOffset);
    }

}
