There are 5 states:

Walk - Activates when any WASD input sensed and will move the player around and the player can collect food manually, but will transition to patrol when there is not input

Patrol - Character will walk to a random location from a pool of transforms

Hunt -  Activates when hunger is over half full. The character will pick a random food item to walk to and eat with faster movement speed.

Starved - Activates at higher than 75% hunger. The character will pick the CLOSEST food item and move towards it to eat at an even higher speed

Dead - Activates at max hunger. Character can not move. Left clicking will reset all food, and set player back to 0 hunger.

