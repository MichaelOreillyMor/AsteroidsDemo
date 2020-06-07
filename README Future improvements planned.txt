-----High priority tasks-----

-Add turn arround (layer 0) animation

-Create an AI that loads asteroids patterns instead of levels, these patterns will have a level of difficulty assigned.
-The new patterns to spawn will be selected getting all the patterns closed to the current game dificulty (patterns selection range)
-This AI will replace the levelsHandler and will control the AsteroidsHandler.
-The patterns can be composed of big, medium and small asteroids. Their velocity when they are spawned would depend on their size.
-New patterns will not be spawned if the limit of asteroids alive has been reached.

-The game dificulty level, from 0f to 1f, will depend on: 
  .If the player is destroying too many asteroids.
  .If the player is not destroying enough asteroids.
  .If the player is still all the time.
  .How many asteroids are on the screen.
  .The time since the start of the game.

-The conditions to calculate the difficulty value will be measured from 0 to 100. The condition with the highest score will determine the difficulty of the Game at that moment.
-The Dificulty will be calculated every N seconds
-The conditions will share an abstract class.
-The conditions will listen to the messages that they need to get their measured and calculate their difficulty value.
-The conditions will have an animation curve to calculate their difficulty value (value (0f, 1f) time (0f,1f) ). Time = measured value / Max measured value.

-A custom Inspector will be nice to see all this information and change the values, the conditions, their current value, the current number of asteroids on screen...

-Balance dificulty, make the game fun.

-Unit tests.

-Asset bundles

-----Low priority tasks-----

-Desing and change UI to TextMesh Pro when all the game is well defined

-Adapts controls to mobile

-Mobile otimizations and check https://thegamedev.guru/unity-performance-checklist/

-Text localization, use Smart Localization https://github.com/NiklasBorglund/Smart-Localization-2

-Documentation, use Doxigen http://www.doxygen.nl/download.html