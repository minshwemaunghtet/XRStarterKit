using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Parent to camera
        this.transform.SetParent(Camera.main.transform, worldPositionStays: false);

        // Attempt to get a realtime transform
        RealtimeTransform realtimeTransform = GetComponent<RealtimeTransform>();
        if (realtimeTransform != null)
        {
            // Take ownership
            realtimeTransform.RequestOwnership();
        }
    }
}
