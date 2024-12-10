# FruitRunner
 Runner Game (ECS)
Project Description
This project is an implementation of a "Runner" game built using Unity and the ECS (Entity Component System) architecture. The player runs forward, collects fruits, avoids obstacles, and scores points. The game includes the following states:

Loading: Loading screen.
Lobby: Menu to view rankings and results.
Gameplay: Core game mechanics.
Features
ECS Architecture: Built with DefaultECS for scalable and performant systems.
Object Pooling: Optimized creation of fruits, obstacles, and environmental objects.
Save Progress Locally: Game results are saved locally using PlayerPrefs or JSON.
Dynamic Environment: Procedurally generated platforms and objects for the game world.
Extensibility: Modular systems (Input, Movement, Collision, Score, Save/Load) for easy expansion.
Unity Ready: Integration with Unity Editor for component setup through adapters.
Gameplay Mechanics
Player:
Runs forward and switches lanes.
Colliding with obstacles ends the game.
Collecting fruits adds points.
Fruits:
Different types of fruits with unique colors and scores.
Obstacles:
Placed in the player’s path and require avoidance.
Environment:
Platforms and randomly generated objects for visual variety.


How to Use
Project Setup:
Install Unity (version 2021.3 LTS or newer).
Clone this repository:
bash
Copy code
git clone https://github.com/username/runner-game.git
Open the project in Unity.
Running the Game:
Open the MainScene located in the Assets/Scenes folder.
Press Play in the Unity Editor.
Enjoy the game!
