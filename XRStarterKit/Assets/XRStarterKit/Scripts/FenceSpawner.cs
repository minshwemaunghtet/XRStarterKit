// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Normal.Realtime;
// public class FenceSpawner : MonoBehaviour
// {
//     public GameObject fencePostPrefab;
//     public GameObject fenceConnectionPrefab;
//     public Material fenceMaterial;
//     public int maxFencePosts = 5;
//     public LayerMask groundLayer;
//     private int fencePostCount = 0;
//     private GameObject lastFencePost = null;
//     private Realtime _realtime;
//     void Update(){
//         if (Input.GetMouseButtonDown(0) && fencePostCount < maxFencePosts)
//         {
//             Vector3 clickPosition = Input.mousePosition;
//             Ray ray = Camera.main.ScreenPointToRay(clickPosition);
//             RaycastHit hit;
//             if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)){
//                 GameObject newFencePost = Realtime.Instantiate(fencePostPrefab.name, hit.point, Quaternion.identity,true); 
//                 MeshRenderer fencePostRenderer = newFencePost.GetComponent<MeshRenderer>();
//                 if (fencePostRenderer != null){
//                     fencePostRenderer.material = fenceMaterial;
//                 }

//                 fencePostCount++;
//                 if (lastFencePost != null){
//                     Vector3 midPoint = (lastFencePost.transform.position + newFencePost.transform.position) / 2f;
//                     GameObject newFenceConnection = Realtime.Instantiate(fenceConnectionPrefab.name, midPoint, Quaternion.identity,true);
//                     Vector3 direction = lastFencePost.transform.position - newFencePost.transform.position;
//                     newFenceConnection.transform.rotation = Quaternion.LookRotation(direction);
//                     float distance = Vector3.Distance(lastFencePost.transform.position, newFencePost.transform.position);
//                     newFenceConnection.transform.localScale = new Vector3(0.2f, 0.4f, distance);

//                     MeshRenderer fenceConnectionRenderer = newFenceConnection.GetComponent<MeshRenderer>();
//                     if (fenceConnectionRenderer != null){
//                         fenceConnectionRenderer.material = fenceMaterial;
//                     }
//                 }

//                 lastFencePost = newFencePost;
//             }
//         }
//     }
// }
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using Normal.Realtime;

// public class FenceSpawner : MonoBehaviour
// {
//     public GameObject fencePostPrefab;
//     public GameObject fenceConnectionPrefab;
//     public Material fenceMaterial;
//     public int maxFencePosts = 5;
//     public LayerMask groundLayer;
//     private int fencePostCount = 0;
//     private GameObject lastFencePost = null;
//     private Realtime _realtime;
//     public PriceTracker tracker;

//     private bool isSpawningEnabled = false;

//     public void EnableSpawning()
//     {
//         isSpawningEnabled = true;
//     }

//     public void DisableSpawning()
//     {
//         isSpawningEnabled = false;
//         fencePostCount = 0; // reset fence count
//         lastFencePost = null; // reset last fence post
//     }

//     public void ResetFences()
//     {
//         fencePostCount = 0;
//         lastFencePost = null;

//         foreach (GameObject fencePost in GameObject.FindGameObjectsWithTag("FencePost"))
//         {
//             Realtime.Destroy(fencePost);
//         }

//         foreach (GameObject fenceConnection in GameObject.FindGameObjectsWithTag("FenceConnection"))
//         {
//             Realtime.Destroy(fenceConnection);
//         }
//     }

//       void Start()
//     {
//         // Prompt the user to enter the number of fences they want
//         int numFences = 0;
//         bool isInputValid = false;
//         while (!isInputValid)
//         {
//             Debug.Log("Enter the number of fences you want: ");
//             string input = Console.ReadLine();
//             isInputValid = int.TryParse(input, out numFences) && numFences > 0;
//         }

//         maxFencePosts = numFences;
//     }

//     void Update()
//     {
//         if (isSpawningEnabled && Input.GetMouseButtonDown(0) && fencePostCount < maxFencePosts && !EventSystem.current.IsPointerOverGameObject())
//         {
//             Vector3 clickPosition = Input.mousePosition;
//             Ray ray = Camera.main.ScreenPointToRay(clickPosition);
//             RaycastHit hit;
//             if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
//             {
//                 GameObject newFencePost = Realtime.Instantiate(fencePostPrefab.name, hit.point, Quaternion.identity, new Realtime.InstantiateOptions() { destroyWhenLastClientLeaves = true });
//                 tracker.numThingsAdded ++;
//                 Debug.Log(tracker.numThingsAdded);
//                 MeshRenderer fencePostRenderer = newFencePost.GetComponent<MeshRenderer>();
//                 if (fencePostRenderer != null)
//                 {
//                     fencePostRenderer.material = fenceMaterial;
//                 }

