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
    public PriceTracker tracker;
    public TMP_InputField fenceCountInput;
    private int fencePostCount = 0;
    private GameObject lastFencePost = null;
    private Realtime _realtime;
    private float cuTotalDistanceFenceObj = 0;


    private bool isSpawningEnabled = false;

    private void Start() {
        fenceCountInput.gameObject.SetActive(false);
    }
    public void EnableSpawning()
    {   
        if(isSpawningEnabled){
            DisableSpawning();
        }else{
            isSpawningEnabled = true;
            fenceCountInput.gameObject.SetActive(true);
            PromptForFenceCount();
    
        }
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
        cuTotalDistanceFenceObj = 0;
        Debug.Log("Prompt for fence count set to" + cuTotalDistanceFenceObj);
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
                        // Adjust the y-coordinate of the hit point by adding half of the height of the fence post
                        Vector3 spawnPosition = hit.point + Vector3.up * fencePostPrefab.transform.localScale.y / 2f;

                        GameObject newFencePost = Realtime.Instantiate(fencePostPrefab.name, spawnPosition, Quaternion.identity, new Realtime.InstantiateOptions() { destroyWhenLastClientLeaves = true });
                        // tracker.numThingsAdded++;

                        // Debug.Log(tracker.numThingsAdded);
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
                            cuTotalDistanceFenceObj += distance;
                            Debug.Log("Fence Post Distance: " + distance);
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
        }else if(fencePostCount ==  maxFencePosts){
            Debug.Log("MaxReached: " + cuTotalDistanceFenceObj);
            tracker.PriceTrackerUpdateMain("Fence", fencePostCount, cuTotalDistanceFenceObj);
            DisableSpawning();
        }
    }
}
