using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RotationHandler : MonoBehaviour
{
    [Header("Rotation Values")]
    public float duration = 1.0f;
    private float currentTimer;
    [Header("Rotation Types")]
    [SerializeField] private Quaternion landscapeLeft;
    [SerializeField] private Quaternion landscapeRight;
    [SerializeField] private Quaternion portraitUpsideDown;
    [SerializeField] private Quaternion portrait;

    private void Update()
    {
        CheckRotation();
    }

    private void Rotate(Quaternion quaternion)
    {
        if (transform.rotation != quaternion)
        {
            currentTimer = 0;
            currentTimer += Time.deltaTime;
            float t = currentTimer / duration;
            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, t * Time.deltaTime);
        }
    }

    private void CheckRotation()
    {
        switch (Screen.orientation)
        {
            case ScreenOrientation.LandscapeLeft:
                Rotate(landscapeLeft);
                break;
            case ScreenOrientation.LandscapeRight:
                Rotate(landscapeRight);
                break;
            case ScreenOrientation.Portrait:
                Rotate(portrait); 
                break;
            case ScreenOrientation.PortraitUpsideDown:
                Rotate(portraitUpsideDown);
                break;
        }
    }
}
