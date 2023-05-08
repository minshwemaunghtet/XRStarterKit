using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PriceTrackerUI : MonoBehaviour
{
    [SerializeField] private PriceTracker priceTracker;

    [Header("UI Elements")]
    // [SerializeField] private TextMeshProUGUI totalNumObjectsText;
    // [SerializeField] private TextMeshProUGUI totalPriceText;
    [SerializeField] private TextMeshProUGUI totalPriceChairsText;
    [SerializeField] private TextMeshProUGUI totalPriceTablesText;
    // [SerializeField] private TextMeshProUGUI totalPriceBenchText;
    [SerializeField] private TextMeshProUGUI totalPriceBushText;
    [SerializeField] private TextMeshProUGUI totalPriceTreesText;
    [SerializeField] private TextMeshProUGUI totalPriceFenceText;
    [SerializeField] private TextMeshProUGUI numChairsText;
    [SerializeField] private TextMeshProUGUI numTablesText;
    // [SerializeField] private TextMeshProUGUI numBenchText;
    [SerializeField] private TextMeshProUGUI numBushText;
    [SerializeField] private TextMeshProUGUI numTreesText;
    [SerializeField] private TextMeshProUGUI numFenceText;
    // [SerializeField] private TextMeshProUGUI numFencePostsText;
    // [SerializeField] private TextMeshProUGUI totalFenceDistanceAbsoluteText;

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
    //     totalNumObjectsText.text = priceTracker.TotalNumObjects.ToString();
    //     totalPriceText.text = priceTracker.TotalPrice.ToString();
        totalPriceChairsText.text = priceTracker.TotalPriceChairs.ToString();
        totalPriceTablesText.text = priceTracker.TotalPriceTables.ToString();
        // totalPriceBenchText.text = priceTracker.TotalPriceBench.ToString();
        totalPriceBushText.text = priceTracker.TotalPriceBush.ToString();
        totalPriceTreesText.text = priceTracker.TotalPriceTrees.ToString();
        totalPriceFenceText.text = priceTracker.TotalPriceFence.ToString();
        numChairsText.text = priceTracker.numChairs.ToString();
        numTablesText.text = priceTracker.numTables.ToString();
        // numBenchText.text = priceTracker.numBench.ToString();
        numBushText.text = priceTracker.numBush.ToString();
        numTreesText.text = priceTracker.numTrees.ToString();
        numFenceText.text = priceTracker.numFence.ToString();
        // numFencePostsText.text = priceTracker.numFencePosts.ToString();
        // totalFenceDistanceAbsoluteText.text = priceTracker.TotalFenceDistanceAbsolute.ToString();
    }
}
