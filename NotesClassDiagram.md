# Class Diagram Notes

## Index
1. [State Handler](#section-state-handler)
2. [Player](#section-player)
3. [Timer](#section-timer)
4. [Client](#section-client)
5. [Potion](#section-potion)
6. [Brew](#section-brewing)

## Walkthrough on how it works
1. The [level](#section-state-handler) starts, the player appears in the middle of the brewing area, with nothing on their hands, but the ability to grab things from close, and from far away.

2. A (something, probably a timer) appears, running to 0, so the user knows when the actual gameplay is starting, and they can prepare for it.

3. After the [level](#section-state-handler) has started, a [client](#section-client) comes in. They will open the door, walk through it, and arrive in front of the counter. Then they will look at the player and display the [potion](#section-potion) they are asking for.

4. After seeing which [potion](#section-potion) the [client](#section-client) requested, the player can turn around and look at the potions poster, which will have the visual representation of the potion (the same one that the client is displaying) and the visual representation of the ingredients (which will match with the ingredient's containers tags).

5. Now that they know which ingredients they need to fulfill the potion, the player can look around in the room and grab the ingredient they need.

## <a name="section-level-handler"></a> Level handler
The level handler is in charge of starting the level when the player is in position, pausing when the pause menu is opened, finishing the level when the timer runs out, and sending the player to a "won"or "lost"scene afterwards.

## <a name="section-player"></a> Player
The player will have the VR camera on it, and will be able to look at its hands, grab objects that are close, and grab objects that are far away using a raycast from its hands.

## <a name="section-timer"></a> Timer
The timer will be used for the game starting, the game ending, and the client's potions petitions. Timer handles starting when told, having a time variable, and then letting know whoever owns it that the timer has ended.

## <a name="section-client"></a> Client
The clients are the humanoids that enter through the door, walk to the counter, and ask for a potion. If their order is fulfilled and on time, they leave happy. If their order is not fulfilled and time runs out, they leave angry. And if their order is incorrectly fulfilled, they get angry and their timer is reduced.

## <a name="section-potion"></a> Potions
A potion has a dictionary which maps all of the current ingredients with the amount that the potion needs. A potion is a scriptable object!

### Poster
To be able to know which ingredients to put in the pot to brew the potion, the player will be able to look around at one of the walls and look at the Potions poster, which will display all of the needed information.

## <a name="section-potion"></a> Ingredients

## <a name="section-potion"></a> Refiller
When you grab from a refiller, it will spawn the ingredient it represents in yout hand.

## <a name="section-brewing"></a> Brewing

# Other
## Grabbable
Grabbable is a component that is added into anything that 
Whataver physics happens, it only needs to be on the layer of elements that are grabbable. 

### Closeness
If it is close, then it will need to interact with the hand, if it is far then it will interact with a raycast. The ray should onlu be shown when it collides with something that is not close, the hands for close things should be default.

Grab and release (explained afterwards) both depend in whether it is close or far.

### Grab
To handle grabbing, elements will extend grabbable, which is the class that will implement all of the collisions/triggers by the VR implementation.

### Release
To handle releasing, elements will extend or use Grabbables implementaion;

## Container
### Grab ingredient
A prefab will be instanced  when the action of grab ingredient is done on the container. Grab overrides (or extends) grab from Grabbable.

## Storage Manager
Is the one that will know how many ingredients are currently available for the user to use, and that will get the event of added ingredient/be used to add ingredients when the refillers are activated.

### Adding an ingredient
Adding an ingredient can be done by comparing passing the actual object, because all of them are instances of a prefab. But to avoid not actually chekcing the same object, the ingredients will have an Enumerator that says which type of ingredient it is, SS

## Refiller
The refillers are the objects that will refill the current ingredients when grabbed. These will be mostly in the garden. They do not go into your hand, you just "activate" them and a puff goes out. They either release an event where it is let know that an ingredient was grabbed, or directly interact with a storage system.

## Ingredient

### Refilling ingredients

### Ingredients inside of containers
The ingredients will appear in your hand when a grab is made on an ingredients container. Look at Container for more information. 

## Raw ingredients
They will spawn in their place after they have been refilled.

## Brew
Is the current breweing potion that is being done. Is where the added ingredients will be.

### ConvertToPotion
When the user grabs a glass and puts it on the pot, the rew will become a potion and that will be instanced and put as grabbed by the user. 

#### Quetsion - Note
When the user grabs a glass and put in on the pot, does the brewing handle turning itself to a potion? Or dioes the pot turn the brew into a potion? Where does the convert to potion live?

### Add/remove ingredient
There are no removing, so that will not be needed.

## Trash can
Will handle getting rid of the current Brew.
