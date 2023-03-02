using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Attempt to get a realtime transform
        RealtimeTransform realtimeTransform = GetComponent<RealtimeTransform>();
        if (realtimeTransform != null)
        {
            // If it belongs to us, reparent it
            if (realtimeTransform.isOwnedLocallyInHierarchy)
            {
                // Parent to camera
                this.transform.SetParent(Camera.main.transform, worldPositionStays: false);
            }
        }
    }
}
