using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Draggable : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isDragging;
    private bool isRotating;

    private int touchID;
    private Vector2 initialTouch0Position;
    private Vector2 initialTouch1Position;
 public float rotationAngleOnKeyPress = 45f;
    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                OnTouchDown(touch);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                OnTouchDrag(touch);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                OnTouchUp();
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                OnRotationStart(touch0, touch1);
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
               RotateObjectOnKeyPress();
            }
            else if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended)
            {
                OnTouchUp();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateObjectOnKeyPress();
        }
    }
    private void RotateObjectOnKeyPress()
    {
        transform.Rotate(Vector3.up, rotationAngleOnKeyPress, Space.World);
    }

    void OnRotationStart(Touch touch0, Touch touch1)
    {
        if (!isDragging)
        {
            isRotating = true;
            initialTouch0Position = touch0.position;
            initialTouch1Position = touch1.position;
        }
    }

    void OnRotation(Touch touch0, Touch touch1)
    {
        if (isRotating)
        {
            Vector2 currentTouch0Position = touch0.position;
            Vector2 currentTouch1Position = touch1.position;

            float initialAngle = Mathf.Atan2(initialTouch1Position.y - initialTouch0Position.y, initialTouch1Position.x - initialTouch0Position.x) * Mathf.Rad2Deg;
            float currentAngle = Mathf.Atan2(currentTouch1Position.y - currentTouch0Position.y, currentTouch1Position.x - currentTouch0Position.x) * Mathf.Rad2Deg;

            float angleDifference = currentAngle - initialAngle;

            transform.Rotate(Vector3.up, angleDifference, Space.World);

            initialTouch0Position = currentTouch0Position;
            initialTouch1Position = currentTouch1Position;
        }
    }
    private void OnTouchUp()
    {
        RealtimeTransform realtimeTransform = GetComponent<RealtimeTransform>();
        if (realtimeTransform != null)
        {
            if (realtimeTransform.isOwnedLocallySelf)
            {
                realtimeTransform.ClearOwnership();
            }
        }

        isDragging = false;
        isRotating = false;
    }
    void OnTouchDown(Touch touch)
    {
        touchID = touch.fingerId;

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            RealtimeTransform realtimeTransform = GetComponent<RealtimeTransform>();
            if (realtimeTransform != null)
            {
                if (realtimeTransform.isOwnedRemotelySelf)
                {
                    Debug.LogWarning("Already owned by another player. Ignoring.");
                    return;
                }
                realtimeTransform.RequestOwnership();
            }

            isDragging = true;
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, screenPoint.z));
        }
    }
    void OnTouchDrag(Touch touch)
    {
        if (isDragging && touch.fingerId == touchID)
        {
            Vector3 cursorPoint = new Vector3(touch.position.x, touch.position.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            transform.position = cursorPosition;
        }
    }
}
