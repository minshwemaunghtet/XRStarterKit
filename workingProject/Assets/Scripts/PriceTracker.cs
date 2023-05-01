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


    //calculate single fence object cost price 
    // def: single fence obj is the whole new fence obj created after placing down desired posts at desired lengths
    // update:
    // - total fence price
    // - total price
    // - TotalFenceDistanceAbsolute
    // - numFence
    // - numFencePosts
    // - TotalPriceFence
    // Variables:
    // fence_total_distance: the total distance sum of all distance between all posts in a single fence obj
    // num_fence_posts: number of fence post (int) in a single fence obj
    public decimal CalculateFenceCost(float fence_total_distance, int num_fence_posts){
        decimal cost = 0;
        // cost of material fence distance + num fence posts
        decimal cost_materials = (COST_MATERIALS_FENCE_ONE_METER * (decimal)fence_total_distance) + (COST_MATERIALS_FENCE_POST * num_fence_posts);

        // cost of labor fence distance + num fence posts
        decimal cost_labor = (COST_LABOR_FENCE_ONE_METER * (decimal)fence_total_distance) + (COST_LABOR_FENCE_POST * num_fence_posts);

        cost = cost_materials + cost_labor;
        return cost;
    }



}
