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

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && fencePostCount < maxFencePosts)
        {
            // Get the position of the mouse click in world space
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.y = 0f; // Ensure fence post is spawned at ground level

            // Instantiate a new fence post at the clicked position
            GameObject newFencePost = Instantiate(fencePostPrefab, clickPosition, Quaternion.identity);

            // Assign the fence material to the fence post's mesh renderer
            MeshRenderer fencePostRenderer = newFencePost.GetComponent<MeshRenderer>();
            if (fencePostRenderer != null)
            {
                fencePostRenderer.material = fenceMaterial;
            }

            // Increment the fence post count
            fencePostCount++;

            // If this is not the first fence post, create a connection to the previous one
            if (lastFencePost != null)
            {
                // Calculate the midpoint between the last fence post and the new one
                Vector3 midPoint = (lastFencePost.transform.position + newFencePost.transform.position) / 2f;

                // Instantiate a new fence connection at the midpoint
                GameObject newFenceConnection = Instantiate(fenceConnectionPrefab, midPoint, Quaternion.identity);

                // Rotate the fence connection to face the last fence post
                Vector3 direction = lastFencePost.transform.position - newFencePost.transform.position;
                newFenceConnection.transform.rotation = Quaternion.LookRotation(direction);

                // Scale the fence connection to fit between the two fence posts
                float distance = Vector3.Distance(lastFencePost.transform.position, newFencePost.transform.position);
                newFenceConnection.transform.localScale = new Vector3(0.2f, .4f, distance);

                // Assign the fence material to the fence connection's mesh renderer
                MeshRenderer fenceConnectionRenderer = newFenceConnection.GetComponent<MeshRenderer>();
                if (fenceConnectionRenderer != null)
                {
                    fenceConnectionRenderer.material = fenceMaterial;
                }
            }

            // Remember this fence post for the next connection
            lastFencePost = newFencePost;
        }
    }
}
