using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Backpack to store all the pickable items
public class Backpack : MonoBehaviour
{
    public float MaxWeight = 2.0f;

    private float currentWeight = 0;
    private Dictionary<string, int> mapNameToCount = new Dictionary<string, int>();
    
    public bool AddItem(GameObject go)
    {
        var item = go.GetComponent<Item>();
        if(!item)
        {
            // TODO @set reason for rejection
            // GO is not a pickable item
            return false;
        }

        if((item.Data.Weight + currentWeight) <= MaxWeight)
        {
            if(!mapNameToCount.ContainsKey(item.Data.Name))
            {
                mapNameToCount.Add(item.Data.Name, 0);
            }

            mapNameToCount[item.Data.Name]++;
            currentWeight += item.Data.Weight;
            return true;
        }
        else
        {
            // TODO @set reason for rejection
            // Backpack is full
            return false;
        }
    }
}
