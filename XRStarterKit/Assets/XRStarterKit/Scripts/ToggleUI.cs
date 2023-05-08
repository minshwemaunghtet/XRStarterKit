using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    public Button toggleButton;
    public Button toggleButton2;
    public GameObject uiElement;
    public GameObject firstUI;

    private void Start()
    {
        toggleButton.onClick.AddListener(ToggleUIElement);
        toggleButton2.onClick.AddListener(ToggleUIElement);
        uiElement.SetActive(false);
        firstUI.SetActive(true);
    }

    private void ToggleUIElement()
    {
        uiElement.SetActive(!uiElement.activeSelf);
        firstUI.SetActive(!firstUI.activeSelf);
    }
}
