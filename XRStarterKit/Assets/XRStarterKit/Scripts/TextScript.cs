using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScript : MonoBehaviour
{
    public PriceTracker priceTracker;
    public TextMeshProUGUI displayText;
    
    void Update()
    {
        // Update the TextMesh Pro component with the current amount of the Scriptable Object
        displayText.text = "Num things added: " + priceTracker.numThingsAdded.ToString();
    }
}
