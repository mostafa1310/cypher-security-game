using UnityEngine;

// Custom enum to specify screen orientation options
public enum OrientationMode
{
    Portrait,
    LandscapeLeft
}

public class ScreenOrientationSetter : MonoBehaviour
{
    // Public field to choose the orientation mode in the Inspector
    public OrientationMode screenMode;

    void Start()
    {
        // Set the screen orientation based on the enum value
        switch (screenMode)
        {
            case OrientationMode.Portrait:
                Screen.orientation = ScreenOrientation.Portrait;
                break;
            case OrientationMode.LandscapeLeft:
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                break;
            default:
                Debug.LogWarning("Screen orientation mode not defined. Defaulting to LandscapeLeft.");
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                break;
        }
    }
}
