using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScript : MonoBehaviour
{
    public PriceTracker priceTracker;
    public TextMeshProUGUI summaryText;
    public TextMeshProUGUI displayText;

    public TextMeshProUGUI chairText;

    /*Quantity Variables*/
    public TextMeshProUGUI chairQt;
    public TextMeshProUGUI tableQt;
    public TextMeshProUGUI treeQt;
    public TextMeshProUGUI bushQt;
    public TextMeshProUGUI concreteQt;
    public TextMeshProUGUI fenceQt;
    

    /*Price Variables*/
    public TextMeshProUGUI chairPrice;
    public TextMeshProUGUI tablePrice;
    public TextMeshProUGUI treePrice;
    public TextMeshProUGUI bushPrice;
    public TextMeshProUGUI concretePrice;
    public TextMeshProUGUI fencePrice;
    void Update()
    {
        summaryText.text = "Summary of all Items";
        // Update the TextMesh Pro component with the current amount of the Scriptable Object
        displayText.text = "Number of objects: " + priceTracker.TotalNumObjects.ToString() + "\n" 
                          + "Total Price: " + priceTracker.TotalPrice.ToString("C") + "\n" 
                          + "Total number of fence posts: " + priceTracker.numFencePosts.ToString();
        chairText.text = " Random text";

        chairQt.text= priceTracker.numChairs.ToString();
        tableQt.text = priceTracker.numTables.ToString();
        
    }
}