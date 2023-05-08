using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Draggable : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isDragging;

    private int touchID;

    private void Update(){
        if (Input.touchCount == 1){
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began){
                OnTouchDown(touch);
            }else if (touch.phase == TouchPhase.Moved){
                OnTouchDrag(touch);
            }else if (touch.phase == TouchPhase.Ended){
                OnTouchUp();
            }
        }else if (Input.touchCount == 2){
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began){
                OnRotationStart(touch0, touch1);
            }else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved){
                OnRotation(touch0, touch1);
            }else if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended){
                OnTouchUp();
            }
        }
    }

    void OnRotationStart(Touch touch0, Touch touch1)
    {
        touchID = touch0.fingerId;
        isDragging = true;
    }

    void OnRotation(Touch touch0, Touch touch1)
    {
        if (isDragging && (touch0.fingerId == touchID || touch1.fingerId == touchID))
        {
            transform.Rotate(Vector3.up, 10f, Space.World);
        }
    }

    private void OnTouchUp(){
        RealtimeTransform realtimeTransform = GetComponent<RealtimeTransform>();
        if (realtimeTransform != null){
            if (realtimeTransform.isOwnedLocallySelf){
                realtimeTransform.ClearOwnership();
            }
        }

        isDragging = false;
    }

    void OnTouchDown(Touch touch){
        touchID = touch.fingerId;

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform == transform){
            RealtimeTransform realtimeTransform = GetComponent<RealtimeTransform>();
            if (realtimeTransform != null){
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

    void OnTouchDrag(Touch touch){
        if (!isDragging || touch.fingerId != touchID){
            return;
        }

        if (Input.touchCount == 1){
            Vector3 cursorPoint = new Vector3(touch.position.x, touch.position.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            transform.position = cursorPosition;
        }
    }
}
