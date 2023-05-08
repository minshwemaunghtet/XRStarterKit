using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    public Button toggleButton;
    public GameObject uiElement;

    private void Start()
    {
        toggleButton.onClick.AddListener(ToggleUIElement);
        uiElement.SetActive(false);
    }

    private void ToggleUIElement()
    {
        uiElement.SetActive(!uiElement.activeSelf);
    }
}
