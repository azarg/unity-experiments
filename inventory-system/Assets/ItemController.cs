using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private Item item;

    private void OnMouseDown() {
        Pickup();
    }

    public void Pickup() {
        bool added = InventoryManager.Instance.AddItem(item);
        if (added) { 
            Destroy(gameObject); 
        }
        else {
            Debug.Log("inventory is full");
        }
    }
}
