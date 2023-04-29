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

}
