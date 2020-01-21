using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all the pickable items
public class Item : MonoBehaviour
{
    public string Name;
    public string Description;
    public float Weight; // in kgs
    public Sprite Sprite;
    public List<string> Actions;

    private void Start()
    {
        Actions.Add("drop");
    }
}
