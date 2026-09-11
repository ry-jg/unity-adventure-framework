using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public GameObject inventoryPanel;
    public Transform itemSlotContainer;
    public GameObject itemSlotPrefab;
    public TMP_Text selectedItemText;

    private List<string> items = new List<string>();
    private string selectedItem = null;
    private bool isOpen = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
    }

    public void AddItem(string itemName)
    {
        // Prevent duplicates
        if (!items.Contains(itemName))
        {
            items.Add(itemName);
            RefreshUI();
            Debug.Log("Picked up: " + itemName);
        }
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName))
        {
            items.Remove(itemName);
            if (selectedItem == itemName) selectedItem = null;
            RefreshUI();
        }
    }

    void RefreshUI()
    {
        foreach (Transform child in itemSlotContainer)
            Destroy(child.gameObject);

        // Create a slot for each item
        foreach (string item in items)
        {
            // Instatiate prefab and populate the item details
            string capturedName = item;
            GameObject slot = Instantiate(itemSlotPrefab, itemSlotContainer);
            TMP_Text label = slot.GetComponentInChildren<TMP_Text>();
            if (label != null) label.text = capturedName;

            // Add click listener to select this item
            Button btn = slot.GetComponent<Button>();
            if (btn != null)
                btn.onClick.AddListener(() => SelectItem(capturedName));
        }

        UpdateSelectedText();
    }

    void SelectItem(string itemName)
    {
        selectedItem = itemName;
        UpdateSelectedText();
    }

    void UpdateSelectedText()
    {
        if (selectedItemText == null) return;
        selectedItemText.text = selectedItem != null
            ? "Selected: " + selectedItem
            : "";
    }

    public string GetSelectedItem()
    {
        return selectedItem;
    }
}
