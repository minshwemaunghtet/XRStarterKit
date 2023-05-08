using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScript : MonoBehaviour
{
    public PriceTracker priceTracker;
    public TextMeshProUGUI summaryText;
    public TextMeshProUGUI displayText;


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

    /*Summary variables*/

    public TextMeshProUGUI totalNumObjects;
    public TextMeshProUGUI totalPrice;
    void Update()
    {
        /*Display on top lefthome screen*/
        summaryText.text = "Summary of all Items";
        // Update the TextMesh Pro component with the current amount of the Scriptable Object
        /*
        displayText.text = "Number of objects: " + priceTracker.TotalNumObjects.ToString() + "\n" 
                          + "Total Price: " + priceTracker.TotalPrice.ToString("C") + "\n" 
                          + "Total number of fence posts: " + priceTracker.numFencePosts.ToString();

        */
        /*Setting Quantity for items*/
        chairQt.text= priceTracker.numChairs.ToString();    
        tableQt.text = priceTracker.numTables.ToString();
        treePrice.text = priceTracker.numTrees.ToString();
        bushQt.text = priceTracker.numBush.ToString();
        fenceQt.text = priceTracker.numFence.ToString();   

        /*Setting Price*/     
        chairPrice.text= priceTracker.TotalPriceChairs.ToString();    
        tablePrice.text = priceTracker.TotalPriceTables.ToString();
        treePrice.text = priceTracker.TotalPriceTrees.ToString();
        bushPrice.text = priceTracker.TotalPriceBush.ToString();
        fencePrice.text = priceTracker.TotalPriceFence.ToString(); 

        /*Setting summary*/
        totalNumObjects.text = priceTracker.TotalNumObjects.ToString();
        totalPrice.text = priceTracker.TotalPrice.ToString();

    }
}