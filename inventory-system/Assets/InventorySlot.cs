using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler 
{
    public InventoryItem inventoryItem;

    public bool IsEmpty() {
        return inventoryItem == null;
    }

    public void OnDrop(PointerEventData eventData) {
        if (inventoryItem == null) {
            inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.MoveToInventorySlot(this);
        }
    }
}
