using UnityEngine;
using Lean.Touch;

public class LeanTouchEnabler : MonoBehaviour
{
    public Behaviour[] behavioursToDisable;
    private bool isLeanTouchEnabled = false;

    private void Start()
    {
        // Disable LeanTouch by default
        DisableLeanTouch();
    }

    public void ToggleLeanTouch()
    {
        isLeanTouchEnabled = !isLeanTouchEnabled;

        if (isLeanTouchEnabled)
        {
            EnableLeanTouch();
        }
        else
        {
            DisableLeanTouch();
        }
    }

    private void EnableLeanTouch()
    {
        LeanTouch.OnFingerTap += HandleFingerSet;
        LeanTouch.OnFingerUp += HandleFingerUp;

        foreach (var behaviour in behavioursToDisable)
        {
            behaviour.enabled = false;
        }
    }

    private void DisableLeanTouch()
    {
        LeanTouch.OnFingerTap -= HandleFingerSet;
        LeanTouch.OnFingerUp -= HandleFingerUp;

        foreach (var behaviour in behavioursToDisable)
        {
            behaviour.enabled = true;
        }
    }

    private void HandleFingerSet(LeanFinger finger)
    {
        Debug.Log("Finger Set!");
    }

    private void HandleFingerUp(LeanFinger finger)
    {
        Debug.Log("Finger Up!");
    }
}
