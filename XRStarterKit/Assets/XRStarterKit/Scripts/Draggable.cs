using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Draggable : MonoBehaviour
{
	private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        // Attempt to get a realtime transform
        RealtimeTransform realtimeTransform = GetComponent<RealtimeTransform>();
        if (realtimeTransform != null)
        {
            // Make sure another user doesn't own it
            if (realtimeTransform.isOwnedRemotelySelf)
            {
                Debug.LogWarning("Already owned by another player. Ignoring.");
                return;
            }

            // Take ownership
            realtimeTransform.RequestOwnership();
        }

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;
    }

    private void OnMouseUp()
    {
        // Attempt to get a realtime transform
        RealtimeTransform realtimeTransform = GetComponent<RealtimeTransform>();
        if (realtimeTransform != null)
        {
            // Make sure we own it
            if (realtimeTransform.isOwnedLocallySelf)
            {
                // Release ownership
                realtimeTransform.ClearOwnership();
            }
        }
    }
}
