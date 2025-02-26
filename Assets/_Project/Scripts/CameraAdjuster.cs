using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    [SerializeField] private float targetAspectRatio = 9f / 16f;
    private void Start()
    {
        if (Camera.main != null) Camera.main.orthographicSize = CalculateOrthographicSize();

        Application.targetFrameRate = 60;
    }

    private float CalculateOrthographicSize()
    {
        var windowAspect = Screen.width / (float)Screen.height;
        var scaleHeight = windowAspect / targetAspectRatio;

        if (scaleHeight < 1.0f)
        {
            return Camera.main.orthographicSize / scaleHeight;
        }

        return Camera.main.orthographicSize;
    }
}
