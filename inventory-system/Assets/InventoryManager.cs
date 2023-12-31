using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public InventorySlot[] inventorySlots;
    [SerializeField] private GameObject inventoryItemPrefab;

    private void Awake() {
        Instance = this;
    }

    public bool AddItem(Item item) {
        for (int i = 0; i < inventorySlots.Length; i++) {
            var slot = inventorySlots[i];
            var inventoryItem = slot.inventoryItem;
            if (inventoryItem != null 
                    && item == inventoryItem.Item 
                    && inventoryItem.Item.maxStackSize > inventoryItem.ItemCount) {
                inventoryItem.IncreaseCount();
                return true;
            }
        }
        for (int i = 0; i < inventorySlots.Length; i++) {
            var slot = inventorySlots[i];
            if (slot.IsEmpty()) {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    private void SpawnNewItem(Item item, InventorySlot slot) {
        var newItem = Instantiate(inventoryItemPrefab);
        var inventoryItemController = newItem.GetComponent<InventoryItem>();
        inventoryItemController.Initialize(item, slot);
    }
}
