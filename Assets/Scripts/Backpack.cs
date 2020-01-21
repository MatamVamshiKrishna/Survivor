using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Backpack to store all the pickable items
public class Backpack : MonoBehaviour
{
    public float MaxWeight = 2.0f;

    private float currentWeight = 0;
    private Dictionary<string, int> mapNameToCount = new Dictionary<string, int>();
    
    //private Dictionary<string, List<GameObject>> items = new Dictionary<string, List<GameObject>>();
    // Note : Store a list of GameObjects, only when you need 
    // more realistic, for ex : there can be two variations of 
    // item (ex : small and big). When you have two variations in inventory
    // and you stacked them, if you don't want to spawn big apple, when you 
    // drop generic one 
    // Note : UI also should support variations

    public bool AddItem(GameObject go)
    {
        var item = go.GetComponent<Item>();
        if(!item)
        {
            // TODO @set reason for rejection
            // GO is not a pickable item
            return false;
        }

        if((item.Weight + currentWeight) <= MaxWeight)
        {
            if(!mapNameToCount.ContainsKey(item.Name))
            {
                mapNameToCount.Add(item.Name, 0);
            }

            mapNameToCount[item.Name]++;
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
