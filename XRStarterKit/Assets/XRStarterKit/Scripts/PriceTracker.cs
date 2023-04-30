using UnityEngine;

[CreateAssetMenu(fileName = "NewPriceTracker", menuName = "Custom/PriceTracker")]
public class PriceTracker : ScriptableObject
{
    public float currentPrice;
    public int numThingsAdded;

    public void AddThing(float priceOfThing)
    {
        currentPrice += priceOfThing;
        numThingsAdded++;
    }

    public void Reset()
    {
        currentPrice = 0f;
        numThingsAdded = 0;
    }

    private void OnDisable()
    {
        Reset();
    }
}
