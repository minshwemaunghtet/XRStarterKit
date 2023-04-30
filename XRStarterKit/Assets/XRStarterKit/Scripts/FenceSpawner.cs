using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
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
    void Update(){
        if (Input.GetMouseButtonDown(0) && fencePostCount < maxFencePosts)
        {
            Vector3 clickPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(clickPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)){
                GameObject newFencePost = Realtime.Instantiate(fencePostPrefab.name, hit.point, Quaternion.identity,true); 
                MeshRenderer fencePostRenderer = newFencePost.GetComponent<MeshRenderer>();
                if (fencePostRenderer != null){
                    fencePostRenderer.material = fenceMaterial;
                }

                fencePostCount++;
                if (lastFencePost != null){
                    Vector3 midPoint = (lastFencePost.transform.position + newFencePost.transform.position) / 2f;
                    GameObject newFenceConnection = Realtime.Instantiate(fenceConnectionPrefab.name, midPoint, Quaternion.identity,true);
                    Vector3 direction = lastFencePost.transform.position - newFencePost.transform.position;
                    newFenceConnection.transform.rotation = Quaternion.LookRotation(direction);
                    float distance = Vector3.Distance(lastFencePost.transform.position, newFencePost.transform.position);
                    newFenceConnection.transform.localScale = new Vector3(0.2f, 0.4f, distance);

                    MeshRenderer fenceConnectionRenderer = newFenceConnection.GetComponent<MeshRenderer>();
                    if (fenceConnectionRenderer != null){
                        fenceConnectionRenderer.material = fenceMaterial;
                    }
                }

                lastFencePost = newFencePost;
            }
        }
    }
}
