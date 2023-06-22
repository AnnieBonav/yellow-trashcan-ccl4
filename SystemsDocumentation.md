# Potionshop Simulator VR Systems

## Ingredient System

The Ingredient System consists of 5 MonoBehaviours and 1 Scriptable Ojbect definition.

### IngredientData.cs

IngredientData stores data for the different ingredient types we use. The scripable objects additionally define the properties an Ingredient will have when it is instanced.

### Ingredient.cs

This Monobehaviour stores all the data an particular Ingredient in the scene has. It also handles VR interactions.

### IngredientContainer.cs

This Component enables us to have objects in the scene that contain a certain amount of ingredients. It uses an IngredientSpawner component and a list of GameOjbect with positions to generate new instances of Ingredient.

### IngredientSpawner.cs

The IngredientSpawner component stores a reference to ingredientData and an Ingredient prefab. It enables us to create instances of Ingredients with the `SpawnIngredient()` function. We use this to add slight varience to each gameobject which has the ingredient component.

### Refiller.cs

The RefillerComponent can be attached to a gameobject and stores a reference to an ingredient Container. It is used to tell the referenced container to refill. We need it because the player interacts with another gameobject than where the components are stored.

### EmptyGrabbable.cs

This components is used to allow us to directly spawn Ingredients from an IngredientContainer into the players hands.

## PotionSystem

The potion system consists of mainly 2 Mono Behaviours and 1 Scriptable Object.

### RecipeData.cs

In this Scriptable object we store which combination of Ingredients produces this Potion type. The setup using Scriptable Objects allows us to expand this easily in the future.

### Potion.cs

This is attached to a GameObject that represents a specific potion. It stores the type of potion and provides functions for VR interactions.

### PotionKnowledgebase.cs

This component uses a singleton pattern to provide information about all possible recipes to other game objects. We use it to generate which Potion a customer requests. At Awake this components loads all Recipes from Ressources and stores it in an Array.

## BrewSystem

The BrewSystem is responsible for the potion that the player is brewing. It mainly consists of 3 MonoBehaviours and interacts with the IngredientSystem and Potion System.

### Brew.cs

This component is used to store what ingredients have been put into the cauldron and decide which potion it is. To do this it checks if the ingredients that are currently in the brew if they correspond to a recipe.

### BrewProperties.cs

We use this to represent the current state of the brew in the cauldron. It calculates bubbling, swirl and the current colour of the brew, interpolating between those values using the different easing functions we learned in class. Every Update this component sends the data of its current state to our custom shader to represent the potion state in game visually.

### IngredientAcceptor.cs

This component uses a Collider to detect what was thrown into the cauldron and call the appropiate functions on the brew to do that it uses the `Take(Ingredient ingredient)` functiion.