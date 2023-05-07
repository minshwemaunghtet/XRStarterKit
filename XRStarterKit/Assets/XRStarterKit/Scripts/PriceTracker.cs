using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewPriceTracker", menuName = "Custom/PriceTracker")]

public class PriceTracker : ScriptableObject
{
    //totals (will be updated by any new object instantiation)
    public int TotalNumObjects = 0; /// number of objects total in play currently
    public decimal TotalPrice = 0; /// full total price for the entire project based on all objects in play currently
    

    /// current price total by object
    /// will be updated by any new object instantiation of the appropriate type
    public decimal TotalPriceChairs = 0;
    public decimal TotalPriceTables = 0;
    public decimal TotalPriceBench = 0;
    public decimal TotalPriceBush = 0;
    public decimal TotalPriceTrees =0;
    public decimal TotalPriceFence = 0; 



    // counts
    /// will be updated by any new object instantiation of the appropriate type
    public int numChairs = 0;
    public int numTables = 0;
    public int numBench = 0;
    public int numBush = 0;
    public int numTrees =0;
    public int numFence = 0; // number of actual full fences placed down
    public int numFencePosts = 0; // number of all posts on all fences
    public float TotalFenceDistanceAbsolute = 0; // absolute distance of fences in play


    // material and labor costs
    public decimal COST_MATERIALS_CHAIR = 35.00m;
    public decimal COST_MATERIALS_TABLE = 100.00m;
    public decimal COST_MATERIALS_BENCH = 50.00m;
    public decimal COST_MATERIALS_BUSH = 45.00m;

    public decimal COST_MATERIALS_TREE = 125.00m;

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

    // calculate non-fence object cost
    public decimal CalculateBasicObjectCost(string resourceType){
        decimal cost =0;

        if (resourceType == "Tree") {cost = COST_MATERIALS_TREE;}

        if (resourceType == "Bench") {cost = COST_MATERIALS_BENCH;}

        if (resourceType == "Chair"){cost = COST_MATERIALS_CHAIR;}

        if (resourceType == "Table"){cost = COST_MATERIALS_TABLE;}

        if (resourceType == "Bush"){cost = COST_MATERIALS_BUSH;}

        return cost;
    }
    // update non-fence object count
    public void UpdateBasicObjectCount(string resourceType){

        if (resourceType == "Tree") {numTrees++;}

        if (resourceType == "Bench") {numBench++;}

        if (resourceType == "Chair"){numChairs++;}

        if (resourceType == "Table"){numTables++;}

        if (resourceType == "Bush"){numBush++;}
    }
    // update basic object (non fence) cumulative
    public void UpdateBasicObjectTotalPrice(string resourceType, decimal totalPrice){

        if (resourceType == "Tree") {TotalPriceTrees+=totalPrice;}

        if (resourceType == "Bench") {TotalPriceBench+=totalPrice;}

        if (resourceType == "Chair"){TotalPriceChairs+=totalPrice;}

        if (resourceType == "Table"){TotalPriceTables+=totalPrice;}

        if (resourceType == "Bush"){TotalPriceBush+=totalPrice;}
    }


    public void PriceTrackerUpdateMain(string ResourceType, int numFencePosts_current_obj=0, float MeterDistance_current_fence_obj= 0) {

        decimal object_cost = 0;

        // global updates
        TotalNumObjects++;

        if (ResourceType == "Fence") {//update fence count, fence tot distance, total fence price, total fence posts
            
            // guard warning clause
            if (numFencePosts_current_obj == 0) {Debug.LogWarning("Resource Type passed as 'Fence' but number of Fence Posts is 0.");}
            if (MeterDistance_current_fence_obj == 0) {Debug.LogWarning("Resource Type passed as 'Fence' but distance of the fence object is 0.");}

            // update fence specific values
            numFencePosts+=numFencePosts_current_obj;
            numFence+=1;
            TotalFenceDistanceAbsolute+=MeterDistance_current_fence_obj;
            // get and update cost fence obj
            object_cost = CalculateFenceCost(MeterDistance_current_fence_obj, numFencePosts_current_obj);
            TotalPriceFence += object_cost;

        }
        else // not resource type Fence
        {
            UpdateBasicObjectCount(ResourceType);
            object_cost = CalculateBasicObjectCost(ResourceType);
            UpdateBasicObjectTotalPrice(ResourceType,object_cost);
        }
        TotalPrice += object_cost;
        Debug.Log("Total Price updated to: " + TotalPrice.ToString());
        Debug.Log("Total num objects:" + TotalNumObjects.ToString());


        
    }
    public void ResetPriceTracker()
{
    TotalNumObjects = 0;
    TotalPrice = 0;

    TotalPriceChairs = 0;
    TotalPriceTables = 0;
    TotalPriceBench = 0;
    TotalPriceBush = 0;
    TotalPriceTrees = 0;
    TotalPriceFence = 0;

    numChairs = 0;
    numTables = 0;
    numBench = 0;
    numBush = 0;
    numTrees = 0;
    numFence = 0;
    numFencePosts = 0;
    TotalFenceDistanceAbsolute = 0;
}

private void OnDisable() {
    ResetPriceTracker();
}



}