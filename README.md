# Node-Based 3D Adventure & Interaction Framework

A decoupled, event-driven Unity systems framework in C# designed for first-person point-and-click traversal, contextual input raycasting, and stateful puzzle mechanics.

Built as an independent systems architecture showcasing clean system boundaries, design patterns, and package-ready modularity without proprietary asset store dependencies.

---

## Technical Highlights for Reviewers

* **Centralized Input & Contextual Raycast Pipeline (`ClickHandler`):** Leverages Unity's New Input System to process click interactions and menu toggles. Integrates `EventSystem.IsPointerOverGameObject()` and modal state verification to block background raycasts when UI is active. Executes priority-ordered raycast hit evaluation across waypoints, interactable items, and puzzle triggers.
* **Kinematic Camera Traversal (`NavigationManager`, `NodeTarget`):** Implements smooth cinematic movement between discrete spatial viewpoints using `Vector3.Lerp` for position and `Quaternion.Slerp` for rotation across defined nodes.
* **Decoupled Puzzle & State Architecture (`Interactable`, `DoorAnimator`):** Eliminates hard scene couplings by driving world interactions through `UnityEvent` delegates. Employs asynchronous coroutines for rotational animations and timed state transitions.
* **Modal UI Orchestration (`ScreenManager`, `CombinationLock`, `KeypadLock`):** Implements a centralized view controller enforcing single-overlay constraints for inspectable UI puzzles (multi-dial combination locks and numeric keypads) with dynamic event callbacks upon resolution.
* **Asynchronous Feedback Broker (`NotificationManager`):** Manages a queue of text notifications using TextMeshPro, handling fade-in, hold duration, and fade-out via coroutine-driven alpha interpolation.

---

## Architecture Diagram

```
                 [Unity New Input System]
                            │
                            ▼
                    [ClickHandler] ──► (UI Raycast Block Check)
                            │
       ┌────────────────────┼───────────────────┬────────────────────┐
       ▼                    ▼                   ▼                    ▼
 [NodeTarget]         [PickupItem]       [Interactable]      [Modal Lock Screens]
       │                    │                   │                    │
       ▼                    ▼                   ▼                    ▼
[NavigationManager]  [InventoryManager]   [UnityEvents]        [ScreenManager]
(Camera Lerp/Slerp)  (Dynamic UI Slots)  (Door Animations)   (Keypad / Dials)
```

---

## Core Toolset & Competencies

* **Language & Runtime:** C# (.NET), Unity Engine
* **Design Patterns:** Singleton managers (`ScreenManager`, `InventoryManager`, `NotificationManager`), Observer / Delegation (`UnityEvent`), State Machine Flags
* **Unity Subsystems:** Unity New Input System, Physics Raycasting, Canvas EventSystem, TextMeshPro
* **Distribution:** Unity Package Manager (UPM) standard with isolated Assembly Definition (`.asmdef`)

---

## Repository Layout

```text
├── package.json                          # UPM manifest & dependency configuration
├── README.md                             # Systems overview & documentation
├── Runtime/
│   ├── RyanJung.AdventureFramework.asmdef # Isolated assembly definition
│   ├── Core/
│   │   ├── ClickHandler.cs               # Central raycasting & input routing
│   │   ├── NavigationManager.cs          # Kinematic camera interpolation
│   │   └── NodeTarget.cs                 # Waypoint trigger metadata
│   ├── Interactions/
│   │   ├── Interactable.cs               # State validation & UnityEvent dispatcher
│   │   ├── PickupItem.cs                 # Inventory collection trigger
│   │   └── DoorAnimator.cs               # Slerp rotation coroutines
│   ├── UI/
│   │   ├── ScreenManager.cs              # Modal window state manager
│   │   ├── CombinationLock.cs            # Multi-dial combination logic
│   │   ├── KeypadLock.cs                 # Dynamic numeric keypad interface
│   │   ├── InventoryManager.cs           # Dynamic slot generation & selection
│   │   ├── NotificationManager.cs        # Canvas text fade broker
│   │   └── WinScreen.cs                  # Scene reload & completion handler
│   └── Input/
│       ├── InputSystem_Actions.cs        # Input System generated C# interface
│       └── InputSystem_Actions.inputactions
└── Prefabs/                              # Pre-wired native Canvas UI prefabs
    ├── ItemSlot.prefab
    ├── InventoryPanel.prefab
    ├── CombinationLock.prefab
    ├── KeypadLock.prefab
    └── WinPanel.prefab
```

---

## Installation via Unity Package Manager (UPM)

This framework can be imported directly into any Unity project (2022.3 LTS or newer) via Git URL:

1. In the Unity Editor, open **Window** → **Package Manager**.
2. Click **+** in the top-left corner and choose **Add package from git URL...**
3. Paste:
   ```text
   https://github.com/ry-jg/unity-adventure-framework.git
   ```

---

## Licensing & Scope Boundaries

* **Proprietary Asset Exclusion:** Environment meshes, textures, and third-party models from prototype demonstrations have been omitted in strict adherence to third-party licensing.
* **Standalone Architecture:** All included code operates against Unity standard components, TextMeshPro, and native UI primitives.

---

## Developer Contact

**Ryan Jung**  
* B.S. in Computer Science and Engineering, The Ohio State University
* Portfolio: [ry-jg.github.io](https://ry-jg.github.io)  
* GitHub: [github.com/ry-jg](https://github.com/ry-jg)