using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName = "Item";
    public string pickupMessage = "";

    public void Pickup()
    {
        // Add the item to the inventory and show a notification
        InventoryManager.Instance.AddItem(itemName);
        string msg = string.IsNullOrEmpty(pickupMessage)
            ? "You picked up the " + itemName + "."
            : pickupMessage;
        NotificationManager.Instance.Show(msg);
        gameObject.SetActive(false);
    }
}
