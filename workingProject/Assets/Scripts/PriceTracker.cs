using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceTracker : ScriptableObject
{
    //totals (will be updated by any new object instantiation)
    public float TotalFenceDistanceAbsolute = 0; // absolute distance of fences in play
    public int TotalNumObjects = 0; /// number of objects total in play currently
    public decimal TotalPrice = 0; /// full total price for the entire project based on all objects in play currently
    

    /// current price total by object
    /// will be updated by any new object instantiation of the appropriate type
    public decimal TotalPriceChairs = 0;
    public decimal TotalPriceTables = 0;
    public decimal TotalPriceBench = 0;
    public decimal TotalPriceBush = 0;
    public decimal TotalPriceDecks =0;
    public decimal TotalPriceFence = 0; 


    // counts
    /// will be updated by any new object instantiation of the appropriate type
    public int numChairs = 0;
    public int numTables = 0;
    public int numBench = 0;
    public int numBush = 0;
    public int numDecks =0;
    public int numFence = 0; // number of actual full fences placed down
    public int numFencePosts = 0; // number of all posts on all fences


    // material and labor costs
    public decimal COST_MATERIALS_CHAIR = 35.00m;
    public decimal COST_MATERIALS_TABLE = 100.00m;
    public decimal COST_MATERIALS_BENCH = 50.00m;
    public decimal COST_MATERIALS_BUSH = 45.00m;

    public decimal COST_MATERIALS_DECK = 900.00m;
    public decimal COST_LABOR_DECK = 350.00m;

    public decimal COST_MATERIALS_FENCE_ONE_METER = 60.00m;
    public decimal COST_MATERIALS_FENCE_POST = 40.00m;
    public decimal COST_LABOR_FENCE_ONE_METER = 20.00m;
    public decimal COST_LABOR_FENCE_POST = 12.00m;





}
