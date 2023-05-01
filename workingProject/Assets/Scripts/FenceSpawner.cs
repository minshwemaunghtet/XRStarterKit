using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FenceSpawner : MonoBehaviour
{
    public GameObject fencePostPrefab;
    public GameObject fenceConnectionPrefab;
    public Material fenceMaterial;
    public int maxFencePosts = 5;
    private int fencePostCount = 0;
    private GameObject lastFencePost = null;

    void Update(){
        float TotalFenceDistance = 0; // used to get full length of one fence obj
        if (Input.GetMouseButtonDown(0) && fencePostCount < maxFencePosts){
            // Currently gets the position of the mouse click... need to update this to the area target
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.y = 0f; // Spawns it at ground level right now, since we don't have vuforia setup

            // Instantiate a new fence post at the clicked position
            GameObject newFencePost = Instantiate(fencePostPrefab, clickPosition, Quaternion.identity);
            MeshRenderer fencePostRenderer = newFencePost.GetComponent<MeshRenderer>();
            if (fencePostRenderer != null){
                fencePostRenderer.material = fenceMaterial;
            }
            fencePostCount++;

            // If this is not the first fence post create a connection to the last one
            if (lastFencePost != null){
                Vector3 midPoint = (lastFencePost.transform.position + newFencePost.transform.position) / 2f;
                
                GameObject newFenceConnection = Instantiate(fenceConnectionPrefab, midPoint, Quaternion.identity);
                
                Vector3 direction = lastFencePost.transform.position - newFencePost.transform.position;
                
                newFenceConnection.transform.rotation = Quaternion.LookRotation(direction);
                
                float distance = Vector3.Distance(lastFencePost.transform.position, newFencePost.transform.position);

                TotalFenceDistance += distance; // every time the distance is calculated between two fence posts update the total fence distance for that fence object
                newFenceConnection.transform.localScale = new Vector3(0.2f, .4f, distance);
                
                MeshRenderer fenceConnectionRenderer = newFenceConnection.GetComponent<MeshRenderer>();
                if (fenceConnectionRenderer != null){
                    fenceConnectionRenderer.material = fenceMaterial;
                }
            }

            // Sets the last fence point as the new one
            lastFencePost = newFencePost;
        }

        


    }
}
