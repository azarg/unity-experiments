using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler 
{
    private Item item;

    public bool IsEmpty() {
        return item == null;
    }

    public void RemoveItem() {
        item = null;
    }

    public void SetItem(Item item) {
        this.item = item;
    }

    public void OnDrop(PointerEventData eventData) {
        if (item == null) {
            var inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.MoveToInventorySlot(this);
        }
    }
}
