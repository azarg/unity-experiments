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
    [SerializeField] private Image itemImage;
    [SerializeField] private Text countDisplay;

    // An Item (scriptable object) that this inventory item represents
    public Item Item { get; private set; }

    private int itemCount;
    public int ItemCount {
        get {
            return itemCount;
        }
        private set {
            itemCount = value;
            RefreshCountDisplay();
        }
    }

    // need to remember the current inventory slot and the inventory slot before dragging starts
    // this is needed to support drag/drop functionality
    private InventorySlot inventorySlot;
    private InventorySlot inventorySlotBeforeDrag;
    
    /// <summary>
    /// This is like the constructor for inventory item. Call this function immediately after instantiation
    /// </summary>
    /// <param name="newItem">The item that this inventory item represents</param>
    /// <param name="inventorySlot">The inventory slot to move the item to</param>
    public void Initialize(Item newItem, InventorySlot inventorySlot, int count = 1) {
        Item = newItem;
        ItemCount = count;
        MoveToInventorySlot(inventorySlot);

        itemImage.sprite = newItem.sprite;
    }

    public void IncreaseCount(int count = 1) {
        ItemCount += count;
    }
    public void DecreaseCount(int count = 1) {
        ItemCount -= count;
    }

    private void RefreshCountDisplay() {
        countDisplay.text = ItemCount.ToString();
        countDisplay.gameObject.SetActive(ItemCount > 1);
    }

    public void MoveToInventorySlot(InventorySlot inventorySlot) {
        this.inventorySlot = inventorySlot;
        inventorySlot.inventoryItem = this;

        transform.SetParent(inventorySlot.transform);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        itemImage.raycastTarget = false;

        // Remember the original inventory slot when dragging starts.
        // We need this so that we can move the inventory item back to this inventory slot
        // in case when item is not dropped on any other inventory slot
        inventorySlotBeforeDrag = inventorySlot;

        // Set parent to root, so that item visual is displayed above all other visuals
        transform.SetParent(transform.root);
    }

    public void OnEndDrag(PointerEventData eventData) {
        itemImage.raycastTarget = true;

        if (this.inventorySlotBeforeDrag != inventorySlot) {
            // Item's inventory slot has changed, i.e. it was dropped on another inventory slot.
            // remove the item from the previous inventory slot
            inventorySlotBeforeDrag.inventoryItem = null;
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
