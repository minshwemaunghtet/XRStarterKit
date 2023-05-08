using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Normal.Realtime;
using UnityEngine.UI;
using TMPro;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public LayerMask groundLayer;
    private Vector3[] corners = new Vector3[4];
    private int cornerIndex = 0;
    private Realtime _realtime;
    private bool isSpawningEnabled = false;
    private string spawn;

    public void DisableSpawning()
    {
        isSpawningEnabled = false;
    }

    public void EnableSpawning(Button button){
        isSpawningEnabled = !isSpawningEnabled;
        Debug.Log("Button pressed: " + button.name);
        spawn = button.name;
    }

    public void ToggleSpawning(Button button)
{
    isSpawningEnabled = !isSpawningEnabled;
    Debug.Log("Button pressed: " + button.name);
    spawn = button.name;
    // Image buttonImage = button.GetComponent<Image>();
    // buttonImage.color = Color.red;
}
    void Update()
    {
        if(isSpawningEnabled){
        if (Input.touchCount > 0)
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
                        corners[cornerIndex] = hit.point;
                        cornerIndex++;

                        if (cornerIndex == 4)
                        {
                            SpawnCube();
                            cornerIndex = 0;
                        }
                    }
                }
            }
        }
        }
    }

    private void SpawnCube(){
        Vector3 min = new Vector3(Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x),
            Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y),
            Mathf.Min(corners[0].z, corners[1].z, corners[2].z, corners[3].z));

        Vector3 max = new Vector3(Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x),
            Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y),
            Mathf.Max(corners[0].z, corners[1].z, corners[2].z, corners[3].z));

        Vector3 center = (min + max) / 2f;
        Vector3 size = max - min;

        Vector3 forward = corners[1] - corners[0];
        Vector3 up = Vector3.Cross(corners[1] - corners[0], corners[2] - corners[0]);
        Quaternion rotation = Quaternion.LookRotation(forward, up);

        Vector3 normal = Vector3.Cross(corners[1] - corners[0], corners[2] - corners[0]).normalized;
        Vector3 levelUp = Vector3.up;
        Vector3 levelRight = Vector3.Cross(normal, levelUp).normalized;
        Vector3 levelForward = Vector3.Cross(levelUp, levelRight).normalized;
        rotation = Quaternion.LookRotation(levelForward, levelUp);

        GameObject cube = Realtime.Instantiate("Cube", center, rotation, new Realtime.InstantiateOptions() { destroyWhenLastClientLeaves = true });
        cube.transform.localScale = size;
    }

}
