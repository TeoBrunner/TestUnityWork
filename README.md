# AxGrid Mini-Slot Machine
**A test task for a Unity Developer position, demonstrating work with the AxGrid framework, FSM architecture, and particle systems.**

https://github.com/user-attachments/assets/ceeae282-5f74-461c-8e0c-eb9762352ae2

The solution is located at `Assets/TASK3 Solution`

## 📖 Project Description
The project implements a "loot box" or mini-slot mechanic. The main goal is to create an infinite scroll of elements with smooth acceleration and precise stopping centered on the target element.

**Key Features**
- **Infinite Reel**: Implemented slot "teleportation" (Wrapping) logic, enabling an infinite spinning effect with a minimal number of objects (4 slots).
- **Precise Stop**: The reel does not stop at an arbitrary position — it automatically centers the nearest sprite within the mask.
- **Particle System**: Upon stopping, an effect is triggered that dynamically captures the sprite of the dropped reward.

## 🛠 Tech Stack
- **AxGrid Framework & FSM**: Full state management (Idle, Spinning, Stopping).
  - **Model**: Reactive data and button state management.
  - **MonoBehaviourExt**: Use of `[OnStart]`, `[OnUpdate]`, and `[Bind]` attributes for clean code.
- **UI**: Binding via `UIButtonDataBind`, eliminating direct Inspector references.
- **Animations**: Coroutines and mathematical Easing functions for smooth deceleration.

## 🏗 Architecture & Design Decisions
1. **FSM States**
The logic is split into independent state classes:
- `IdleState` — awaiting input, buttons are active.
- `SpinningState` — gradual speed ramp-up, Stop button is locked for the first 3 seconds.
- `StoppingState` — calculates the distance to the "perfect center" and triggers smooth deceleration.

2. **Smooth Stop Algorithm**
Instead of simply reducing speed to zero, a target distance (S) is calculated:
`S = (StepY × Rounds) + OffsetToCenter`
This guarantees that the reel always stops pixel-perfect at the center, regardless of the current speed at the moment the button is pressed.

4. **Dynamic Particles**
The Texture Sheet Animation module in Sprites mode is used to work with a Sprite Atlas. This avoids creating unique materials for each symbol and maintains high performance (a single Draw Call for all particles).

## 📝 Reviewer Notes
The following challenges were addressed during development:
- **Decoupling**: Display logic (`ReelView`) is fully separated from control logic (`FSM`); interaction happens through `Settings.Model`.
- **UX/UI**: Button locking is implemented via `DataBind` to prevent invalid state transitions.
- **Optimization**: Slot objects are reused, and sprites are combined into a Sprite Atlas.
- **Responsiveness**: The window content renders correctly at all possible resolutions.

https://github.com/user-attachments/assets/544666f4-cf82-4f54-96ef-8ded8abaea8c
