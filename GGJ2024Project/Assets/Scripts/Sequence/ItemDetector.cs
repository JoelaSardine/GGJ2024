using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetector : MonoBehaviour
{
    public List<ItemType> Items;

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.attachedRigidbody?.GetComponent<Item>();
        
        if(item) Items.Add(item.type);
    }

    private void OnTriggerExit(Collider other)
    {
        Item item = other.attachedRigidbody?.GetComponent<Item>();
        
        if(item) Items.Remove(item.type);
    }
}
