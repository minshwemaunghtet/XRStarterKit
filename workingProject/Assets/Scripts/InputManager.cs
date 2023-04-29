using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class InputManager : ARBaseGestureInteractable
{
    public Camera arCam;
    public ARRaycastManager _raycastManager;
    public GameObject crosshair;

    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private Touch touch;
    private Pose pose;

    private GameObject selectedObject; // Store the selected object from ButtonManager

    void Start()
    {
    }

    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if (gesture.targetObject == null)
            return true;
        return false;
    }

    protected override void OnEndManipulation(TapGesture gesture)
    {
        if (gesture.isCanceled)
            return;

        if (gesture.targetObject != null || IsPointerOverUI(gesture))
        {
            return;
        }

        if (selectedObject == null)
        {
            Debug.LogWarning("Selected object is null. Please select an object first.");
            return;
        }

        if (GestureTransformationUtility.Raycast(gesture.startPosition, _hits, TrackableType.PlaneWithinPolygon))
        {
            // Instantiate the selected object
            GameObject placedObj = Instantiate(selectedObject, _hits[0].pose.position, _hits[0].pose.rotation);

            // Set the y-position of the object to the y-position of the hit point in the AR plane
            placedObj.transform.position = new Vector3(placedObj.transform.position.x, _hits[0].pose.position.y, placedObj.transform.position.z);

            // Create an anchor object for the placed object and parent the placed object to it
            var anchorObject = new GameObject("PlacementAnchor");
            anchorObject.transform.position = _hits[0].pose.position;
            anchorObject.transform.rotation = _hits[0].pose.rotation;
            placedObj.transform.parent = anchorObject.transform;
        }
    }

    void CrosshairCalculation()
    {
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));

        if (GestureTransformationUtility.Raycast(origin, _hits, TrackableType.PlaneWithinPolygon))
        {
            pose = _hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(90, 0, 0);
        }
    }

    bool IsPointerOverUI(TapGesture touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.startPosition.x, touch.startPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    // This method is called by the ButtonManager when an object is selected
    public void SetSelectedObject(GameObject selectedObject)
    {
        this.selectedObject = selectedObject;
    }
}
