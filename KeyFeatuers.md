# Key Featuers and Implementation Details

## 3D Modelling

3 humanoid characters are modelled, rigged, textured and animated.
- Fairy Customer (texture painted and rigged)
    - Idle
    - Walk
    - Cheer
- Soldier Customer (texture painted and rigged)
    - Idle
    - Walk
    - Cheer
- Drinking Dwarf (texture painted and rigged)
    - Idle

All of those where exported to .fbx and materials setup in Unity.

About 50 props where created including:
- 8 potions
- 4 Ingredients
- 3 Ingredient refillers
- The building
- Pot on Socket
- ...


## Audio

- Audio is implemented using WWise
- All objects sound believable. For example:
    - Dropping Stuff
    - Footsteps
    - ...
- Objects in the scene that would reasonably do have sounds implemented spatialized.
- There is acoustic feedback on player actions, eg. creating a potion. Giving a potion to a customer, ...

## Unity Coding

 - Game includes a main menu.
 - Choosing if tutorial runs or not is carried over to the game scene with a singleton that does not get destroyed on load.
 - Game assets are controlled with VR inputs.
 - Interactable objects use Triggers or Colliders depending on need of the game needs.
 - Game is built as an .apk and can be ran on Oculus Quest 2 devices.

 ## C# & Theory of CG&A

The game has a system for representing the current brew's properties. It stores Colour, bubbling and swirl. Whenever an ingredient is added to the brew there are values added to those properties depending on the added ingredient. To reach the new value, the brew properties interpolate to the new value over 1 second using different easings.
The colour changes linearly, bubbling uses EaseOutBounce and swirl uses EaseInCubic. For more details refer to the implementation under `Assets/Scripts/BrewSystem/BrewProperties.cs`. Those Values are put into a custom shader we created every Frame to represent the current state of the brew to the player.

