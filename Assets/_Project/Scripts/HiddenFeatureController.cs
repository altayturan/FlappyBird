using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HiddenFeatureController : MonoBehaviour
{
    [SerializeField] private VolumeProfile postProcessVolume;

    private ColorAdjustments colorAdjustment;
    private float currentHue = 0f;
    private int clickCount = 0;
    private float lastClickTime;
    private const float CLICK_TIMEOUT = 1f; // Reset clicks if no click within 1 second
    private const int REQUIRED_CLICKS = 10;
    private bool isPostProcessingEnabled = false;

    void Start()
    {
        // Get the ColorGrading effect from post process volume
        postProcessVolume.TryGet(out colorAdjustment);
        // Initially disable post processing
        Camera.main.GetUniversalAdditionalCameraData().renderPostProcessing = false;
    }

    void Update()
    {
        // Check if enough time has passed without clicks to reset
        if (Time.time - lastClickTime > CLICK_TIMEOUT)
        {
            clickCount = 0;
        }

        
    }

    private IEnumerator ColorChange()
    {
        while (isPostProcessingEnabled)
        {
            currentHue = (currentHue + 3f) % 180f; // Loop between 0 and 180
            colorAdjustment.hueShift.value = currentHue;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void OnScoreTextClicked()
    {
        lastClickTime = Time.time;
        clickCount++;

        if (clickCount >= REQUIRED_CLICKS)
        {
            if (!isPostProcessingEnabled)
            {
                // Enable post processing on first achievement
                Camera.main.GetUniversalAdditionalCameraData().renderPostProcessing = true;
                isPostProcessingEnabled = true;
                Invoke(nameof(StopProcessing),10f);
                StartCoroutine(ColorChange());
            }

     
            
            // Reset click count to allow continuous interaction
            clickCount = 0;
        }
    }

    public void StopProcessing()
    {
        isPostProcessingEnabled = false;
        Camera.main.GetUniversalAdditionalCameraData().renderPostProcessing = false;

    }

}
