using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Light))]
public class ARLightEstimation : MonoBehaviour
{
    [SerializeField] private Light arLight = null;
    [SerializeField] private ARCameraManager arCameraManager = null;

    private void OnEnable()
    {
        Assert.IsNotNull(arCameraManager, "ARLightEstimation's arCameraManager is not assigned!");
        if (arCameraManager == null)
        {
            return;
        }

        arCameraManager.frameReceived += FrameChanged;
    }

    private void OnDisable()
    {
        if (arCameraManager == null)
        {
            return;
        }

        arCameraManager.frameReceived -= FrameChanged;
    }

    private void FrameChanged(ARCameraFrameEventArgs args)
    {
        Assert.IsNotNull(arLight, "ARLightEstimation's arLight is not assigned!");

        if (args.lightEstimation.averageBrightness.HasValue)
        {
            arLight.intensity = args.lightEstimation.averageBrightness.Value;
        }

        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            arLight.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
        }

        if (args.lightEstimation.colorCorrection.HasValue)
        {
            arLight.color = args.lightEstimation.colorCorrection.Value;
        }
    }
}