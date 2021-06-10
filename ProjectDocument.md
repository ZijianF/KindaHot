# Game Basic Information #

## Summary ##

**A paragraph-length pitch for your game.**

## Gameplay Explanation ##

The game's goal is to get to the final destination without dying to enemies.
Controlls:'
Moving: "WASD" keys on keyboard.
Aiming and Shooting: Hold Mouse right click to aim, and left click to shoot where the crosshair points at.
Slowing time: Press left shift key on keyboard to slow down time.
Player is encouraged to use the ability of slowing down time and covers on the map to isolate enemies and take them down one by one.


**If you did work that should be factored in to your grade that does not fit easily into the proscribed roles, add it here! Please include links to resources and descriptions of game-related material that does not fit into roles here.**

# Main Roles #

**Please consider that Since we only have two member on the team, we often will have to do tasks that belong to the same role. Detailed work will be described in the section**

## User Interface

Zijian: Implemented the starting menu, menu after death, and meny after achieving the goal. Also implemented the cross hair showing up while aiming.
The starting menu, "death menu", and "win menu" lets the player make their decision after triggered certain state of the game. For example, when the main character dies, player can choose either try again or quit back to the main menu.
Cross hair only shows up when player hold right click to aim. It gives the players a better idea of exactly where the bullets will go and therefore enhance the controll of the game.


## Movement/Physics

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?** 
There are two parts of this section. Both of us have done some parts for this section
Zijian: The player's movement is based on a script combining with the unity Character Contoller object. (We found out it is not the most convinent way of implementing this).
The Bullet Time (slow motion) feature was implemented by slowing down whole game's time scale and at the same time, gives the main character a little speed boost to emphasize the strength of the ability.

## Animation and Visuals

**List your assets including their sources and licenses.**
Assets used:
Main character model: https://assetstore.unity.com/packages/3d/characters/humanoids/adventurer-blake-158728
CrossHair: https://assetstore.unity.com/packages/2d/gui/icons/crosshairs-plus-139902
Enemy robot soldier model: https://assetstore.unity.com/packages/3d/characters/robots/robot-soldier-142438
Guns and bullet model: 

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**
We try to make the main character's animation match his action.
Using of the robot's model suits better with the fictional theme.
guns: put something here
Due to shortage of time, we did not have much time to polish the game and visual is one of its biggest weaknesses.

## Input
**This part is mainly implemented by Zijian.**
The Defualt input configuration is as such:
Moving: "WASD" keys on keyboard.
Aiming and Shooting: Hold Mouse right click to aim, and left click to shoot where the crosshair points at.
Slowing time: Press left shift key on keyboard to slow down time.

The game currently only supports above imput method.

## Game Logic
Zijian: Except the starting menu scene (pre start state), the game has three states: 
These states are managed by a few scripts that keep track of different objects.
  1. Playing state: player is free to move around and fight with enemies. During this state of the game, player's health and final destination field's state will be tracked to determine whether the player failed or passed the level.
  2. When the player's health points are emptied, a menu will be shown on screen to ask if the player want to try again or go back to the main menu.
  3. When the player manages to touch the final destination field without dying to enemies, the game will congrats the player for beating the level and let player go back to the main menu. 

# Sub-Roles

## Audio

**List your assets including their sources and licenses.**

**Describe the implementation of your audio system.**

**Document the sound style.** 

## Gameplay Testing

**Add a link to the full results of your gameplay tests.**

**Summarize the key findings from your gameplay tests.**

## Narrative Design

**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 

## Press Kit and Trailer

**Include links to your presskit materials and trailer.**

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**



## Game Feel
Zijian: A lot of time was spent on the camera to make it feel interactive. Most of the time, it remains some distance and gives the player a wider field of view to better see the environment. When the player is aiming, view will switch to a secondary camera that has a narrower field of view an more zoomed in, it lets the player better focus on aiming. 
