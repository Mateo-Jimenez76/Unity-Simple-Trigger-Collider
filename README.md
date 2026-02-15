# Quick Start
1. Attach a TriggerCollider2D or TriggerCollider
2. Click + on any of the UnityEvents you wish to use
3. Drag in the "CommonUseCaseFunctions" asset found at the path Assets/Resources/CommonUseCaseFunctions.asset" into the object box

> [!TIP]
> You can also use your own custom functions by dragging any object that has scripts with functions you wish to call 😊

5. Select your desired function and your good to go :D

![Trigger Collider Demonstration](https://github.com/user-attachments/assets/90c28c10-2248-437a-8159-5ef866222fbe)


## Dependencies
- Only tested and approved for Unity Editor versions 6000.2.8f1 and above.
- The Demo scene requires [Cinemachine](https://docs.unity3d.com/Packages/com.unity.cinemachine@3.0/manual/index.html) 3.1.5 to be installed (The actual package does not require this for functionality)

# Features

### Components
| Name | Purpose |
|-------------|---------|
| TriggerCollider2D | Listens for OnTrigger2D functions and invokes the corresponding UnityEvent |
| TriggerCollider | Listens for OnTrigger functions and invokes the corresponding UnityEvent |

### Common Use Case Functions
I have included a scriptable object that contains a handful of helpful functions that are often used in the context of trigger colliders.
The reason I included this feature is so you can get implimenting and testing right away! 
And then later replace these functions with more polished and fine tuned versions if you wish.

- LoadSceneAsync(string sceneName)
- DestroyObjectCollidedWith(Collider2D collision)
- DestroyObjectCollidedWith(Collider collision)
- LogCollision2D(Collider2D collision)
- LogCollision(Collider collision)

>[!NOTE]
> The 2D/3D versions of functions will only appear when being used with their corresponding trigger colliders. 
> Thus it is impossible to use LogCollision(Collider collision), for example, with a TriggerCollider2D component

# Understanding The Behaviour of Trigger Colliders

>[!NOTE]
> The only difference between TriggerCollider and TriggerCollider2D is what physics functions they listen to, OnTriggerEnter or OnTriggerEnter2D and so on, and the corresponding Collider or Collider2D component that they rely on. Functionality and flexibility are otherwise the same between scripts.

A component of type Collider(2D) is required by the scripts. If one is not already present, then a BoxCollider(2D) will be added to the object and marked as trigger.

>[!TIP]
> The Collider(2D) that is created by default can be changed in the settings menu of the package found in <b>Edit > Project Settings > Simple Trigger Colliders</b>

![Trigger Collider Settings Demonstration](https://github.com/user-attachments/assets/0a6f66df-7371-4d21-bf40-b9229e143140)

> [!WARNING]
> A component of type Collider(2D) cannot be removed if it is the only Collider(2D) on the game object.
> If it does get removed then another Collider(2D) will be added to the object(only works in edit mode).
> This is done to prevent the script from breaking as it relies on a Collider(2D) component being present on the object.

# Understanding The Defined Unity Events

Each [UnityEvent<Collider(2D),GameObject>](https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Events.UnityEvent.html) in the inspector corresponds with their respective OnTrigger functions.
- OnTriggerEnter(2D)
- OnTriggerStay(2D)
- OnTriggerExit(2D)

There are two ways to pass in data to functions called by the UnityEvent<Collider(2D),GameObject> 
1. Writing the data explicitly in the inspector
2. Having the data be passed in automatially through the OnTrigger calls.

The first option is fairly simple, assign the object, pick the function you wish to call and input the values.

> [!WARNING]
> When using method 1 the function must only have 0-1 parameters, have a return type of void, and be public in order to appear as an option in the inspector.

In order to use method 2 the function must only take in only two parameters (Collision(2D) collision, GameObject caller).

> [!TIP]
> You can still subscribe to the OnTrigger event through code if the function doesnt take in the two parameters, but you lose access to the data.

## Unity Event Execution Order

The execution order of functions listed within any given UnityEvent is relatively unknown, and thus it cannot be relied on. For example,

<img width="674" height="388" alt="UnityEvent Execution Example" src="https://github.com/user-attachments/assets/52f48d95-1492-4cab-9935-b24cc4128077" />

looking at the above image it would make sense that the object would...

1. Play a sound named "pickupCoin"
2. Play a Particle System effect
3. Log the collision
4. Deactivate the object

...all in that order when something enters its trigger collider. However when testing, the observed execution order is as follows

1. Log the collision
2. Deactivate the object

The sound and particle effect functions are ignored because those two components are on the object.
And since the object is now innactive the functions have no effect.

> [!NOTE]
> To read more about what an object being "inactive", "disabled", or "deactivated" means visit the official Unity documentation on [GameObject.SetActive](https://docs.unity3d.com/6000.3/Documentation/ScriptReference/GameObject.SetActive.html) 

<b>Solution 1</b>

You can move functionality away from the object that gets deactivated thus allowing the functions to have their effects go though.

> [!NOTE]
> Every function in a UnityEvent is called whether or not the invoking object becomes inactive due to one of those calls.

<b>Solution 2</b>

You can also take advantage of the OnTriggerStay and OnTriggerExit events. These events DO have an explicit execution order relative to each other that goes as follows

1. OnTriggerEnter
2. OnTriggerStay
3. OnTriggerExit

Thus we can move the SetActive(false) function call to OnTriggerStay to ensure that all other functions are called before we deactivate the object.

<b>Solution 3</b>

You can move the functionality into your own custom functions, thus allowing you to more directly control execution order and functionality.
Although this takes more work I do recommened it, once you are past initial testing and prototyping, as you can implement [error handling](https://www.geeksforgeeks.org/dsa/error-handling-in-programming/) and [conditional operations](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators) to make more robust and full proof products.

### Understanding The Common Use Case Functions

> [!NOTE]
> The reason for the use of a scriptable object is to circumvent the limitation that UnityEvents can only call functions from object references and not scripts.

Another method of using these functions is directly through code. You can use the functions by making a reference to CommonUseCaseFunctions.[Function Name] as all functions are static.



