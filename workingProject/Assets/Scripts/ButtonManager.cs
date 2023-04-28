using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    public Button btn;
    public GameObject furniture;
    public InputManager inputManager; // Add a reference to the InputManager script
    
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SelectObject);
    }

    void SelectObject()
    {
        DataHandler.Instance.furniture = furniture;
        inputManager.SetSelectedObject(furniture); // Call SetSelectedObject() on the InputManager
    }

    // void SelectObject()
    // {
    //     // Instantiate the object and store a reference to it
    //     GameObject newFurniture = Instantiate(furniture);

    //     // Modify the y value of the object's position
    //     Vector3 newPosition = newFurniture.transform.position;
    //     newPosition.y = 2.0f; // Replace 2.0f with the desired y value
    //     newFurniture.transform.position = newPosition;

    //     // Store the reference to the instantiated object in DataHandler
    //     DataHandler.Instance.furniture = newFurniture;
    // }

}
