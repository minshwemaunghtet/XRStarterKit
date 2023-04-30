using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAreaTarget : MonoBehaviour{
    public float maxRaycastDistance = 100.0f;
    public LayerMask areaTargetLayer;
    public GameObject prefab;

    void Update(){
        if (Input.GetMouseButtonDown(0)){
            Vector3 clickPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(clickPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxRaycastDistance, areaTargetLayer)){
                if (hit.collider.GetComponent<MeshCollider>() != null){
                    Debug.Log("Raycast hit the Area Target's Mesh Collider");
                    Instantiate(prefab, hit.point, prefab.transform.rotation);
                }
            }
        }
    }
}