//                 fencePostCount++;
//                 if (lastFencePost != null)
//                 {
//                     Vector3 midPoint = (lastFencePost.transform.position + newFencePost.transform.position) / 2f;
//                     GameObject newFenceConnection = Realtime.Instantiate(fenceConnectionPrefab.name, midPoint, Quaternion.identity, new Realtime.InstantiateOptions() { destroyWhenLastClientLeaves = true });
//                     Vector3 direction = lastFencePost.transform.position - newFencePost.transform.position;
//                     newFenceConnection.transform.rotation = Quaternion.LookRotation(direction);
//                     float distance = Vector3.Distance(lastFencePost.transform.position, newFencePost.transform.position);
//                     newFenceConnection.transform.localScale = new Vector3(0.2f, 0.4f, distance);

//                     MeshRenderer fenceConnectionRenderer = newFenceConnection.GetComponent<MeshRenderer>();
//                     if (fenceConnectionRenderer != null)
//                     {
//                         fenceConnectionRenderer.material = fenceMaterial;
//                     }
//                 }

//                 lastFencePost = newFencePost;
//             }
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Normal.Realtime;
using UnityEngine.UI;
using TMPro;

public class FenceSpawner : MonoBehaviour
{
    public GameObject fencePostPrefab;
    public GameObject fenceConnectionPrefab;
    public Material fenceMaterial;
    public int maxFencePosts = 5;
    public LayerMask groundLayer;
    private int fencePostCount = 0;
    private GameObject lastFencePost = null;
    private Realtime _realtime;
    public PriceTracker tracker;
    public TMP_InputField fenceCountInput;

    private bool isSpawningEnabled = false;

    private void Start() {
        fenceCountInput.gameObject.SetActive(false);
    }
    public void EnableSpawning()
    {
        fenceCountInput.gameObject.SetActive(true);
        PromptForFenceCount();
        isSpawningEnabled = true;
    }

    public void DisableSpawning()
    {
        fenceCountInput.gameObject.SetActive(false);
        isSpawningEnabled = false;
        fencePostCount = 0;
        lastFencePost = null; 
    }

    public void ResetFences()
    {
        fencePostCount = 0;
        lastFencePost = null;

        foreach (GameObject fencePost in GameObject.FindGameObjectsWithTag("FencePost"))
        {
            Realtime.Destroy(fencePost);
        }

        foreach (GameObject fenceConnection in GameObject.FindGameObjectsWithTag("FenceConnection"))
        {
            Realtime.Destroy(fenceConnection);
        }
    }

    public void SetMaxFencePosts(string input)
    {
        int numFences = 0;
        if (int.TryParse(input, out numFences) && numFences > 0)
        {
            maxFencePosts = numFences;
            fenceCountInput.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Invalid input for number of fences");
        }
    }

    public void PromptForFenceCount()
    {
        fenceCountInput.gameObject.SetActive(true);
        fenceCountInput.ActivateInputField();
        fenceCountInput.onEndEdit.AddListener(SetMaxFencePosts);
    }

    void Update()
    {
        if (isSpawningEnabled && Input.touchCount > 0 && fencePostCount < maxFencePosts)
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
                        GameObject newFencePost = Realtime.Instantiate(fencePostPrefab.name, hit.point, Quaternion.identity, new Realtime.InstantiateOptions() { destroyWhenLastClientLeaves = true });
                        tracker.numThingsAdded++;
                        Debug.Log(tracker.numThingsAdded);
                        MeshRenderer fencePostRenderer = newFencePost.GetComponent<MeshRenderer>();
                        if (fencePostRenderer != null)
                        {
                            fencePostRenderer.material = fenceMaterial;
                        }

                        fencePostCount++;
                        if (lastFencePost != null)
                        {
                            Vector3 midPoint = (lastFencePost.transform.position + newFencePost.transform.position) / 2f;
                            GameObject newFenceConnection = Realtime.Instantiate(fenceConnectionPrefab.name, midPoint, Quaternion.identity, new Realtime.InstantiateOptions() { destroyWhenLastClientLeaves = true });
                            Vector3 direction = lastFencePost.transform.position - newFencePost.transform.position;
                            newFenceConnection.transform.rotation = Quaternion.LookRotation(direction);
                            float distance = Vector3.Distance(lastFencePost.transform.position, newFencePost.transform.position);
                            newFenceConnection.transform.localScale = new Vector3(0.2f, 0.4f, distance);
                            MeshRenderer fenceConnectionRenderer = newFenceConnection.GetComponent<MeshRenderer>();
                            if (fenceConnectionRenderer != null)
                            {
                            fenceConnectionRenderer.material = fenceMaterial;
                            }
                        }
                        lastFencePost = newFencePost;
                    }
                }
            }
        }
    }
}
