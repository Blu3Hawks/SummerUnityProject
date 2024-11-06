using System.Collections;
using UnityEngine;

public class ScreenOrientationHandler : MonoBehaviour
{
    void Start()
    {
        AdjustLayoutForOrientation();
    }

    void Update()
    {
        AdjustLayoutForOrientation();
    }

    private void AdjustLayoutForOrientation()
    {
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            Screen.orientation = ScreenOrientation.PortraitUpsideDown;
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
        }
    }
}
