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
    private string spawn;

    public void DisableSpawning()
    {
        isSpawningEnabled = false;
    }

    public void ToggleSpawning(Button button)
    {
        isSpawningEnabled = !isSpawningEnabled;
        Debug.Log("Button pressed: " + button.name);
        spawn = button.name;
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
                        // Adjust the y-coordinate of the hit point by adding half of the height of the object
                        Vector3 spawnPosition = hit.point + Vector3.up * prefab.transform.localScale.y / 2f;
                        GameObject newPrefab = Realtime.Instantiate(spawn, spawnPosition, Quaternion.identity, new Realtime.InstantiateOptions() { destroyWhenLastClientLeaves = true });
                        tracker.numThingsAdded++;
                        // Debug.Log(tracker.numThingsAdded);
                    }
                }
            }
        }
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using Normal.Realtime;
// using UnityEngine.UI;
// using TMPro;

// public class PlaceObject : MonoBehaviour
// {
//     public LayerMask groundLayer;
//     public GameObject prefab;
//     public PriceTracker tracker;
//     private Realtime _realtime;
//     private bool isSpawningEnabled = false;

//     private GameObject selectedObject = null;
//     private Vector3 touchOffset = Vector3.zero;

//     public void DisableSpawning()
//     {
//         isSpawningEnabled = false;
//     }
//     public void ToggleSpawning()
//     {
//         isSpawningEnabled = !isSpawningEnabled;
//     }

//     void Update()
//     {
//         if (isSpawningEnabled && Input.touchCount > 0)
//         {
//             bool isTouchOverUI = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
//             if (!isTouchOverUI)
//             {
//                 Touch touch = Input.GetTouch(0);
//                 if (touch.phase == TouchPhase.Began)
//                 {
//                     Vector3 touchPosition = touch.position;
//                     Ray ray = Camera.main.ScreenPointToRay(touchPosition);
//                     RaycastHit hit;
//                     if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
//                     {
//                         GameObject newPrefab = Realtime.Instantiate(prefab.name, hit.point, Quaternion.identity, new Realtime.InstantiateOptions() { destroyWhenLastClientLeaves = true });
//                         tracker.numThingsAdded++;
//                         Debug.Log(tracker.numThingsAdded);
//                     }
//                     else
//                     {
//                         // No ground hit, so check if we hit an object
//                         RaycastHit[] hits = Physics.RaycastAll(ray);
//                         foreach (RaycastHit objectHit in hits)
//                         {
//                             if (objectHit.transform.gameObject.tag == "Selectable")
//                             {
//                                 // Found a selectable object, so select it
//                                 selectedObject = objectHit.transform.gameObject;
//                                 touchOffset = selectedObject.transform.position - hit.point;
//                                 break;
//                             }
//                         }
//                     }
//                 }
//                 else if (touch.phase == TouchPhase.Moved && selectedObject != null)
//                 {
//                     Vector3 touchPosition = touch.position;
//                     Ray ray = Camera.main.ScreenPointToRay(touchPosition);
//                     RaycastHit hit;
//                     if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
//                     {
//                         // Move the selected object to the touched position
//                         selectedObject.transform.position = hit.point + touchOffset;
//                     }
//                 }
//                 else if (touch.phase == TouchPhase.Ended)
//                 {
//                     // Release the selected object
//                     selectedObject = null;
//                     touchOffset = Vector3.zero;
//                 }
//             }
//         }
//     }
// }
