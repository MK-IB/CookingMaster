# CookingMaster
A “programmer-art” version of a salad chef simulation in Unity.

## Overview
2 players having two different controls are the cooks. They will go to different counters containing vegetables to prepare salads for customers.
Each customer will ask for unique salad combination to the players.

## Features
- Key Guide: On the start screen user is guided about the control system for both the players.
- Score: Scores will be added to each player for serving right salad to a customer. Dissatisfied customer leaves negative score for both players.
- Timer: Each player will have certain time period to serve to as many customer as they want, after which game will end.
- Winner: The player having highest score will win and will be shown inthe Game Over panel.

  ## Steps I would take to complete the Unsolved Features
  - Pickups (powerups): I would detect the customer being served by a player and access and check if its remaining timer is 70% of the total wait timer.
    if so, I would spawn a pickup in the counter area with script having PlayerType enum and set it to the player type served.
    Any player colliding with the pickup will be checked if its player type is same as the pickup type.
    Then only that player can take it.
    ++ After acquiring pickup
            - I would add certain value to the player speed.
            - Would add certain point to the player more than the normal customer serving point.
            - Would also increase the player's timer.
  - Reset and New Round: I would have a button on the Game Over panel which would reload the level after saving the player's score in the Unity PlayerPrefs.
