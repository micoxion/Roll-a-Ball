# Roll-a-Ball

## Author: Micaiah Soto

## Description  

A simple game in which you roll a ball around with `wasd` and the arrow keys. Collect the floating cubes in the time limit to win!  
Press `spacebar` to jump!  
Press `r` to restart the game at any time  

## Testing

Currently the `PlayerController` calculates the score required to win by iterating over the PickUp `GameObjects` in the PickUp Parent and summing all their `PickUpData` scripts `pointValue` variables.  
By changing the `scoreOverride` variable in the `PlayerController` to a value greater than 0 you can override the score needed to win.