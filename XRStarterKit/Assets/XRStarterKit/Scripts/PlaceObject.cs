using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Normal.Realtime;
using UnityEngine.UI;
using TMPro;

public class PlaceObject : MonoBehaviour
{
    public LayerMask groundLayer;
    public GameObject prefab;
    public PriceTracker tracker;
    private Realtime _realtime;
    private bool isSpawningEnabled = false;
    
    // public void EnableSpawning()
    // {
    //     isSpawningEnabled = true;
    // }

    public void DisableSpawning()
    {
        isSpawningEnabled = false;
    }
    public void ToggleSpawning()
    {
        isSpawningEnabled = !isSpawningEnabled;
    }

    void Update()
    {
        if (isSpawningEnabled && Input.touchCount > 0)
        {
            bool isTouchOverUI = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
            if (!isTouchOverUI)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 touchPosition = touch.position;
                    Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
                    {
                        GameObject newPrefab = Realtime.Instantiate(prefab.name, hit.point, Quaternion.identity, new Realtime.InstantiateOptions() { destroyWhenLastClientLeaves = true });
                        tracker.numThingsAdded++;
                        Debug.Log(tracker.numThingsAdded);
                    }
                }
            }
        }
    }
}
