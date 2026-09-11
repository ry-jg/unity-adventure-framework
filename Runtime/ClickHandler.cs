using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickHandler : MonoBehaviour
{
    public NavigationManager navManager;
    private InputSystem_Actions inputActions;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }

    void Update()
    {
        // Check for inventory input
        if (inputActions.Interact.Inventory.WasPressedThisFrame())
            InventoryManager.Instance.ToggleInventory();

        if (!inputActions.Interact.Click.WasPressedThisFrame())
            return;

        // Prevent interactions if a screen is open or if the pointer is over a UI element
        if (ScreenManager.Instance != null && ScreenManager.Instance.IsScreenOpen()) return;
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        // Raycast to detect what was clicked
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            NodeTarget target = hit.collider.GetComponent<NodeTarget>();
            if (target != null)
            {
                navManager.GoToNode(target.nodeIndex);
                return;
            }

            PickupItem pickup = hit.collider.GetComponent<PickupItem>();
            if (pickup != null)
            {
                pickup.Pickup();
                return;
            }

            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.TryInteract();
                return;
            }

            CombinationLock combLock = hit.collider.GetComponent<CombinationLock>();
            if (combLock != null)
            {
                combLock.OpenLock();
                return;
            }

            KeypadLock keypad = hit.collider.GetComponent<KeypadLock>();
            if (keypad != null)
            {
                keypad.OpenKeypad();
                return;
            }
        }
    }

    void OnDestroy()
    {
        inputActions.Disable();
    }
}
