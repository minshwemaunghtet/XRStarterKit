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
        displayText.text = "Number of objects: " + priceTracker.TotalNumObjects.ToString() + "\n" 
                          + "Total Price: " + priceTracker.TotalPrice.ToString("C") + "\n" 
                          + "Total number of fence posts: " + priceTracker.numFencePosts.ToString();
    }
}