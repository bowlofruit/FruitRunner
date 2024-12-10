# **Runner Game (ECS)**

### **Project Description**
This project is an implementation of a "Runner" game built using Unity and the **ECS (Entity Component System)** architecture. The player runs forward, collects fruits, avoids obstacles, and scores points. The game includes the following states:
- **Loading**: Loading screen.
- **Lobby**: Menu to view rankings and results.
- **Gameplay**: Core game mechanics.

---

## **Features**
- **ECS Architecture:** Built with DefaultECS for scalable and performant systems.
- **Object Pooling:** Optimized creation of fruits, obstacles, and environmental objects.
- **Save Progress Locally:** Game results are saved locally using PlayerPrefs or JSON.
- **Dynamic Environment:** Procedurally generated platforms and objects for the game world.
- **Extensibility:** Modular systems (Input, Movement, Collision, Score, Save/Load) for easy expansion.
- **Unity Ready:** Integration with Unity Editor for component setup through adapters.

---

## **Gameplay Mechanics**
1. **Player:**
   - Runs forward and switches lanes.
   - Colliding with obstacles ends the game.
   - Collecting fruits adds points.
2. **Fruits:**
   - Different types of fruits with unique colors and scores.
3. **Obstacles:**
   - Placed in the player’s path and require avoidance.
4. **Environment:**
   - Platforms and randomly generated objects for visual variety.

---

## **How to Use**
### **Project Setup:**
1. Install Unity (version 2021.3 LTS or newer).
2. Clone this repository:
   ```bash
   git clone https://github.com/username/runner-game.git
   ```
3. Open the project in Unity.

### **Running the Game:**
1. Open the `MainScene` located in the `Assets/Scenes` folder.
2. Press **Play** in the Unity Editor.
3. Enjoy the game!

---

## **Extensibility Options**
- Add new types of obstacles or fruits.
- Expand the UI, e.g., include a leaderboard.
- Integrate online progress saving.
- Optimize graphics or collision handling.

---

## **Authors**
- **Serhii Lypskiy** (Unity Developer)
