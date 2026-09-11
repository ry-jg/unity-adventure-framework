using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string requiredItem = "";
    public bool consumeItem = true;
    public bool oneTimeUse = true;

    public string successMessage = "It worked!";
    public string failMessage = "That doesn't work here.";

    public UnityEvent OnSuccess;
    public UnityEvent OnFail;

    private bool used = false;

    public void TryInteract()
    {
        if (oneTimeUse && used) return;

        // If no item is required, just succeed
        if (string.IsNullOrEmpty(requiredItem))
        {
            Succeed();
            return;
        }

        // Check if the selected inventory item matches the required item
        string selected = InventoryManager.Instance.GetSelectedItem();
        if (selected == requiredItem)
        {
            if (consumeItem)
                InventoryManager.Instance.RemoveItem(requiredItem);
            Succeed();
        }
        else
        {
            Fail();
        }
    }

    void Succeed()
    {
        used = true;
        if (!string.IsNullOrEmpty(successMessage))
            NotificationManager.Instance.Show(successMessage);
        OnSuccess.Invoke();
    }

    void Fail()
    {
        if (!string.IsNullOrEmpty(failMessage))
            NotificationManager.Instance.Show(failMessage);
        OnFail.Invoke();
    }
}
