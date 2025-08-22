# RPS
 Rock, Paper, Scissors!

<br/>

### Project Overview

This is a Unity-based Rock-Paper-Scissors game with extended features, including multiple difficulty modes, customizable player choices, 2D/3D visual modes, and a fully dynamic UI. The project demonstrates advanced Unity techniques like persistent singletons, runtime UI generation, animator handling, scene-independent game settings, and interface-driven dynamic level design.

The game allows the player to:

* Play classic Rock-Paper-Scissors or extended versions with “Flower” (easy) or “Gun” (hard).

* Switch between 2D backgrounds and 3D character models.

* Adjust volume and difficulty in a main menu options panel.

* See a live round history with round numbers and outcomes.

* Easily support new game modes or rule sets thanks to a flexible IRulesStrategy interface, allowing future extensions without touching core gameplay logic.



<br/>

## Key Features

### Multiple Difficulty Modes

<img width="765" height="191" alt="image" src="https://github.com/user-attachments/assets/4b9d2903-82b2-4a98-bc0c-06e44fc61a38" />
<img width="2259" height="341" alt="image" src="https://github.com/user-attachments/assets/a8672ac5-7eb8-47e1-b902-cafcdb6ab0f2" />
<img width="2178" height="333" alt="image" src="https://github.com/user-attachments/assets/040ec8b1-02f8-4255-a49b-c8ab98fab857" />
<img width="2272" height="350" alt="image" src="https://github.com/user-attachments/assets/ba998615-d7b0-417c-b361-b467a4416b6a" />

- **Easy**: Adds a special “Flower” choice that guarantees player victory.
- **Normal**: Classic Rock-Paper-Scissors.
- **Hard**: Adds “Gun” which guarantees AI victory.
- Difficulty selection is handled via a **dropdown** in the options menu and stored in a persistent **GameSettings** singleton.

<br/>

### 2D / 3D Mode Switching

<img width="771" height="515" alt="image" src="https://github.com/user-attachments/assets/f3416612-85b9-458a-a2bc-68088807cf9c" />
<img width="777" height="517" alt="image" src="https://github.com/user-attachments/assets/cfbd1925-97a3-4b3f-8e40-2431d6f1ee90" />

- Player can choose between **2D backgrounds** or **3D character models**.
- **GameInitializer** dynamically spawns the selected prefab or sets the background sprite.
- Supports live changes in the options menu, with selection immediately reflected in the game scene.

<br/>

### Dynamic UI

<img width="1098" height="488" alt="image" src="https://github.com/user-attachments/assets/ed57c5be-a491-488f-883b-f105a35c550a" />

- Choice buttons are **generated dynamically** depending on current difficulty and available options.
- Round history is displayed in a **scrollable list**, showing round numbers, player choice, AI choice, and outcome.
- Game over popup displays the **final winner**.

<br/>

### AI System
- A modular **IAIStrategy** interface allows different AI behaviors.
- Currently implemented with **RandomAIStrategy**, which selects moves randomly based on the available choices for the current difficulty.

<br/>

### Animation Handling

<img width="1459" height="756" alt="image" src="https://github.com/user-attachments/assets/88a7ab94-1a8e-47d6-9b46-27626ef9e464" />

- Player and AI choice animations are **triggered dynamically**.
- Uses **OpponentAnimatorController** to handle AI animation sequences with proper triggers for **win/lose/draw** outcomes.

<br/>

### Persistent Game Settings
- **GameSettings** singleton preserves player preferences across scene loads: volume, mode (2D/3D), selected prefab/background, and difficulty.
- Options menu allows live adjustment of these settings.

<br/>

### Interface-Driven Dynamic Level Design
- Using **IRulesStrategy** allows the game to easily support **new game modes**, rule variations, or special mechanics.
- Adding a new difficulty or special rule only requires implementing the interface, without modifying core **GameManager** logic.
- This makes the project **highly extensible and future-proof** for experimentation with gameplay.
<br/>
<br/>
Thanks for your time and checking out my project, hope you enjoyed! 
