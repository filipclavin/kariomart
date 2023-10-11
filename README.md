# KarioMart

## Description
The game starts in the main menu where you can switch between 1-player mode and 2-player mode and pick a track to play.<br><br>
The moment you pick a track, the race starts. The first player to complete a lap wins. There are also boosts that spawn in positions randomly decided at the start of the race. These boosts respawn every 10 seconds. The boosts uncap your max speed for two seconds.<br><br>
When a player wins, the game stops the race, announces the winner, and provides a button to go back to the main menu.

## Quickstart
1. Clone this repository and import the project into your Unity Hub. Unity should set everything up for you, but in case anything goes wrong, the project's Unity version is **2022.3.8f1** and the only installed package is **Input System**.<br>
2. Load the MainMenu scene under the Scenes folder, enter the game view, and press play.

## Structure
I tried to think about limiting each script's responsibilities to one thing or a small set of things - for instance - the GameManager's only responsibility is to handle scene loading, and only the GameManager handles it. The other managers (the MainMenuManager, RaceManager and PauseMenuManager) are unconcerned with how to load the other scenes, making their respective scripts cleaner. I also tried to separate responsibilities within a script into different methods but failed to do so in some places like the RaceManager due to some time pressure caused by a highly busy work schedule.<br><br>
Had I not been as swamped outside of school, I would have liked to improve not only the scripts' structures - but also their extendibility. For instance, right now the game is pretty hard-coded to be run with either one or two players. In order to support N players, you would need to change quite a bit of code inside the scripts, which goes against the Open-Closed principle.

## Controls
Player 1 controls the orange car with WASD \(or a gamepad, in which case it's the right trigger to go forward, the left trigger to go backward, and the left analog stick to steer\). If you're playing in 2-player mode, player 2 controls the black car with the arrow keys.<br><br>
Player 1 may pause the game with esc \(or start if using a gamepad\), from where they may choose to resume or go back to the main menu.

## Sources
Before this project, I did not know how you should achieve running code after a delay in C#. [This StackOverflow thread](https://stackoverflow.com/questions/545533/delayed-function-calls) helped me, [and this piece of C# documentation](https://stackoverflow.com/questions/22872589/how-to-cancel-await-task-delay](https://stackoverflow.com/questions/22872589/how-to-cancel-await-task-delay)https://stackoverflow.com/questions/22872589/how-to-cancel-await-task-delay) showed how to cancel a delayed task.

Author: Filip Clavin
