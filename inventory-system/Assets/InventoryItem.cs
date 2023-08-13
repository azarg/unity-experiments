using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Represents an item in the inventory.
/// * requires initialization to set the item and inventory slot
/// * displays the inventory item as an image
/// * has drag functionality, which makes the inventory item image to follow the mouse when dragged, and move back to the original inventory slot if not dropped on another inventory slot
/// </summary>
[RequireComponent(typeof(Image))]
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // this is where the item's inventory image is displayed
    [SerializeField] private Image image;

    // An Item (scriptable object) that this inventory item represents
    public Item Item { get; private set; }

    // need to remember the current inventory slot and the inventory slot before dragging starts
    // this is needed to support drag/drop functionality
    private InventorySlot inventorySlot;
    private InventorySlot inventorySlotBeforeDrag;
    
    /// <summary>
    /// This is like the constructor for inventory item. Call this function immediately after instantiation
    /// </summary>
    /// <param name="newItem">The item that this inventory item represents</param>
    /// <param name="inventorySlot">The inventory slot to move the item to</param>
    public void Initialize(Item newItem, InventorySlot inventorySlot) {
        Item = newItem;
        MoveToInventorySlot(inventorySlot);

        image.sprite = newItem.sprite;
    }

    public void MoveToInventorySlot(InventorySlot inventorySlot) {
        this.inventorySlot = inventorySlot;
        inventorySlot.SetItem(Item);

        transform.SetParent(inventorySlot.transform);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        image.raycastTarget = false;

        // Remember the original inventory slot when dragging starts.
        // We need this so that we can move the inventory item back to this inventory slot
        // in case when item is not dropped on any other inventory slot
        inventorySlotBeforeDrag = inventorySlot;

        // Set parent to root, so that item visual is displayed above all other visuals
        transform.SetParent(transform.root);
    }

    public void OnEndDrag(PointerEventData eventData) {
        image.raycastTarget = true;

        if (this.inventorySlotBeforeDrag != inventorySlot) {
            // Item's inventory slot has changed, i.e. it was dropped on another inventory slot.
            // remove the item from the previous inventory slot
            inventorySlotBeforeDrag.RemoveItem();
        }
        else {
            // Item's inventory slot has not changed after drag/drop, 
            // which happens when item does not get dropped on another inventory slot.
            // Therefore we set the parent back to the original inventory slot before the drag started
            transform.SetParent(inventorySlotBeforeDrag.transform);
        }
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }
}
